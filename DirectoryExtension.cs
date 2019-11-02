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
            foreach(var file in dir.GetFiles())
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
                catch(Exception)
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
