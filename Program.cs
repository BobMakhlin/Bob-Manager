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
        public const ConsoleColor DefaultColor = ConsoleColor.DarkBlue;
        [STAThread]
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            fileManager.Start();
        }
    }
}
