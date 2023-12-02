namespace A3P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt").Trim();
            var bps = text.Trim().Split("\r\n");

            int sum = 0;
            for (int i = 0; i <= bps.Length - 3; i += 3)
            {
                var e1 = bps[i];
                var e2 = bps[i + 1];
                var e3 = bps[i + 2];

                var common = e1
                    .Where(x => e2.Contains(x))
                    .Where(x => e3.Contains(x))
                    .Distinct()
                    .Single();

                sum += ToPrio(common);
            }

            Console.WriteLine(sum);
        }

        private static int ToPrio(char x)
        {
            var n = Convert.ToInt16(x);
            var uni_A = Convert.ToInt16('A');
            var uni_a = Convert.ToInt16('a');

            if (char.IsUpper(x))
                return n - uni_A + 27;
            else
                return n - uni_a + 1;
        }
    }
}