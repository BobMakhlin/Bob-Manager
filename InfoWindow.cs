/*
 * Bob Manager. Console file manager.
 * Copyright(C) 2019 Bob Makhlin

 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this program.If not, see https://www.gnu.org/licenses/.
*/

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

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape || key == ConsoleKey.F1)
                    return;
            }
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
            Console.BackgroundColor = Program.DefaultColor;
        }
    }
}
