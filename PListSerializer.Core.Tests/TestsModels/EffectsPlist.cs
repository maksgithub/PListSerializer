using System.Collections.Generic;
using PListSerializer.Core.Attributes;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class EffectsPlist
    {
        //[PlistName()]
        public Dictionary<string, AdjustmentLayer> AdjustmentLayers { get; set; }
    }
}