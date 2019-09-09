using System.IO;
using System.Text;
using NUnit.Framework;
using PListNet;
using PListNet.Nodes;
using PListSerializer.Core.Tests.TestsModels;

namespace PListSerializer.Core.Tests.Tests
{
    [TestFixture]
    public class PListDeserializerTests
    {
        private Deserializer _deserializer;

        [OneTimeSetUp]
        public void SetUp()
        {
            _deserializer = new Deserializer();
        }

        [TestCase]
        public void Deserialize_BigObject_Tests()
        {
            var node = new DictionaryNode();
            var arrayNode = new ArrayNode();
            var dictionaryNode = new DictionaryNode();
            node.Add("Array0", arrayNode);
            node.Add("Array1", arrayNode);
            node.Add("Array2", arrayNode);
            node.Add("List0", arrayNode);
            node.Add("List1", arrayNode);
            node.Add("List2", arrayNode);

            node.Add("Ints1", arrayNode);

            node.Add("Dictionary0", dictionaryNode);
            node.Add("Dictionary1", dictionaryNode);
            node.Add("Dictionary2", dictionaryNode);
            node.Add("Dictionary3", dictionaryNode);

            node.Add("DictionarySameType", dictionaryNode);
            node.Add("DictionarySameType2", dictionaryNode);
            node.Add("DictionarySameType3", dictionaryNode);

            var res = _deserializer.Deserialize<BigObject>(node);
            Assert.IsNotNull(res.Array0);
            Assert.IsNotNull(res.Array1);
            Assert.IsNotNull(res.Array2);

            Assert.IsNotNull(res.Dictionary0);
            Assert.IsNotNull(res.Dictionary1);
            Assert.IsNotNull(res.Dictionary2);
            Assert.IsNotNull(res.Dictionary3);

            Assert.IsNotNull(res.Ints1);
            Assert.IsNotNull(res.List0);
            Assert.IsNotNull(res.List1);
            Assert.IsNotNull(res.List2);
        }

        [TestCase]
        public void Deserialize_Class_With_Properties_Same_Test()
        {
            var node = new DictionaryNode();
            var arrayNode = new ArrayNode();
            var dictionaryNode = new DictionaryNode();
            node.Add("ArraySameType", arrayNode);
            node.Add("ArraySameType2", arrayNode);
            node.Add("ArraySameType3", arrayNode);

            node.Add("List1", arrayNode);
            node.Add("List2", arrayNode);
            node.Add("List3", arrayNode);

            node.Add("ClassSameType", dictionaryNode);
            node.Add("ClassSameType2", dictionaryNode);
            node.Add("ClassSameType3", dictionaryNode);

            node.Add("DictionarySameType", dictionaryNode);
            node.Add("DictionarySameType2", dictionaryNode);
            node.Add("DictionarySameType3", dictionaryNode);

            var res = _deserializer.Deserialize<ClassWithSameTypes>(node);
            Assert.IsNotNull(res.ArraySameType);
            Assert.IsNotNull(res.ArraySameType2);
            Assert.IsNotNull(res.ArraySameType);
            Assert.IsNotNull(res.ClassSameType);
            Assert.IsNotNull(res.ClassSameType2);
            Assert.IsNotNull(res.ClassSameType3);
            Assert.IsNotNull(res.DictionarySameType);
            Assert.IsNotNull(res.DictionarySameType2);

            Assert.IsNotNull(res.List1);
            Assert.IsNotNull(res.List2);
            Assert.IsNotNull(res.List3);
        }

        [TestCase]
        public void Deserialize_Effect_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Plist2);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var r = _deserializer.Deserialize<RootPList>(node);
            Assert.IsNotNull(r);
            Assert.AreEqual("Custom", r.GroupIdentifier);
            Assert.AreEqual("Clarity Booster - 2018.lmp", r.PresetIdentifierKey);
            Assert.AreEqual(true, r.Hidden);
            Assert.AreEqual("259F230F-A18A-489C-87FE-024B503E1F5C", r.Id);
            Assert.IsNotNull(r.AdjustmentLayers);
            Assert.IsNotNull(r.AdjustmentLayers[0]);
        }

        [TestCase]
        public void Serialize_EffectsInfo_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Plist3);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var r = _deserializer.Deserialize<EffectsPlist>(node);
            Assert.IsNotNull(r.AdjustmentLayers);
            var adjustmentLayer = r.AdjustmentLayers["DevelopAdjustmentLayer"];

            Assert.IsNotNull(adjustmentLayer);
            Assert.AreEqual("raw_dev2", adjustmentLayer.InfoImageName);
            Assert.AreEqual("1", adjustmentLayer.Identifier);
            Assert.IsNotNull(adjustmentLayer.Sublayers);
            Assert.That(adjustmentLayer.Sublayers, Has.Length.EqualTo(4));
            Assert.IsNotNull(adjustmentLayer.Sublayers[0].EffectsIMG);
        }
    }
}