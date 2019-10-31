using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class FileTable
    {
        private int _index = 0;

        public DirectoryInfo Dir { get; set; }
        public ConsoleColor SelectedItemColor { get; set; } = ConsoleColor.DarkRed;
        public (int X, int Y) Pos { get; set; }

        private string GetInfo(FileSystemInfo item)
        {
            string info = string.Empty;

            if (item is DirectoryInfo dir)
            {
                info = $"| {dir.Name.Shorten(20),-20} | " +
                    $"{"<DIR>",-12} | " +
                    $"{dir.CreationTime.ToShortDateString()} | " +
                    $"{dir.CreationTime.ToShortTimeString(),-5} |";
            }
            else if (item is FileInfo file)
            {
                info = $"| {file.Name.Shorten(20),-20} | " +
                    $"{Helper.FormatSize(file.Length),-12:0.000} | " +
                    $"{file.CreationTime.ToShortDateString()} | " +
                    $"{file.CreationTime.ToShortTimeString(),-5} |";
            }

            return info;
        }

        public int Index
        {
            get => _index;
            set
            {
                int itemsCount = Dir.GetItems().ToList().Count();

                if (value >= 0 && value < itemsCount)
                    _index = value;
            }
        }

        public void Draw()
        {
            string title = $"| {"Name",-20} | {"Size (MB)",-12} | {"Date",-10} | {"Time",-5} |";

            Console.SetCursorPosition(Pos.X, Pos.Y);
            Console.Write(title);

            string line = new string('-', title.Length);

            Console.SetCursorPosition(Pos.X, Pos.Y + 1);
            Console.Write(line);

            var items = Dir.GetItems().ToList();

            int start = Index / Program.MaxItemsCount * Program.MaxItemsCount;
            for (int i = start; i < (start + Program.MaxItemsCount > items.Count() ? items.Count() : start + Program.MaxItemsCount); i++)
            {
                try
                {
                    if (items[i] is DirectoryInfo dir)
                    {
                        if (i == Index)
                            Console.BackgroundColor = SelectedItemColor;

                        Console.SetCursorPosition(Pos.X, Pos.Y + (i - start + 2));
                        Console.Write(GetInfo(dir));

                        if (i == Index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (items[i] is FileInfo file)
                    {
                        if (i == Index)
                            Console.BackgroundColor = SelectedItemColor;

                        Console.SetCursorPosition(Pos.X, Pos.Y + (i - start + 2));
                        Console.Write(GetInfo(file));

                        if (i == Index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void DrawSelectedItem(List<FileSystemInfo> items)
        {
            Console.SetCursorPosition(Pos.X, Pos.Y + (Index % Program.MaxItemsCount) + 2);
            Console.BackgroundColor = SelectedItemColor;
            Console.Write(GetInfo(items[Index]));
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void RedrawUp()
        {
            int oldY = Console.CursorTop;

            var items = Dir.GetItems().ToList();

            DrawSelectedItem(items);

            if (Index + 1 < items.Count())
            {
                Console.SetCursorPosition(Pos.X, Pos.Y + (Index % Program.MaxItemsCount) + 3);
                Console.Write(GetInfo(items[Index + 1]));
            }

            Console.SetCursorPosition(0, Program.MaxItemsCount + 3);
        }
        public void RedrawDown()
        {
            var items = Dir.GetItems().ToList();

            DrawSelectedItem(items);

            if (Index - 1 >= 0)
            {
                Console.SetCursorPosition(Pos.X, Pos.Y + (Index % Program.MaxItemsCount) + 1);
                Console.Write(GetInfo(items[Index - 1]));
            }

            Console.SetCursorPosition(0, Program.MaxItemsCount + 3);
        }
    }
}