using System;
using PListNet;

namespace PListSerializer.Core.Converters
{

    public interface IPlistConverter
    {
    }
    public interface IPlistConverter<T> : IPlistConverter
    {
        T Deserialize(PNode node);
    }

    //class StringConverter : IPlistConverter<string>
    //{
    //    public string Deserialize1(PNode node)
    //    {
    //    }
    //}
}