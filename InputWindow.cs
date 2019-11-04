using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobManager
{
    class InputWindow
    {
        public (int X, int Y) Pos { get; set; }
        public string Text { get; set; }
        public void Draw()
        {
            int tildesCount = (Console.BufferWidth - Text.Length) / 2;

            string tildes = new string('~', tildesCount);

            Console.SetCursorPosition(Pos.X, Pos.Y);

            Console.Write($"{tildes}{Text}{tildes}");

            Console.SetCursorPosition(Pos.X, Pos.Y + 3);
            Console.Write(new string('~', Console.BufferWidth));
        }
        public string GetString()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y + 1);
            return Console.ReadLine();
        }
    }
}
