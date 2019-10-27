using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BobManager
{
    class FileManager
    {
        FileTable fileTable = new FileTable();
        public FileManager()
        {
            // Set directory
            fileTable.Dir = new DirectoryInfo(@"C:\Users\boris\Desktop");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }
        private void HandleKeyboard()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    fileTable.Index--;

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.DownArrow:
                    fileTable.Index++;

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.Enter:
                    var items = fileTable.Dir.GetItems().ToList();

                    if (items[fileTable.Index] is DirectoryInfo dir)
                    {
                        DirectoryInfo d = new DirectoryInfo(dir.FullName);
                        fileTable.Dir = d;
                        fileTable.Index = 0;
                    }
                    else if (items[fileTable.Index] is FileInfo file)
                    {
                        Process.Start(file.FullName);
                    }

                    Console.Clear();
                    Show();
                    break;
            }
        }
        public void Start()
        {
            Show();

            while (true)
            {
                HandleKeyboard();
            }
        }
        private void Show()
        {
            string title = $"| {"Name",-20} | {"Size (MB)",-16} | {"Date",-10} | {"Time",-5} |";
            Console.WriteLine(title);

            string line = new string('-', title.Length);
            Console.WriteLine(line);

            var items = fileTable.Dir.GetItems().ToList();

            int start = fileTable.Index / 20 * 20;

            long size;
            for (int i = start; i < (start + 20 > items.Count() ? items.Count() : start + 20); i++)
            {
                if (items[i] is DirectoryInfo dir)
                {
                    if (i == fileTable.Index)
                        Console.BackgroundColor = ConsoleColor.DarkRed;

                    try
                    {
                        size = dir.GetSize();
                    }
                    catch (Exception)
                    {
                        size = 0;
                    }

                    Console.WriteLine($"| {dir.Name.Shorten(20),-20} | " +
                        $"{size / 1e+6,-16:0.000} | " +
                        $"{dir.CreationTime.ToShortDateString()} | " +
                        $"{dir.CreationTime.ToShortTimeString(),-5} |");

                    if (i == fileTable.Index)
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else if (items[i] is FileInfo file)
                {
                    if (i == fileTable.Index)
                        Console.BackgroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine($"| {file.Name.Shorten(20),-20} | " +
                        $"{file.Length / 1e+6,-16:0.000} | " +
                        $"{file.CreationTime.ToShortDateString()} | " +
                        $"{file.CreationTime.ToShortTimeString(),-5} |");

                    if (i == fileTable.Index)
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
            }
        }
    }
}
