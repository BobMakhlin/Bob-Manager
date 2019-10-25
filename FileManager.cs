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
        DirectoryInfo _dir = new DirectoryInfo(@"C:\Users\boris\Desktop");
        int index = 0;

        public FileManager()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }
        private void HandleKeyboard()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (index > 0)
                    {
                        index--;
                        Console.Clear();
                        Show();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (index < _dir.GetItems().ToList().Count - 1)
                    {
                        index++;
                        Console.Clear();
                        Show();
                    }
                    break;
                case ConsoleKey.Enter:
                    var items = _dir.GetItems().ToList();

                    try
                    {
                        if (items[index] is DirectoryInfo dir)
                        {
                            _dir = new DirectoryInfo(dir.FullName);
                        }
                        else if (items[index] is FileInfo file)
                        {
                            Process.Start(file.FullName);
                        }
                    }
                    catch (Exception)
                    {
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

            var items = _dir.GetItems().ToList();
            try
            {
                for (int i = index / 21 * 21; i < index / 21 * 21 + 21; i++)
                {
                    if(items[i] is DirectoryInfo dir)
                    {
                        if (i == index)
                            Console.BackgroundColor = ConsoleColor.DarkRed;

                        Console.WriteLine($"| {dir.Name.Shorten(20),-20} | " +
                           $"{dir.GetSize() / 1e+6,-16:0.000} | " +
                           $"{dir.CreationTime.ToShortDateString()} | " +
                           $"{dir.CreationTime.ToShortTimeString(),-5} |");

                        if (i == index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if(items[i] is FileInfo file)
                    {
                        if (i == index)
                            Console.BackgroundColor = ConsoleColor.DarkRed;

                        Console.WriteLine($"| {file.Name.Shorten(20),-20} | " +
                        $"{file.Length / 1e+6,-16:0.000} | " +
                        $"{file.CreationTime.ToShortDateString()} | " +
                        $"{file.CreationTime.ToShortTimeString(),-5} |");

                        if (i == index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
