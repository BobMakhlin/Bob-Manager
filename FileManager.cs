using System;
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
                {
                    Pos = (63, 0)
                }
            };
        FunctionsBar[] bars = new FunctionsBar[2]
            {
                new FunctionsBar(),
                new FunctionsBar()
            };

        public FileManager()
        {
            // Set directories
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Dir = new DirectoryInfo(@"C:\");
            }

            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].Pos = (tables[0].Pos.X, tables[0].Pos.Y + Program.MaxItemsCount + 3 + i);
            }

            bars[0].AddFunction("F1", "Info");
            bars[0].AddFunction("F2", "Mkdir");
            bars[0].AddFunction("F3", "Create file");
            bars[0].AddFunction("F4", "Rename");
            bars[0].AddFunction("F5", "Update");
            bars[0].AddFunction("F6", "Copy");
            bars[0].AddFunction("F7", "Paste");
            bars[0].AddFunction("Tab", "Change disk");
            bars[0].AddFunction("Delete", "");
            bars[0].AddFunction("Enter", "Open");

            bars[1].AddFunction("Esc", "Parent");
            bars[1].AddFunction("Left", "");
            bars[1].AddFunction("Rigth", "");
            bars[1].AddFunction("Up", "");
            bars[1].AddFunction("Down", "");
            bars[1].AddFunction(Helper.CenterText("<Coming soon!>", 80, '-'), "");

            Console.WindowWidth = 124;
            Console.WindowHeight = 30 + bars.Count() - 1;

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

                    if (activeTable.Index == items.Count() - 1)
                        activeTable.Index--;

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
                        Pos = (bars[bars.Count()-1].Pos.X, bars[bars.Count() - 1].Pos.Y + 2)
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
                        Pos = (bars[bars.Count() - 1].Pos.X, bars[bars.Count() - 1].Pos.Y + 2)
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
                        Pos = (bars[bars.Count() - 1].Pos.X, bars[bars.Count() - 1].Pos.Y + 2)
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
                    activeTable.PasteItem(obj);

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.Escape:
                    var parent = Directory.GetParent(activeTable.Dir.FullName);

                    if (parent != null)
                    {
                        activeTable.Dir = parent;

                        Console.Clear();
                        Show();
                    }

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
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Draw();
            }

            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].Draw();
            }
        }
    }
}