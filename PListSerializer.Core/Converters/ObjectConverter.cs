using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    public class ObjectConverter<TObject> : IPlistConverter<TObject>
    {
        private readonly Func<TObject> _activator;
        private readonly EqualityComparer<TObject> _equalityComparer;
        private readonly Dictionary<string, Action<TObject, PNode>> _deserializeMethods;

        public ObjectConverter(Dictionary<PropertyInfo, 
            IPlistConverter> propertyConverters, PropertyInfo p)
        {
            var outInstanceConstructor = typeof(TObject).GetConstructor(Array.Empty<Type>());

            _activator = outInstanceConstructor == null
                ? throw new Exception($"Default constructor for {typeof(TObject).Name} not found")
                : Expression.Lambda<Func<TObject>>(Expression.New(outInstanceConstructor)).Compile();

            _deserializeMethods = propertyConverters.ToDictionary(
                pair => pair.Key.Name,
                pair => BuildDeserializeMethod(pair.Key, pair.Value));

            _deserializeMethods.Add(p.Name, BuildDeserializeMethod(p, this));
            _equalityComparer = EqualityComparer<TObject>.Default;
        }


        public void Add()
        {
            throw new NotImplementedException();
        }
        public void Add(string key, Action<TObject, PNode> value)
        {
            _deserializeMethods.Add(key, value);
        }
        public TObject Deserialize(PNode tokenizer1)
        {
            var instance = _activator();
            if (tokenizer1 is DictionaryNode tokenizer2)
            {
                var tokenizer = tokenizer2.GetEnumerator();
                while (tokenizer.MoveNext())
                {
                    var token = tokenizer.Current;
                    var propertyName = token.Key;
                    if (!_deserializeMethods.TryGetValue(propertyName, out var converter))
                        continue;

                    converter(instance, token.Value);
                }
            }

            return instance;
        }

        private static Action<TObject, PNode> BuildDeserializeMethod(PropertyInfo property,
            IPlistConverter propertyValueConverter)
        {
            const string deserializeMethodName = nameof(IPlistConverter<object>.Deserialize);

            var instance = Expression.Parameter(typeof(TObject), "instance");
            var tokenizer = Expression.Parameter(typeof(PNode), "tokenizer");

            var converterType = propertyValueConverter.GetType();
            var deserializeMethod = converterType.GetMethod(deserializeMethodName);

            if (deserializeMethod == null)
            {
                throw new InvalidOperationException($"Bad converter for type {property.PropertyType}");
            }

            var converter = Expression.Constant(propertyValueConverter, converterType);
            var propertyValue = Expression.Call(converter, deserializeMethod, tokenizer);

            var body = Expression.Assign(Expression.Property(instance, property), propertyValue);
            return Expression
                .Lambda<Action<TObject, PNode>>(body, instance, tokenizer)
                .Compile();
        }
    }
}