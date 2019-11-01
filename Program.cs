namespace BobManager
{
    class Program
    {
        public const int MaxItemsCount = 20;
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            fileManager.Start();
        }
    }
}
