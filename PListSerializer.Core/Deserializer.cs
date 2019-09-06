using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PListNet;
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

        public TOut Deserialize<TOut>(PNode source)
        {
            var outType = typeof(TOut);
            var converter = GetOrBuildConverter(outType);
            var typedConverter = (IPlistConverter<TOut>)converter;
            var deserialize = typedConverter.Deserialize(source);
            return deserialize;
        }

        private IPlistConverter GetOrBuildConverter(Type type)
        {
            return _converters.GetOrAdd(type, () => BuildConverter(type));
        }

        private IPlistConverter BuildConverter(Type type)
        {
            if (type.IsDictionary())
            {
                var arrayElementType = type.GenericTypeArguments;
                var keyConverter = GetOrBuildConverter(arrayElementType[0]);
                var valueConverter = GetOrBuildConverter(arrayElementType[1]);

                var dictionaryConverterType = typeof(DictionaryConverter<>).MakeGenericType(arrayElementType[1]);
                return (IPlistConverter)Activator.CreateInstance(dictionaryConverterType, valueConverter);
            }

            if (type.IsArray)
            {
                Type arrayElementType = type.GetElementType();
                var arrayElementConverter = GetOrBuildConverter(arrayElementType);

                var arrayConverterType = typeof(ArrayConverter<>).MakeGenericType(arrayElementType);
                return (IPlistConverter)Activator.CreateInstance(arrayConverterType, arrayElementConverter);
            }

            var properties = type.GetProperties();

            var propertyInfo = properties.FirstOrDefault(x => x.PropertyType == type);
            var propertyInfos = properties
                .Where(x => x != propertyInfo)
                .Where(x =>
                {
                    var elementType = x.PropertyType.GetElementType();
                    var b = elementType == type;
                    var b1 = x.PropertyType.IsArray && b;
                    return !b1;
                })
                .Where(x =>
                {
                    if (x.IsDictionary())
                    {
                        var t = x.PropertyType.GenericTypeArguments.FirstOrDefault(x2 => x2 == type);
                        return t == null;
                    }
                    return true;
                })
                .ToList();

            var objectPropertyConverters = propertyInfos
                .ToDictionary(p => p, p => GetOrBuildConverter(p.PropertyType));

            var objectConverterType = typeof(ObjectConverter<>).MakeGenericType(type);
            var plistConverter = (IPlistConverter)Activator
                .CreateInstance(objectConverterType, objectPropertyConverters, propertyInfo);

            return plistConverter;
        }
    }
}
