namespace A2P2
{
    internal class Program
    {
        private const int r = 1;
        private const int p = 2;
        private const int s = 3;

        private const int l = 0;
        private const int d = 3;
        private const int w = 6;

        private static void Main(string[] args)
        {
            var playLines = File.ReadAllText("input.txt").Trim().Split("\r\n");

            var score = 0;
            foreach (var line in playLines)
            {
                var op = OpToRPS(line[0]);
                var state = MeToState(line[2]);

                var options = new[] { r, p, s };
                var me = options.Single(x =>
                {
                    if (state == l)
                        return IsLoss(op, x);
                    if (state == d)
                        return IsDraw(op, x);
                    if (state == w)
                        return IsWin(op, x);
                    throw new NotImplementedException();
                });

                score += state + me;
            }

            Console.WriteLine(score);
        }

        private static int OpToRPS(char letter)
        {
            return letter switch
            {
                'A' => r,
                'B' => p,
                'C' => s,
                _ => throw new NotImplementedException()
            };
        }

        private static int MeToState(char letter)
        {
            return letter switch
            {
                'X' => l,
                'Y' => d,
                'Z' => w,
                _ => throw new NotImplementedException()
            };
        }

        private static bool IsWin(int op, int me)
        {
            return (me == r && op == s)
                || (me == p && op == r)
                || (me == s && op == p);
        }

        private static bool IsDraw(int op, int me)
        {
            return op == me;
        }

        private static bool IsLoss(int op, int me)
        {
            return (me == r && op == p)
                || (me == p && op == s)
                || (me == s && op == r);
        }
    }
}