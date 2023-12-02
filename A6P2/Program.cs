namespace A6P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var streamReader = new StreamReader("input.txt");

            const int markSize = 14;
            var markNotFound = true;
            var buffer = "";
            while (markNotFound)
            {
                var curr = Convert.ToChar(streamReader.Read());
                buffer += curr;

                if (buffer.Length >= markSize)
                {
                    var mark = buffer.Substring(buffer.Length - markSize, markSize);
                    if (mark.Distinct().Count() == markSize)
                        markNotFound = false;
                }
            }

            Console.WriteLine(buffer.Length);
        }
    }
}