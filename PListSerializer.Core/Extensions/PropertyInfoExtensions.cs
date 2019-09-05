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
    }
}