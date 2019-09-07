using System.IO;
using System.Text;
using NUnit.Framework;
using PListNet;
using PListSerializer.Core.Tests.TestsModels;

namespace PListSerializer.Core.Tests
{
    [TestFixture]
    public class PListCollectionsSerializeTests
    {
        [TestCase]
        public void Recursion_Deep_SubclassArray_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.PList4);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var res = d.Deserialize<ClassWithArraySameType>(node);
            Assert.IsNotNull(res.ArraySameType);
            Assert.IsNotNull("-1", res.Id);
            Assert.AreEqual("0", res.ArraySameType[0].Id);
            Assert.AreEqual("1", res.ArraySameType[1].Id);
            Assert.AreEqual("2", res.ArraySameType[2].Id);
            Assert.AreEqual("3", res.ArraySameType[3].Id);
            Assert.AreEqual("4", res.ArraySameType[4].Id);
            Assert.AreEqual("5", res.ArraySameType[5].Id);

            Assert.AreEqual("00", res.ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("01", res.ArraySameType[0].ArraySameType[1].Id);
            Assert.AreEqual("000", res.ArraySameType[0].ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("001", res.ArraySameType[0].ArraySameType[0].ArraySameType[1].Id);
        }

        [TestCase]
        public void Recursion_Deep_SubclassDictionaryAndArray_Test()
        {
            var byteArray = Encoding.ASCII.GetBytes(Resources.PList5);
            var stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
            var d = new Deserializer();
            var root = d.Deserialize<ClassWithDictionaryAndArraySameType>(node);
            Assert.IsNotNull(root.DictionaryArrays);
            var array1 = root.DictionaryArrays["Arrays1"];

            Assert.IsNotNull(array1);
            Assert.IsNotNull(array1.Id);
            Assert.IsNotNull(array1.ArraySameType);
            Assert.AreEqual("0", array1.ArraySameType[0].Id);
            Assert.AreEqual("1", array1.ArraySameType[1].Id);
            Assert.AreEqual("2", array1.ArraySameType[2].Id);
            Assert.AreEqual("3", array1.ArraySameType[3].Id);
            Assert.AreEqual("4", array1.ArraySameType[4].Id);
            Assert.AreEqual("5", array1.ArraySameType[5].Id);

            Assert.AreEqual("00", array1.ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("01", array1.ArraySameType[0].ArraySameType[1].Id);
            Assert.AreEqual("000", array1.ArraySameType[0].ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("001", array1.ArraySameType[0].ArraySameType[0].ArraySameType[1].Id);
        
            var array2 = root.DictionaryArrays["Arrays2"];

            Assert.IsNotNull(array2);
            Assert.IsNotNull(array2.Id);
            Assert.IsNotNull(array2.ArraySameType);
            Assert.AreEqual("0", array2.ArraySameType[0].Id);
            Assert.AreEqual("1", array2.ArraySameType[1].Id);
            Assert.AreEqual("2", array2.ArraySameType[2].Id);
            Assert.AreEqual("3", array2.ArraySameType[3].Id);
            Assert.AreEqual("4", array2.ArraySameType[4].Id);
            Assert.AreEqual("5", array2.ArraySameType[5].Id);

            Assert.AreEqual("00", array2.ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("01", array2.ArraySameType[0].ArraySameType[1].Id);
            Assert.AreEqual("000", array2.ArraySameType[0].ArraySameType[0].ArraySameType[0].Id);
            Assert.AreEqual("001", array2.ArraySameType[0].ArraySameType[0].ArraySameType[1].Id);
        }
    }
}