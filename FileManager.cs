﻿using System;
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
        FileTable[] tables = new FileTable[2]
            {
                new FileTable(),
                new FileTable()
            };

        enum ActiveTable
        {
            FirstTableActive,
            SecondTableActive
        }

        ActiveTable activeTable = ActiveTable.FirstTableActive;

        public FileManager()
        {
            // Set directory
            tables[0].Dir = new DirectoryInfo(@"C:\Users\boris\Desktop");
            tables[1].Dir = new DirectoryInfo(@"C:\Users\boris\Desktop");
            tables[1].SelectedItemColor = ConsoleColor.DarkGreen;

            Console.WindowWidth = 125;

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

        private void HandleKeyboard()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    tables[(int)activeTable].Index--;

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.DownArrow:
                    tables[(int)activeTable].Index++;

                    Console.Clear();
                    Show();
                    break;
                case ConsoleKey.LeftArrow:
                    if(activeTable != ActiveTable.FirstTableActive)
                    {
                        activeTable = ActiveTable.FirstTableActive;

                        Console.Clear();
                        Show();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if(activeTable != ActiveTable.SecondTableActive)
                    {
                        activeTable = ActiveTable.SecondTableActive;

                        Console.Clear();
                        Show();
                    }
                    break;
                case ConsoleKey.Enter:
                    var items = tables[(int)activeTable].Dir.GetItems().ToList();

                    if (items[tables[(int)activeTable].Index] is DirectoryInfo dir)
                    {
                        DirectoryInfo d = new DirectoryInfo(dir.FullName);
                        tables[(int)activeTable].Dir = d;
                        tables[(int)activeTable].Index = 0;
                    }
                    else if (items[tables[(int)activeTable].Index] is FileInfo file)
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
            tables[0].Draw((0, 0));
            tables[1].Draw((61, 0));
        }
    }
}
