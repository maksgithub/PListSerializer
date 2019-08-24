using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer
{
    class Program
    {

        static void Main(string[] args)
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.PList1);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);

            var t = Deserialize<quiz>(node);
        }

        private static T Deserialize<T>(PNode node) where T : class, new()
        {
            var deserialize = new T();
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
                            prop.SetValue(deserialize,nodeVal3);
                        }
                    }
                }
            }
            return default(T);
        }
    }

    internal class quiz
    {
        public List<question> question { get; set; }
    }

    internal class question
    {
        private string text { get; set; }
        private string answer { get; set; }
    }
}
