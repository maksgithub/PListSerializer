using System.Collections.Generic;

namespace PListSerializer.Core.Tests.TestsModels
{
    public class ClassWithDictionaryAndArraySameType
    {
        public Dictionary<string, ClassWithArraySameType> DictionaryArrays { get; set; }
    }
}