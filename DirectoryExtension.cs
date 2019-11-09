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
    static class DirectoryExtension
    {
        public static long GetSize(this DirectoryInfo dir)
        {
            long sum = 0;
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    sum += file.Length;
                }
                catch (Exception)
                {
                }
            }
            foreach (var directory in dir.GetDirectories())
            {
                try
                {
                    sum += GetSize(directory);
                }
                catch (Exception)
                {
                }
            }

            return sum;
        }
        public static void Remove(this DirectoryInfo dir)
        {
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {
                }
            }
            foreach (var directory in dir.GetDirectories())
            {
                try
                {
                    Remove(directory);
                    directory.Delete();
                }
                catch (Exception)
                {
                }
            }

            dir.Delete();
        }
        public static void CopyTo(this DirectoryInfo dir, string path)
        {
            Directory.CreateDirectory(path);

            foreach (var file in dir.GetFiles())
            {
                try
                {
                    file.CopyTo($"{path}\\{file.Name}");
                }
                catch (Exception)
                {
                }
            }

            foreach (var directory in dir.GetDirectories())
            {
                try
                {
                    CopyTo(directory, $"{path}\\{directory.Name}");
                }
                catch (Exception)
                {
                }
            }
        }

        public static IEnumerable<FileSystemInfo> GetItems(this DirectoryInfo dir)
        {
            var items = new List<FileSystemInfo>();

            try
            {
                var parent = Directory.GetParent(dir.FullName);
                if (parent != null)
                {
                    items.Add(parent);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                var dirs = dir.GetDirectories();
                foreach (var directory in dirs)
                {
                    try
                    {
                        items.Add(directory);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            try
            {
                var files = dir.GetFiles();
                foreach (var file in files)
                {
                    try
                    {
                        items.Add(file);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            return items;
        }
    }
}
