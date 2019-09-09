using System.ComponentModel;

namespace PListSerializer.Core.Attributes
{
    public class PlistNameAttribute : DescriptionAttribute
    {
        public PlistNameAttribute(string name) : base(name)
        {
        }
    }
}