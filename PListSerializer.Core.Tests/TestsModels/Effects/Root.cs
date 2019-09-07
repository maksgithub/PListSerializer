using System.Collections.Generic;
using PListSerializer.Core.Attributes;

namespace PListSerializer.Core.Tests.TestsModels.Effects
{
    public class Root
    {
        [PlistName("AdjustmentLayers")]
        public Dictionary<string, Layer> Layers { get; set; }
    }
}