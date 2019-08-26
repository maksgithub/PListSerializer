using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PListNet.Nodes;
using PListSerializer.Core.Extensions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PListSerializer.Core.Tests
{
    [TestClass]
    class PListExtensionsTests
    {
        private DictionaryNode _rootDictNode;

        [OneTimeSetUp] 
        public void SetUp()
        {
            _rootDictNode = new DictionaryNode
            {
                {"1", new ArrayNode()},
                {"2", new DictionaryNode()},
                {"3", new FillNode()},
                {"4", new NullNode()},
                {"5", new BooleanNode()},
                {"6", new DataNode()},
                {"7", new DateNode()},
                {"8", new IntegerNode()},
                {"9", new RealNode()},
                {"10", new StringNode()}
            };
        }

        [TestCase]
        public void GetArrayNode_Test()
        {
           var r = _rootDictNode.GetValue<ArrayNode>("1");
           Assert.IsNotNull(r);
           Assert.IsInstanceOfType(r, typeof(ArrayNode));
        }

        [TestCase]
        public void GetBoolNode_Test()
        {
           var r = _rootDictNode.GetValue<bool>("1");
           Assert.IsInstanceOfType(r, typeof(bool));
        }
    }
}
