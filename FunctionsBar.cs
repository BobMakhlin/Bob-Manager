using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobManager
{
    class FunctionsBar
    {
        Dictionary<string, string> functions = new Dictionary<string, string>();
        public (int X, int Y) Pos { get; set; }

        public void AddFunction(string name, string descr)
        {
            functions.Add(name, descr);
        }
        public void Draw()
        {
            Console.SetCursorPosition(Pos.X, Pos.Y);

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
    }
}
