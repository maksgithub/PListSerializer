using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PListSerializer.Core.Attributes;

namespace PListSerializer.Core.Extensions
{
    static class PropertyInfoExtensions
    {
        public static string GetName(this PropertyInfo propertyInfo)
        {
            var result = propertyInfo?
                .GetCustomAttributes(typeof(PlistNameAttribute), false)
                .Cast<PlistNameAttribute>()
                .FirstOrDefault();

            return result?.Description ?? propertyInfo?.Name;
        }

        public static bool IsDictionary(this PropertyInfo property)
        {
            return property.PropertyType.IsDictionary();
        }

        public static HashSet<Type> GetGenericSubTypes(this PropertyInfo propertyInfo)
        {
            var result = new HashSet<Type>();
            var propertyType = propertyInfo.PropertyType;
            if (propertyType.IsArray)
            {
                var elementType = propertyType.GetElementType();
                result.Add(elementType);
            }
            else if (propertyType.IsDictionary() || propertyType.IsList())
            {
                result = propertyType.GenericTypeArguments.ToHashSet();
            }

            return result;
        }
    }
}