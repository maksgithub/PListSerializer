using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PListNet;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PListSerializer.Core.Tests
{
    [TestClass]
    public class PListSerializerTests
    {
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
            Assert.AreEqual("API stands for Application Programming Interface.", r.answer);
        }
    }
}
