using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels.Effects
{
    public class Effect
    {
        public string Identifier { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }
    }
}