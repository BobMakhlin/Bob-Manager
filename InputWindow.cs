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
