using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PListNet;
using PListNet.Nodes;
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
        public void Serialize_Effect_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.Plist2);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var r = d.Deserialize<plist>(node);
            Assert.IsNotNull(r);
            Assert.AreEqual("Custom", r.group_identifier);
            Assert.AreEqual("Clarity Booster - 2018.lmp", r.kMPPresetIdentifierKey);
            Assert.AreEqual(true, r.Hidden);
            Assert.AreEqual("259F230F-A18A-489C-87FE-024B503E1F5C", r.uuid);
            Assert.IsNotNull(r.AdjustmentLayers);
            Assert.IsNotNull(r.AdjustmentLayers[0]);
            Assert.AreEqual("Normal", r.AdjustmentLayers[0].BlendModeIdentifier);

            //Assert.IsNotNull(r.question1);C:\Users\MARS\source\repos\PListSerializer\PListSerializer.Core.Tests\packages.config
            //Assert.AreEqual("4242422", r.question1.text);
            //Assert.AreEqual("4242422", r.question1.text2);
            //Assert.AreEqual("4242422", r.question1.text3);
            //Assert.AreEqual("4242422", r.question1.text4);
        }
    }

    public class plist
    {
        public string group_identifier { get; set; }
        public string kMPPresetIdentifierKey { get; set; }
        public int priority { get; set; }
        public bool Hidden { get; set; }
        public string uuid { get; set; }
        public AdjustmentLayer[] AdjustmentLayers { get; set; }
    }

    public class AdjustmentLayer
    {
        public string BlendModeIdentifier { get; set; }
    }
}