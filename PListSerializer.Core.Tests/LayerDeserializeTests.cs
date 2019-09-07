using System.IO;
using System.Text;
using NUnit.Framework;
using PListNet;
using PListSerializer.Core.Tests.TestsModels;
using PListSerializer.Core.Tests.TestsModels.Effects;

namespace PListSerializer.Core.Tests
{
    [TestFixture]
    public class LayerDeserializeTests
    {
        private Deserializer _deserializer;

        [OneTimeSetUp]
        public void SetUp()
        {
            _deserializer = new Deserializer();
        }
        [TestCase]
        public void Deserialize_Layer_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Effects);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            Root root = _deserializer.Deserialize<Root>(node);

            Assert.IsNotNull(root);
            Assert.That(root.Layers, Has.Count.EqualTo(76));
            var layer1 = root.Layers["AccentAIExpertAdjustmentLayer"];
            Assert.That(layer1.InfoImageName, Is.EqualTo("accent_ai_enhancer"));
            Assert.That(layer1.Identifier, Is.EqualTo("AccentAIExpertAdjustmentLayer"));
            Assert.That(layer1.InfoDescription, Is.EqualTo("Smart enhancement with the help of Artificial Intelligence. This filter automatically analyzes your image, and makes the necessary adjustments to bring naturally beautiful results."));
            Assert.That(layer1.Name, Is.EqualTo("Accent AI Filter"));
            Assert.That(layer1.Effects, Has.Length.EqualTo(1));

            Assert.That(layer1.Effects[0].Identifier, Is.EqualTo("MIPLAccentAIExpertEffect"));
            var parameter = layer1.Effects[0].Parameters["Filtering Mode"];
            Assert.That(parameter.Name, Is.EqualTo("Filtering Mode"));
            Assert.That(parameter.DiscreteValues, Has.Length.EqualTo(3));

            Assert.That(parameter.DiscreteValues[0], Is.EqualTo("No histogram filtering"));
            Assert.That(parameter.DiscreteValues[1], Is.EqualTo("New"));
            Assert.That(parameter.DiscreteValues[2], Is.EqualTo("Old"));

            var layer2 = root.Layers["DevelopAdjustmentLayer"];
            Assert.That(layer2.Identifier, Is.EqualTo("DevelopAdjustmentLayer"));
            Assert.That(layer2.Sublayers[1].Identifier, Is.EqualTo("DevelopAdjustmentSubLayer"));
            Assert.That(layer2.Sublayers[1].Effects[0].Parameters["Shadows"], Is.Not.Null);
        }
    }
}