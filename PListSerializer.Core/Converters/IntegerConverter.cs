using PListNet;

namespace PListSerializer.Core.Converters
{
    internal class IntegerConverter : IPlistConverter<int>
    {
        public int Deserialize(PNode rootNode)
        {
            if (rootNode is PNode<long> genericNode)
            {
                return (int)genericNode.Value;
            }

            return default;
        }
    }
}