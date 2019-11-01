using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class InfoWindow
    {
        public static void ShowInfo(FileSystemInfo item)
        {
            Draw(item);

            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }
        private static void Draw(FileSystemInfo item)
        {
            string title = new string('-', 42);

            string[] info = new string[7];

            info[0] = info[6] = title;
            info[1] = $"| Name: {item.Name.Shorten(32),-32} |";

            info[2] = "| Size: ";
            if (item is DirectoryInfo dir)
            {
                info[2] += $"{Helper.FormatSize(dir.GetSize()),-32} |";
            }
            else if (item is FileInfo file)
            {
                info[2] += $"{Helper.FormatSize(file.Length),-32} |";
            }

            info[3] = $"| Attributes: {item.Attributes.ToString().Shorten(26),-26} |";
            info[4] = $"| Creation time: {item.CreationTime,-23} |";
            info[5] = $"| Last access time: {item.LastAccessTime,-20} |";

            (int X, int Y) pos;
            pos.X = Console.BufferWidth / 2 - title.Length / 2;
            pos.Y = Console.WindowHeight / 2 - info.Length + 1;

            Console.BackgroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < info.Length; i++)
            {
                Console.SetCursorPosition(pos.X, pos.Y + i);
                Console.Write(info[i]);
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
    }
}
