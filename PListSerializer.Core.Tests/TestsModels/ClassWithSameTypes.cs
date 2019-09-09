using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class BaseClassWithSameTypes
    {
        public ClassWithSameTypes ClassSameType3 { get; set; }
        public ClassWithSameTypes[] ArraySameType3 { get; set; }
        public Dictionary<string, ClassWithSameTypes> DictionarySameType3 { get; set; }
        public List<ClassWithSameTypes> List3 { get; set; }
    }

    public class ClassWithSameTypes : BaseClassWithSameTypes
    {
        public string Id { get; set; }
        public ClassWithSameTypes ClassSameType { get; set; }
        public ClassWithSameTypes ClassSameType2 { get; set; }
        public ClassWithSameTypes[] ArraySameType { get; set; }
        public ClassWithSameTypes[] ArraySameType2 { get; set; }
        public Dictionary<string, ClassWithSameTypes> DictionarySameType { get; set; }
        public Dictionary<string, ClassWithSameTypes> DictionarySameType2 { get; set; }

        public List<ClassWithSameTypes> List1 { get; set; }
        public List<ClassWithSameTypes> List2 { get; set; }
    }
}