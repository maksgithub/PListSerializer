using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PListNet;

namespace PListSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(Resources.PList1);
            MemoryStream stream = new MemoryStream(byteArray);
            var node = PList.Load(stream);
        }
    }
}
