using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        public static bool IsList(this Type type)
        {
            if (type == null) return false;
            return
                type.IsGenericType &&
                type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(this Type type)
        {
            if (type == null) return false;
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        public static bool IsDictionary(this PropertyInfo property)
        {
            return property.PropertyType.IsDictionary();
        }
    }
}