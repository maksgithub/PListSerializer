using System;
using System.Collections.Generic;

namespace PListSerializer.Core.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsList(this Type type)
        {
            return type != null &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(this Type type)
        {
            return type != null &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
    }
}