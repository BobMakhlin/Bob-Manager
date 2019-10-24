using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BobManager
{
    static class MyDirectory
    {
        public static long GetDirectorySize(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            long sum = dir.GetFiles().Select(x => x.Length).Sum();

            foreach (var directory in dir.GetDirectories())
            {
                sum += GetDirectorySize(directory.FullName);
            }

            return sum;
        }
    }
}
