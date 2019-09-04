using PListNet;

namespace PListSerializer.Core.Converters
{
    public class IntegerConverter : IPlistConverter<int>
    {
        public int Deserialize(PNode node)
        {
            if (node is PNode<long> genericNode)
            {
                return (int)genericNode.Value;
            }

            return default;
        }

        public void Add()
        {
            throw new System.NotImplementedException();
        }
    }
}