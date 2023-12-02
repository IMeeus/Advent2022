namespace A4P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var txt = File.ReadAllText("input.txt").Trim();

            var pairs = txt.Split("\r\n");

            int counter = 0;
            foreach (var pair in pairs)
            {
                var e1 = pair.Split(",")[0];
                var e1min = int.Parse(e1.Split("-")[0]);
                var e1max = int.Parse(e1.Split("-")[1]);

                var e2 = pair.Split(",")[1];
                var e2min = int.Parse(e2.Split("-")[0]);
                var e2max = int.Parse(e2.Split("-")[1]);

                var e1range = Enumerable.Range(e1min, e1max - e1min + 1);
                var e2range = Enumerable.Range(e2min, e2max - e2min + 1);

                if (e2range.Any(x => e1range.Contains(x)))
                    counter++;

                //if (InRange(e2min, e1min, e1max) ||
                //    InRange(e2max, e1min, e1max) ||
                //    InRange(e1min, e2min, e2max) ||
                //    InRange(e1max, e2min, e2max))
                //    counter++;
            }

            Console.WriteLine(counter.ToString());
        }

        private static bool InRange(int x, int min, int max)
        {
            return min <= x && x <= max;
        }
    }
}