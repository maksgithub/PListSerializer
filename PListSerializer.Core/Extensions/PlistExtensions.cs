using System.Runtime.InteropServices;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Extensions
{
    public static class PlistExtensions
    {
        public static T GetValue<T>(this PNode pNode, string key)
        {
            if (pNode is DictionaryNode dNode)
            {
                if (dNode.TryGetValue(key, out var dNodeValue))
                {
                    if (dNodeValue is PNode<T> genericSubNode)
                    {
                        return genericSubNode.Value;
                    }

                    if (dNodeValue is T subNode)
                    {
                        return subNode;
                    }
                }
            }

            return default;
        }
    }
}
