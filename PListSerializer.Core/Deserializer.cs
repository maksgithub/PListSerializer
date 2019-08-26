using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PListNet;
using PListNet.Nodes;
using PListSerializer.Core.Extensions;

namespace PListSerializer.Core
{
    public class Deserializer
    {
        public T Deserialize<T>(PNode node) where T : class, new()
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
    }
}
