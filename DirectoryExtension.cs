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
            long sum = dir.GetFiles().Select(x => x.Length).Sum();

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
            catch(Exception)
            {
            }

            return items;
        }
    }
}
