using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class FileManager
    {
        DirectoryInfo _dir = new DirectoryInfo(@"C:\Users\boris\Desktop");
        //int index = 0;

        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Show();
        }
        private void Show()
        {
            string title = $"| {"Name",-26} | {"Size (MB)",-16} | {"Date",-10} | {"Time",-5} |";
            Console.WriteLine(title);

            string line = new string('-', title.Length);
            Console.WriteLine(line);

            try
            {
                foreach (var dir in _dir.GetDirectories())
                {
                    Console.WriteLine($"| {dir.Name,-26} | {MyDirectory.GetDirectorySize(dir.FullName) / (double)1e+6,-16:0.000} | {dir.CreationTime.ToShortDateString()} | {dir.CreationTime.ToShortTimeString(),-5} |");
                }

                Console.WriteLine(line);

                foreach (var file in _dir.GetFiles())
                {
                    Console.WriteLine($"| {file.Name,-26} | {file.Length,-16:0.000} | {file.CreationTime.ToShortDateString()} | {file.CreationTime.ToShortTimeString(),-5} |");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
