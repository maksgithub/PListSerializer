using System;
using PListNet;

namespace PListSerializer.Core.Converters
{
    public interface IPlistConverter
    {
    }

    internal interface IPlistConverter<out T> : IPlistConverter
    {
        T Deserialize(PNode rootNode);
    }
}