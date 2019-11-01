using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    class SwitchDriveWindow
    {
        int _index = 0;
        DriveInfo[] _drives = DriveInfo.GetDrives();

        public FileTable FileTable { get; set; }
        public void Start()
        {
            Draw();

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        if (Directory.Exists(_drives[_index].Name))
                        {
                            FileTable.Dir = new DirectoryInfo(DriveInfo.GetDrives()[_index].Name);
                            FileTable.Index = 0;
                        }
                        return;
                    case ConsoleKey.UpArrow:
                        if (_index > 0)
                            _index--;
                        Draw();
                        break;
                    case ConsoleKey.DownArrow:
                        if (_index < DriveInfo.GetDrives().Count() - 1)
                            _index++;
                        Draw();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.Tab:
                        return;
                }
            }
        }
        private void Draw()
        {
            string title = new string('-', 21);

            (int X, int Y) pos;
            pos.X = Console.BufferWidth / 2 - title.Length / 2 - 2;
            pos.Y = Console.WindowHeight / 2 - _drives.Count() - 2;

            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write($"| Name | Total size |");
            Console.SetCursorPosition(pos.X, pos.Y + 1);
            Console.Write(title);

            for (int i = 0; i < _drives.Count(); i++) 
            {
                Console.SetCursorPosition(pos.X, pos.Y + i + 2);

                if (i == _index)
                    Console.BackgroundColor = ConsoleColor.DarkRed;

                Console.Write($"| {_drives[i],-4} | {Helper.FormatSize(_drives[i].TotalSize),-10} |");

                if (i == _index)
                    Console.BackgroundColor = ConsoleColor.DarkGray;
            }

            Console.SetCursorPosition(pos.X, pos.Y + _drives.Count() + 2);
            Console.Write(title);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
    }
}
