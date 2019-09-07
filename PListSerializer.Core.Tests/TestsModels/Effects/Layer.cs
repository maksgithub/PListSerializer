namespace PListSerializer.Core.Tests.TestsModels.Effects
{
    public class Layer
    {
        public string BlendModeIdentifier { get; set; }
        public string InfoImageName { get; set; }
        public string Identifier { get; set; }
        public Layer[] Sublayers { get; set; }

        public Effect[] EffectsIMG { get; set; }
    }
}