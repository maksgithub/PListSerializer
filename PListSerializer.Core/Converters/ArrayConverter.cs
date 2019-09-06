using System.Collections.Generic;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    internal sealed class ArrayConverter<TElement> : IPlistConverter<TElement[]>
    {
        private readonly List<TElement> _buffer;
        private readonly IPlistConverter<TElement> _elementConverter;

        public ArrayConverter(IPlistConverter<TElement> elementConverter)
        {
            _elementConverter = elementConverter;
            _buffer = new List<TElement>(100);
        }

        public TElement[] Deserialize(PNode rootNode)
        {
            if (!(rootNode is ArrayNode arrayNode))
            {
                return default;
            }

            using (var enumerator = arrayNode.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var token = enumerator.Current;
                    var element = _elementConverter.Deserialize(token);
                    _buffer.Add(element);
                }
            }

            var array = new TElement[_buffer.Count];
            for (var i = 0; i < array.Length; i++)
                array[i] = _buffer[i];

            _buffer.Clear();

            return array;
        }
    }
}
