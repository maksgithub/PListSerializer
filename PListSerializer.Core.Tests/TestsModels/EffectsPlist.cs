using System.Collections.Generic;
using PListSerializer.Core.Attributes;
using PListSerializer.Core.Tests.TestsModels.Effects;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class EffectsPlist
    {
        public Dictionary<string, Layer> AdjustmentLayers { get; set; }
    }
}