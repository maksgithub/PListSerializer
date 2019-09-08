using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class ClassWithSameTypes
    {
        public string Id { get; set; }
        public ClassWithSameTypes ClassSameType { get; set; }
        public ClassWithSameTypes ClassSameType2 { get; set; }
        public ClassWithSameTypes[] ArraySameType { get; set; }
        public ClassWithSameTypes[] ArraySameType2 { get; set; }
        public Dictionary<string, ClassWithSameTypes> DictionarySameType { get; set; }
        public Dictionary<string, ClassWithSameTypes> DictionarySameType2 { get; set; }
    }
}