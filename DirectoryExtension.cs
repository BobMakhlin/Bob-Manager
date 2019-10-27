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
                sum += GetSize(directory);
            }

            return sum;
        }
        public static IEnumerable<FileSystemInfo> GetItems(this DirectoryInfo dir)
        {
            var items = new List<FileSystemInfo>();

            var parent = Directory.GetParent(dir.FullName);
            items.Add(parent);

            items.AddRange(dir.GetDirectories());
            items.AddRange(dir.GetFiles());

            return items;
        }
    }
}
