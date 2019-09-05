using PListSerializer.Core.Attributes;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class Plist
    {
        [PlistName("group_identifier")]
        public string GroupIdentifier { get; set; }

        [PlistName("kMPPresetIdentifierKey")]
        public string PresetIdentifierKey { get; set; }

        [PlistName("priority")]
        public int Priority { get; set; }

        public bool Hidden { get; set; }

        [PlistName("uuid")]
        public string Id { get; set; }

        public AdjustmentLayer[] AdjustmentLayers { get; set; }
    }
}