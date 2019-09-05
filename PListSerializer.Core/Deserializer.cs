﻿using System;
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
            return typedConverter.Deserialize(source);
        }

        private IPlistConverter GetOrBuildConverter(Type type)
        {
            return _converters.GetOrAdd(type, () => BuildConverter(type));
        }

        public bool IsList(Type type)
        {
            if (type == null) return false;
            return
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public bool IsDictionary(Type type)
        {
            if (type == null) return false;
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        private IPlistConverter BuildConverter(Type type)
        {
            if (IsDictionary(type))
            {
                Type[] arrayElementType = type.GenericTypeArguments;
                var keyConverter = GetOrBuildConverter(arrayElementType[0]);
                var valueConverter = GetOrBuildConverter(arrayElementType[1]);

                var dicitionaryConverterType = typeof(DictionaryConverter<>).MakeGenericType(arrayElementType[1]);
                return (IPlistConverter)Activator.CreateInstance(dicitionaryConverterType, valueConverter);
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
            var objectPropertyConverters = properties
                .Where(x => x != propertyInfo)
                .ToDictionary(p => p, p => GetOrBuildConverter(p.PropertyType));

            var objectConverterType = typeof(ObjectConverter<>).MakeGenericType(type);
            var plistConverter = (IPlistConverter)Activator
                .CreateInstance(objectConverterType, objectPropertyConverters, propertyInfo);

            return plistConverter;
        }
    }
}
