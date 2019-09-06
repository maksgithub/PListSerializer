using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PListNet;
using PListNet.Nodes;
using PListSerializer.Core.Tests.TestsModels;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PListSerializer.Core.Tests
{
    [TestClass]
    public class PListSerializerTests
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
            node.Add("ArraySameType", subNode);
            var res = _deserializer.Deserialize<ClassWithArraySameType>(node);
            Assert.IsNotNull(res.ArraySameType);
        }

        [TestCase]
        public void Recursion_Subclass_Test()
        {
            var node = new DictionaryNode();
            var subNode = new ArrayNode();
            node.Add("SameClass", subNode);
            var res = _deserializer.Deserialize<ClassWithClassSameType>(node);
            Assert.IsNotNull(res.SameClass);
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
            Assert.IsInstanceOfType(res, typeof(int));
            Assert.AreEqual(source, res);
        }

        [TestCase(42)]
        [TestCase(-13423)]
        [TestCase(0)]
        public void Deserialize_Long_Test(long source)
        {
            var node = new IntegerNode(source);
            var res = _deserializer.Deserialize<long>(node);
            Assert.IsInstanceOfType(res, typeof(long));
            Assert.AreEqual(source, res);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Deserialize_Bool_Test(bool source)
        {
            var node = new BooleanNode(source);
            var res = _deserializer.Deserialize<bool>(node);
            Assert.IsInstanceOfType(res, typeof(bool));
            Assert.AreEqual(source, res);
        }

        [TestCase("String_42")]
        [TestCase("String_42grtryrthytrytryrt")]
        public void Deserialize_String_Test(string source)
        {
            var node = new StringNode(source);
            var res = _deserializer.Deserialize<string>(node);
            Assert.IsInstanceOfType(res, typeof(string));
            Assert.AreEqual(source, res);
        }

        public void Deserialize_String_Test()
        {
            DateTime source = DateTime.MaxValue;
            var node = new DateNode(source);
            var res = _deserializer.Deserialize<DateTime>(node);
            Assert.IsInstanceOfType(res, typeof(DateTime));
            Assert.AreEqual(source, res);
        }

        [TestCase]
        public void Serialize_quiz_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.PList1);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var r = d.Deserialize<question>(node);
            Assert.IsNotNull(r);
            Assert.AreEqual("What does 'API' stand for?", r.text);

            Assert.IsNotNull(r.question1);
            Assert.AreEqual("4242422", r.question1.text);
            Assert.AreEqual("4242422", r.question1.text2);
            Assert.AreEqual("4242422", r.question1.text3);
            Assert.AreEqual("4242422", r.question1.text4);
        }

        [TestCase]
        public void Deserialize_Effect_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Plist2);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var r = d.Deserialize<Plist>(node);
            Assert.IsNotNull(r);
            Assert.AreEqual("Custom", r.GroupIdentifier);
            Assert.AreEqual("Clarity Booster - 2018.lmp", r.PresetIdentifierKey);
            Assert.AreEqual(true, r.Hidden);
            Assert.AreEqual("259F230F-A18A-489C-87FE-024B503E1F5C", r.Id);
            Assert.IsNotNull(r.AdjustmentLayers);
            Assert.IsNotNull(r.AdjustmentLayers[0]);
            //Assert.AreEqual("Normal", r.AdjustmentLayers[0].BlendModeIdentifier);
        }

        [TestCase]
        public void Serialize_EffectsInfo_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Plist3);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var r = d.Deserialize<EffectsPlist>(node);
            Assert.IsNotNull(r.AdjustmentLayers);
            var adjustmentLayer = r.AdjustmentLayers["DevelopAdjustmentLayer"];

            Assert.IsNotNull(adjustmentLayer);
            //Assert.AreEqual("raw_dev2", adjustmentLayer.InfoImageName);
            //Assert.AreEqual("DevelopAdjustmentLayer", adjustmentLayer.Identifier);
            Assert.IsNotNull(adjustmentLayer.Sublayers);
            Assert.AreEqual(4, adjustmentLayer.Sublayers.Length);
            //Assert.IsNotNull("LensCorrection", adjustmentLayer.Sublayers[0].EffectsIMG);
        }
    }
}