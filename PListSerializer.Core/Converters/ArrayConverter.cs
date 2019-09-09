using System.Collections.Generic;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    internal sealed class ArrayConverter<TElement> : IPlistConverter<TElement[]>
    {
        private readonly IPlistConverter<TElement> _elementConverter;

        public ArrayConverter(IPlistConverter<TElement> elementConverter)
        {
            _elementConverter = elementConverter;
        }

        public TElement[] Deserialize(PNode rootNode)
        {
            var buffer = new List<TElement>(10);
            if (!(rootNode is ArrayNode arrayNode))
            {
                return default;
            }

            using (var enumerator = arrayNode.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var pNode = enumerator.Current;
                    var element = _elementConverter.Deserialize(pNode);
                    buffer.Add(element);
                }
            }

            return buffer.ToArray();
        }
    }
}
