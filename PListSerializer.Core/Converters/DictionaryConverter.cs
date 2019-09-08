using System.Collections.Generic;
using PListNet;
using PListNet.Nodes;

namespace PListSerializer.Core.Converters
{
    public class DictionaryConverter<TVal> : IPlistConverter<Dictionary<string, TVal>>
    {
        private readonly IPlistConverter<TVal> _elementConverter;

        public DictionaryConverter(IPlistConverter<TVal> elementConverter)
        {
            _elementConverter = elementConverter;
        }

        public Dictionary<string, TVal> Deserialize(PNode rootNode)
        {
            if (!(rootNode is DictionaryNode dictionaryNode))
            {
                return default;
            }

            var result = new Dictionary<string, TVal>();

            using (var enumerator = dictionaryNode.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var token = enumerator.Current;
                    var element = _elementConverter.Deserialize(token.Value);
                    result.Add(token.Key, element);
                }
            }
            return result;
        }
    }
}
