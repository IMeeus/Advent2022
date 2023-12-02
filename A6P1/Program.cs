namespace A6P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var streamReader = new StreamReader("input.txt");

            var markNotFound = true;
            var buffer = "";
            while (markNotFound)
            {
                var curr = Convert.ToChar(streamReader.Read());
                buffer += curr;

                if (buffer.Length >= 4)
                {
                    var mark = buffer.Substring(buffer.Length - 4, 4);
                    if (mark.Distinct().Count() == 4)
                        markNotFound = false;
                }
            }

            Console.WriteLine(buffer.Length);
        }
    }
}