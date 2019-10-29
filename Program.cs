using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            fileManager.Start();

            //for (int i = 0; i < 40; i++)
            //{
            //    Console.WriteLine($"{i} -> {i % 20}");
            //}
        }
    }
}
