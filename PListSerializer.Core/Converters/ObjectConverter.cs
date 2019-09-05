using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    public class ObjectConverter<TObject> : IPlistConverter<TObject>
    {
        private readonly Func<TObject> _activator;
        private readonly Dictionary<string, Action<TObject, PNode>> _deserializeMethods;

        public ObjectConverter(Dictionary<PropertyInfo, IPlistConverter> propertyConverters, PropertyInfo propertyInfo)
        {
            var outInstanceConstructor = typeof(TObject).GetConstructor(Array.Empty<Type>());

            _activator = outInstanceConstructor == null
                ? throw new Exception($"Default constructor for {typeof(TObject).Name} not found")
                : Expression.Lambda<Func<TObject>>(Expression.New(outInstanceConstructor)).Compile();

            _deserializeMethods = propertyConverters.ToDictionary(
                pair => pair.Key.Name,
                pair => BuildDeserializeMethod(pair.Key, pair.Value));

            if (propertyInfo != null)
            {
                _deserializeMethods.Add(propertyInfo.Name, BuildDeserializeMethod(propertyInfo, this));
            }
        }

        public TObject Deserialize(PNode rootNode)
        {
            var instance = _activator();
            if (rootNode is DictionaryNode dictionaryNode)
            {
                using (var enumerator = dictionaryNode.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var token = enumerator.Current;
                        var propertyName = token.Key;
                        if (_deserializeMethods.TryGetValue(propertyName, out var converter))
                        {
                            converter(instance, token.Value);
                        }
                    }
                }
            }

            return instance;
        }

        private static Action<TObject, PNode> BuildDeserializeMethod(PropertyInfo property,
            IPlistConverter propertyValueConverter)
        {
            const string deserializeMethodName = nameof(IPlistConverter<object>.Deserialize);

            var instance = Expression.Parameter(typeof(TObject));
            var parameter = Expression.Parameter(typeof(PNode));

            var converterType = propertyValueConverter.GetType();
            var deserializeMethod = converterType.GetMethod(deserializeMethodName);

            if (deserializeMethod == null)
            {
                throw new InvalidOperationException($"Bad converter for type {property.PropertyType}");
            }

            var converter = Expression.Constant(propertyValueConverter, converterType);
            var propertyValue = Expression.Call(converter, deserializeMethod, parameter);

            var body = Expression.Assign(Expression.Property(instance, property), propertyValue);
            return Expression
                .Lambda<Action<TObject, PNode>>(body, instance, parameter)
                .Compile();
        }
    }
}