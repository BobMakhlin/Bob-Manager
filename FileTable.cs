﻿using System;
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
        public (int X, int Y) Pos { get; set; }
        public bool Active { get; set; } = false;
        public ConsoleColor ActiveItemColor { get; set; } = ConsoleColor.DarkRed;
        public ConsoleColor InactiveItemColor { get; set; } = ConsoleColor.DarkGreen;

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
            string title = $"| {Helper.CenterText("Name", 22)} | {Helper.CenterText("Size ", 11)} | {Helper.CenterText("Date", 10)} | {"Time",-5} |";

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
                            Console.BackgroundColor = Active ? ActiveItemColor : InactiveItemColor;

                        Console.SetCursorPosition(Pos.X, Pos.Y + (i - start + 2));
                        Console.Write(Helper.GetItemInfo(dir));

                        if (i == Index)
                            Console.BackgroundColor = Program.DefaultColor;
                    }
                    else if (items[i] is FileInfo file)
                    {
                        if (i == Index)
                            Console.BackgroundColor = Active ? ActiveItemColor : InactiveItemColor;

                        Console.SetCursorPosition(Pos.X, Pos.Y + (i - start + 2));
                        Console.Write(Helper.GetItemInfo(file));

                        if (i == Index)
                            Console.BackgroundColor = Program.DefaultColor;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public static void DrawFunctionsBar((int X, int Y) pos)
        {
            var functions = new Dictionary<string, string>();

            functions.Add("Left Ar", "");
            functions.Add("Rigth Ar", "");
            functions.Add("Up Ar", "");
            functions.Add("Down Ar", "");
            functions.Add("Enter", "Open");
            functions.Add("Tab", "Change drive");
            functions.Add("Delete", "");
            functions.Add("F1", "Info");
            functions.Add("F5", "Update");
            functions.Add("F6", "Copy");
            functions.Add("F7", "Paste");

            Console.SetCursorPosition(pos.X, pos.Y);

            Console.Write("| ");
            foreach (var function in functions)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(function.Key);
                Console.ForegroundColor = ConsoleColor.Gray;

                if (function.Value != string.Empty)
                {
                    Console.Write($" {function.Value}");
                }
                Console.Write(" | ");
            }
        }

        private void DrawItem(List<FileSystemInfo> items, int index, ConsoleColor color = Program.DefaultColor)
        {
            Console.SetCursorPosition(Pos.X, Pos.Y + (index % Program.MaxItemsCount) + 2);

            if(color != Program.DefaultColor)
                Console.BackgroundColor = color;

            Console.Write(Helper.GetItemInfo(items[index]));

            if (color != Program.DefaultColor)
                Console.BackgroundColor = Program.DefaultColor;
        }
        public void RedrawUp()
        {
            int oldY = Console.CursorTop;

            var items = Dir.GetItems().ToList();

            DrawItem(items, Index, ActiveItemColor);

            if (Index + 1 < items.Count())
            {
                DrawItem(items, Index + 1);
            }
        }
        public void RedrawDown()
        {
            var items = Dir.GetItems().ToList();

            DrawItem(items, Index, ActiveItemColor);

            if (Index - 1 >= 0)
            {
                DrawItem(items, Index - 1);
            }
        }
        public static void RedrawLeft(FileTable left, FileTable right)
        {
            var leftItems = left.Dir.GetItems().ToList();
            var rightItems = right.Dir.GetItems().ToList();

            left.DrawItem(leftItems, left.Index, left.ActiveItemColor);
            right.DrawItem(rightItems, right.Index, right.InactiveItemColor);
        }
        public static void RedrawRight(FileTable left, FileTable right)
        {
            var leftItems = left.Dir.GetItems().ToList();
            var rightItems = right.Dir.GetItems().ToList();

            right.DrawItem(rightItems, right.Index, right.ActiveItemColor);
            left.DrawItem(leftItems, left.Index, left.InactiveItemColor);
        }

        // Working with directory.
        public void DeleteSelectedItem()
        {
            var items = Dir.GetItems().ToList();

            if(items[Index] is DirectoryInfo dir)
            {
                dir.Remove();
            }
            else if(items[Index] is FileInfo file)
            {
                file.Delete();
            }
        }
    }
}