using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class ClassWithDictionarySameType
    {
        public Dictionary<string, ClassWithDictionarySameType> DictionarySameType { get; set; }
    }
}