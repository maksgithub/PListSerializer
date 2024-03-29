﻿using PListNet;

namespace PListSerializer.Core.Converters
{
    internal class PrimitiveConverter<T> : IPlistConverter<T>
    {
        public T Deserialize(PNode rootNode)
        {
            if (rootNode is PNode<T> genericNode)
            {
                return genericNode.Value;
            }

            return default;
        }
    }
}