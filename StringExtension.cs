namespace BobManager
{
    static class StringExtension
    {
        public static string Shorten(this string str, int count)
        {
            if (count > 3 && str.Length > count)
                return $"{str.Substring(0, count - 3)}...";
            return str;
        }
    }
}
