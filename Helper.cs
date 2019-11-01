using System;

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
    }
}
