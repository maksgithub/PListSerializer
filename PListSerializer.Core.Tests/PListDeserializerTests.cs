using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using PListNet;
using PListNet.Nodes;
using PListSerializer.Core.Tests.TestsModels;

namespace PListSerializer.Core.Tests
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
        public void Deserialize_Array_Test()
        {
            var node = new DictionaryNode();
            var subNode = new ArrayNode();
            node.Add("Array", subNode);
            var res = _deserializer.Deserialize<ClassWithArray>(node);
            Assert.IsNotNull(res.Array);
        }

        [TestCase]
        public void Deserialize_Array_Dictionary()
        {
            var node = new DictionaryNode();
            var subNode = new DictionaryNode();
            node.Add("Dictionary", subNode);
            var res = _deserializer.Deserialize<ClassWithDictionary>(node);
            Assert.IsNotNull(res.Dictionary);
        }

        [TestCase]
        public void Recursion_SubclassArray_Test()
        {
            var node = new DictionaryNode();
            var subNode = new ArrayNode();
            var subNode2 = new ArrayNode();
            var subNode3 = new DictionaryNode();
            var subNode4 = new DictionaryNode();
            var subNode5 = new DictionaryNode();
            var subNode6 = new DictionaryNode();
            node.Add("ArraySameType", subNode);
            node.Add("ArraySameType2", subNode2);
            node.Add("ClassSameType", subNode3);
            node.Add("ClassSameType2", subNode4);
            node.Add("DictionarySameType", subNode5);
            node.Add("DictionarySameType2", subNode6);

            var res = _deserializer.Deserialize<ClassWithSameTypes>(node);
            Assert.IsNotNull(res.ArraySameType);
            Assert.IsNotNull(res.ArraySameType2);
            Assert.IsNotNull(res.ClassSameType);
            Assert.IsNotNull(res.ClassSameType2);
            Assert.IsNotNull(res.DictionarySameType);
            Assert.IsNotNull(res.DictionarySameType2);
        }

        [TestCase]
        public void Recursion_SubclassDictionary_Test()
        {
            var node = new DictionaryNode();
            var subNode = new DictionaryNode();
            node.Add("DictionarySameType", subNode);
            var res = _deserializer.Deserialize<ClassWithDictionarySameType>(node);
            Assert.IsNotNull(res.DictionarySameType);
        }

        [TestCase(42)]
        [TestCase(-13423)]
        [TestCase(0)]
        public void Deserialize_Int_Test(int source)
        {
            var node = new IntegerNode(source);
            var res = _deserializer.Deserialize<int>(node);
            Assert.That(res, Is.TypeOf<int>());
            Assert.AreEqual(source, res);
        }

        [TestCase(42)]
        [TestCase(-13423)]
        [TestCase(0)]
        public void Deserialize_Long_Test(long source)
        {
            var node = new IntegerNode(source);
            var res = _deserializer.Deserialize<long>(node);
            Assert.That(res, Is.TypeOf<long>());
            Assert.AreEqual(source, res);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Deserialize_Bool_Test(bool source)
        {
            var node = new BooleanNode(source);
            var res = _deserializer.Deserialize<bool>(node);
            Assert.That(res, Is.TypeOf<bool>());
            Assert.AreEqual(source, res);
        }

        [TestCase("String_42")]
        [TestCase("String_42grtryrthytrytryrt")]
        public void Deserialize_String_Test(string source)
        {
            var node = new StringNode(source);
            var res = _deserializer.Deserialize<string>(node);
            Assert.That(res, Is.TypeOf<string>());
            Assert.AreEqual(source, res);
        }

        [TestCase()]
        public void Deserialize_String_Test()
        {
            DateTime source = DateTime.MaxValue;
            var node = new DateNode(source);
            var res = _deserializer.Deserialize<DateTime>(node);
            Assert.That(res, Is.TypeOf<DateTime>());
            Assert.AreEqual(source, res);
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