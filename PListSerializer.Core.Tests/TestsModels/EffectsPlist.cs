using PListSerializer.Core.Attributes;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class EffectsPlist
    {
        public string InfoImageName { get; set; }

        [PlistName()]
        public string InfoImageName { get; set; }
    }
}