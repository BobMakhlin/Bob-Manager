using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobManager
{
    class Helper
    {
        public static string FormatSize(long length)
        {
            if (length < Math.Pow(2, 10))
                return $"{length:0.000} b";
            if (length >= Math.Pow(2, 10) && length < Math.Pow(2, 20))
                return $"{length / (double)1000:0.000} kb";
            if (length >= Math.Pow(2, 20) && length < Math.Pow(2, 30))
                return $"{length / 1e+6:0.000} mb";
            if (length >= Math.Pow(2, 30) && length < Math.Pow(2, 40))
                return $"{length / 1e+9:0.000} gb";
            if(length >= Math.Pow(2, 40) && length < Math.Pow(2, 60))
                return $"{length / 1e+12:0.000} tb";
            throw new FormatException("Bad size");
        }
    }
}
