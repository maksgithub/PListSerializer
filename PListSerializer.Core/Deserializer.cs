using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PListNet;
using PListNet.Nodes;
using PListSerializer.Core.Converters;
using PListSerializer.Core.Extensions;

namespace PListSerializer.Core
{
    public class Deserializer
    {
        private readonly Dictionary<Type, IPlistConverter> _converters;

        public Deserializer()
        {
            _converters = new Dictionary<Type, IPlistConverter>()
            {
                {typeof(bool), new PrimitiveConverter<bool>()},
                {typeof(int), new IntegerConverter()},
                {typeof(long), new PrimitiveConverter<long>()},
                {typeof(string), new PrimitiveConverter<string>()},
                {typeof(double), new PrimitiveConverter<double>()},
                {typeof(byte[]), new PrimitiveConverter<byte[]>()},
                {typeof(DateTime), new PrimitiveConverter<DateTime>()},
            };
        }
        public T Deserialize1<T>(PNode node) where T : class, new()
        {
            var constructor = typeof(T).GetConstructor(Array.Empty<Type>());
            var r = Expression.Lambda<Func<T>>(Expression.New(constructor)).Compile();
            var deserialize = r();
            var s = typeof(T).Name;
            var dNode = node.GetValue<DictionaryNode>(s);
            foreach (var prop in deserialize.GetType().GetProperties())
            {
                var p = dNode.GetValue<string>(prop.Name);
                prop.SetValue(deserialize, p);
            }

            return deserialize;
        }
        public TOut Deserialize<TOut>(PNode source)
        {
            var outType = typeof(TOut);
            var converter = GetOrBuildConverter(outType);
            // using (var tokenizer = new JsonTokenizer(source, _buffer))
            {
                // if (outType.IsPrimitive || outType == typeof(string)) tokenizer.MoveNext();

                var typedConverter = (IPlistConverter<TOut>)converter;
                return typedConverter.Deserialize(source);
            }
        }

        private IPlistConverter GetOrBuildConverter(Type type)
        {
            if (_converters.TryGetValue(type, out var exists)) return exists;

            var converter = BuildConverter(type);
            _converters.Add(type, converter);

            return converter;
        }

        private IPlistConverter BuildConverter(Type type)
        {
            if (type.IsArray)
            {
                var arrayElementType = type.GetElementType();
                var arrayElementConverter = GetOrBuildConverter(arrayElementType);

                //var arrayConverterType = typeof(ArrayConverter<>).MakeGenericType(arrayElementType);
                //return (IPlistConverter)Activator.CreateInstance(arrayConverterType, arrayElementConverter);
                return default;
            }

            var properties = type.GetProperties().ToList();
            Dictionary<PropertyInfo, IPlistConverter> objectPropertyConverters = properties
                .Where(x => x.PropertyType != type)
                .ToDictionary(p => p, p => GetOrBuildConverter(p.PropertyType));

            PropertyInfo p1 = properties.FirstOrDefault(x => x.PropertyType == type);
            Type objectConverterType = typeof(ObjectConverter<>).MakeGenericType(type);
            IPlistConverter plistConverter = (IPlistConverter)Activator
                .CreateInstance(objectConverterType, objectPropertyConverters, p1);
            return plistConverter;
        }
    }
}
