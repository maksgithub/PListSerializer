using System;
using PListNet;

namespace PListSerializer.Core.Converters
{
    public interface IPlistConverter
    {
    }

    public interface IPlistConverter<out T> : IPlistConverter
    {
        T Deserialize(PNode rootNode);
    }
}