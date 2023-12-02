namespace A3P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");
            var bps = text.Trim().Split("\r\n");

            int sum = 0;
            foreach (var bp in bps)
            {
                var halfSize = bp.Length / 2;
                var c1 = bp.Substring(0, halfSize);
                var c2 = bp.Substring(halfSize, halfSize);

                var common = c1
                    .Where(x => c2.Contains(x))
                    .Distinct()
                    .Single();

                sum += ToPrio(common);
            }

            Console.WriteLine(sum);
        }

        private static int ToPrio(char x)
        {
            var n = Convert.ToInt16(x);
            if (char.IsUpper(x))
                return n - 65 + 27;
            else
                return n - 97 + 1;
        }
    }
}