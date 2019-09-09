using System.Collections.Generic;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    class ListConverter<TVal> : IPlistConverter<List<TVal>>
    {
        private readonly IPlistConverter<TVal> _elementConverter;

        public ListConverter(IPlistConverter<TVal> elementConverter)
        {
            _elementConverter = elementConverter;
        }

        public List<TVal> Deserialize(PNode rootNode)
        {
            if (!(rootNode is ArrayNode dictionaryNode))
            {
                return default;
            }

            var result = new List<TVal>();

            using (var enumerator = dictionaryNode.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var token = enumerator.Current;
                    var element = _elementConverter.Deserialize(token);
                    result.Add(element);
                }
            }
            return result;
        }
    }
}
