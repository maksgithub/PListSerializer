using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            _buffer = new List<TElement>(10);
        }

        public TElement[] Deserialize(PNode rootNode)
        {
            if (rootNode is ArrayNode arrayNode)
            {
                var e = arrayNode.GetEnumerator();
                while (e.MoveNext())
                {
                    var token = e.Current;
                    //var tokenType = token.TokenType;

                    //if (tokenType == JsonTokenType.ArrayStart) continue;
                    //if (tokenType == JsonTokenType.ArrayEnd) break;

                    var element = _elementConverter.Deserialize(token);
                    _buffer.Add(element);
                }

                var array = new TElement[_buffer.Count];
                for (var i = 0; i < array.Length; i++)
                    array[i] = _buffer[i];

                _buffer.Clear();

                return array;
            }
            return default;
        }
    }
}
