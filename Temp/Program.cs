namespace Temp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt").Replace("\r\n", "\\r\\n");
            Console.WriteLine(text);
        }
    }
}