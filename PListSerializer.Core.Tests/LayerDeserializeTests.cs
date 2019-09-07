using System.IO;
using System.Text;
using NUnit.Framework;
using PListNet;
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
            var layers = _deserializer.Deserialize<Layer>(node);

            Assert.IsNotNull(layers);
        }
    }
}