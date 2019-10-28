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
        private static string FormatFileSize(long length)
        {
            if (length < Math.Pow(2, 10))
                return $"{length:0.000} b";
            if (length >= Math.Pow(2, 10) && length < Math.Pow(2, 20))
                return $"{length / (double)1000:0.000} kb";
            if (length >= Math.Pow(2, 20) && length < Math.Pow(2, 30))
                return $"{length / 1e+6:0.000} mb";
            if (length >= Math.Pow(2, 30) && length < Math.Pow(2, 40))
                return $"{length / 1e+9:0.000} gb";
            throw new FormatException("Bad size");
        }
        public void Draw((int X, int Y) pos)
        {

            string title = $"| {"Name",-20} | {"Size (MB)",-12} | {"Date",-10} | {"Time",-5} |";

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(title);

            string line = new string('-', title.Length);

            Console.SetCursorPosition(pos.X, pos.Y + 1);
            Console.Write(line);

            var items = Dir.GetItems().ToList();

            int start = Index / 20 * 20;
            for (int i = start; i < (start + 20 > items.Count() ? items.Count() : start + 20); i++)
            {
                try
                {
                    if (items[i] is DirectoryInfo dir)
                    {
                        if (i == Index)
                            Console.BackgroundColor = SelectedItemColor;

                        Console.SetCursorPosition(pos.X, pos.Y + (i - start + 2));
                        Console.Write($"| {dir.Name.Shorten(20),-20} | " +
                            $"{"<DIR>",-12} | " +
                            $"{dir.CreationTime.ToShortDateString()} | " +
                            $"{dir.CreationTime.ToShortTimeString(),-5} |");

                        if (i == Index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (items[i] is FileInfo file)
                    {
                        if (i == Index)
                            Console.BackgroundColor = SelectedItemColor;

                        Console.SetCursorPosition(pos.X, pos.Y + (i - start + 2));
                        Console.Write($"| {file.Name.Shorten(20),-20} | " +
                            $"{FormatFileSize(file.Length),-12:0.000} | " +
                            $"{file.CreationTime.ToShortDateString()} | " +
                            $"{file.CreationTime.ToShortTimeString(),-5} |");

                        if (i == Index)
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}