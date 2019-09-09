using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PListNet.Nodes;

namespace PListSerializer.Core.Tests.Tests
{
    [TestFixture()]
    public class PListDeserializePrimitiveTypesTests
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
    }
}