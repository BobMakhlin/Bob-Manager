using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class Program
    {
        public const int MaxItemsCount = 20;
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            fileManager.Start();
        }
    }
}
