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
    class Helper
    {
        public static string FormatSize(long length)
        {
            if (length < Math.Pow(2, 10))
                return $"{length:0.000} B";
            if (length >= Math.Pow(2, 10) && length < Math.Pow(2, 20))
                return $"{length / (double)1000:0.000} KB";
            if (length >= Math.Pow(2, 20) && length < Math.Pow(2, 30))
                return $"{length / 1e+6:0.000} MB";
            if (length >= Math.Pow(2, 30) && length < Math.Pow(2, 40))
                return $"{length / 1e+9:0.000} GB";
            if (length >= Math.Pow(2, 40) && length < Math.Pow(2, 60))
                return $"{length / 1e+12:0.000} TB";
            throw new FormatException("Bad size");
        }
        public static string CenterText(string str, int count, char symbol = ' ')
        {
            if (count <= str.Length)
                return str;

            int partSize = (count - str.Length) / 2;

            string left = new string(symbol, partSize);
            string right = new string(symbol, partSize);

            return left + str + right;
        }
        public static string GetItemInfo(FileSystemInfo item)
        {
            string info = string.Empty;

            if (item is DirectoryInfo dir)
            {
                info = $"| {dir.Name.Shorten(22),-22} | " +
                    $"<Directory> | " +
                    $"{dir.CreationTime.ToShortDateString()} | " +
                    $"{dir.CreationTime.ToShortTimeString(),5} |";
            }
            else if (item is FileInfo file)
            {
                info = $"| {file.Name.Shorten(22),-22} | " +
                    $"{Helper.FormatSize(file.Length),11} | " +
                    $"{file.CreationTime.ToShortDateString()} | " +
                    $"{file.CreationTime.ToShortTimeString(),5} |";
            }

            return info;
        }
    }
}
