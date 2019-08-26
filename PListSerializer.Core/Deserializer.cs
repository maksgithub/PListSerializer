using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core
{
    public class Deserializer
    {
        public T Deserialize<T>(PNode node) where T : class, new()
        {
            var constructor = typeof(T).GetConstructor(Array.Empty<Type>());
            var r = Expression.Lambda<Func<T>>(Expression.New(constructor)).Compile();
            var deserialize = r();
            if (node is DictionaryNode dNode)
            {
                var s = typeof(T).Name;
                dNode.TryGetValue(s, out var dNodeVal);
                foreach (var prop in deserialize.GetType().GetProperties())
                {
                    if (dNodeVal is DictionaryNode dNodeVal2)
                    {
                        dNodeVal2.TryGetValue(prop.Name, out var dNodeVal3);
                        {
                            var nodeVal3 = "rt";
                            prop.SetValue(deserialize, nodeVal3);
                        }
                    }
                }
            }


            return deserialize;
        }
    }
}
