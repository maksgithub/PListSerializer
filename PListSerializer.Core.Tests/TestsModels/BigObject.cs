using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class BaseBigObject
    {
        public EmptyClass[] Array0 { get; set; }
        public Dictionary<string, EmptyClass> Dictionary0 { get; set; }
        public List<EmptyClass> List0 { get; set; }
    }

    public class BigObject : BaseBigObject
    {
        public int[] Ints1 { get; set; }
        public string[] Strings { get; set; }

        public EmptyClass[] Array1 { get; set; }
        public EmptyClass[] Array2 { get; set; }

        public Dictionary<string, EmptyClass> Dictionary1 { get; set; }
        public Dictionary<string, EmptyClass> Dictionary2 { get; set; }
        public Dictionary<string, string> Dictionary3 { get; set; }

        public List<EmptyClass> List1 { get; set; }
        public List<EmptyClass> List2 { get; set; }
    }
}