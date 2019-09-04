﻿using PListNet;

namespace PListSerializer.Core.Converters
{
    public class PrimitiveConverter<T> : IPlistConverter<T>
    {
        public T Deserialize(PNode node)
        {
            if (node is PNode<T> genericNode)
            {
                return genericNode.Value;
            }

            return default;
        }

        public void Add()
        {
            throw new System.NotImplementedException();
        }
    }
}