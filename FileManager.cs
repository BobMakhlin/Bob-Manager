﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Threading;

namespace BobManager
{
    class FileManager
    {
        FileTable[] tables = new FileTable[2]
            {
                new FileTable()
                {
                    Active = true
                },
                new FileTable()
            };

        public FileManager()
        {
            // Set directory
            tables[0].Dir = new DirectoryInfo(@"C:\");
            tables[1].Dir = new DirectoryInfo(@"C:\");

            Console.WindowWidth = 124;
            Console.WindowHeight = 31;

            Console.BackgroundColor = Program.DefaultColor;
            Console.Clear();
        }

        private void HandleKeyboard()
        {
            var activeTable = tables.ToList().Find(x => x.Active == true);
            var items = activeTable.Dir.GetItems().ToList();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    activeTable.Index--;

                    if ((activeTable.Index + 1) % Program.MaxItemsCount == 0)
                    {
                        Console.Clear();
                        Show();
                    }
                    else
                    {
                        activeTable.RedrawUp();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    activeTable.Index++;

                    if (activeTable.Index % Program.MaxItemsCount == 0)
                    {
                        Console.Clear();
                        Show();
                    }
                    else
                    {
                        activeTable.RedrawDown();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (!tables[0].Active)
                    {
                        tables[0].Active = true;
                        tables[1].Active = false;

                        FileTable.RedrawLeft(tables[0], tables[1]);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (!tables[1].Active)
                    {
                        tables[1].Active = true;
                        tables[0].Active = false;

                        FileTable.RedrawRight(tables[0], tables[1]);
                    }
                    break;
                case ConsoleKey.Enter:
                    if (items[activeTable.Index] is DirectoryInfo dir)
                    {
                        DirectoryInfo d = new DirectoryInfo(dir.FullName);
                        activeTable.Dir = d;
                        activeTable.Index = 0;

                        Console.Clear();
                        Show();
                    }
                    else if (items[activeTable.Index] is FileInfo file)
                    {
                        Process.Start(file.FullName);
                    }
                    break;
                case ConsoleKey.Tab:
                    SwitchDriveWindow driveWindow = new SwitchDriveWindow()
                    {
                        FileTable = activeTable
                    };
                    driveWindow.Start();

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.Delete:
                    activeTable.DeleteSelectedItem();
                    activeTable.Index = 0;

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F5:
                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F1:
                    InfoWindow.ShowInfo(items[activeTable.Index]);

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F2:
                    InputWindow inputWindow = new InputWindow()
                    {
                        Text = "Directory name",
                        Pos = ((tables[0].Pos.X, tables[0].Pos.Y + Program.MaxItemsCount + 6))
                    };
                    inputWindow.Draw();
                    activeTable.CreateDirectory(inputWindow.GetString());

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F3:
                    inputWindow = new InputWindow()
                    {
                        Text = "File name",
                        Pos = ((tables[0].Pos.X, tables[0].Pos.Y + Program.MaxItemsCount + 6))
                    };
                    inputWindow.Draw();
                    activeTable.CreateFile(inputWindow.GetString());

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F4:
                    inputWindow = new InputWindow()
                    {
                        Text = "New name",
                        Pos = ((tables[0].Pos.X, tables[0].Pos.Y + Program.MaxItemsCount + 6))
                    };
                    inputWindow.Draw();
                    activeTable.RenameSelectedItem(inputWindow.GetString());

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.F6:
                    Clipboard.SetData(typeof(FileSystemInfo).FullName, items[activeTable.Index]);
                    break;
                case ConsoleKey.F7:
                    var obj = (FileSystemInfo)Clipboard.GetData(typeof(FileSystemInfo).FullName);

                    if (obj is DirectoryInfo directory)
                    {
                        string path = $"{activeTable.Dir.FullName}\\{obj.Name}";
                        directory.CopyTo(path);

                        int index = activeTable.Dir.GetItems().ToList().FindIndex(x => x.FullName == path);
                        if (index != -1)
                            activeTable.Index = index;
                    }
                    else if (obj is FileInfo file)
                    {
                        string path = $"{activeTable.Dir.FullName}\\{obj.Name}";

                        file.CopyTo(path);

                        int index = activeTable.Dir.GetItems().ToList().FindIndex(x => x.FullName == path);
                        if (index != -1)
                            activeTable.Index = index;
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
                try
                {
                    HandleKeyboard();
                }
                catch (Exception)
                {
                }
            }
        }
        private void Show()
        {
            tables[0].Draw();

            tables[1].Pos = (63, 0);
            tables[1].Draw();

            FileTable.DrawFunctionsBar((tables[0].Pos.X, tables[0].Pos.Y + Program.MaxItemsCount + 3));
        }
    }
}