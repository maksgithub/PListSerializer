using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PListSerializer
{
    static class PlistReader
    {
        public static string ToString(this byte[] pListBytes)
        {
            var result = Encoding.UTF8.GetString(pListBytes);
            return result;
        }
    }
}
