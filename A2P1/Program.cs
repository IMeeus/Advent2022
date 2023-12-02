namespace A2P1
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
                var me = MeToRPS(line[2]);

                if (IsLoss(op, me))
                {
                    score += l + me;
                    continue;
                }
                if (IsDraw(op, me))
                {
                    score += d + me;
                    continue;
                }
                if (IsWin(op, me))
                {
                    score += w + me;
                    continue;
                }
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

        private static int MeToRPS(char letter)
        {
            return letter switch
            {
                'X' => r,
                'Y' => p,
                'Z' => s,
                _ => throw new NotImplementedException()
            };
        }

        private static bool IsWin(int op, int me)
        {
            if (me == r && op == s) return true;
            if (me == p && op == r) return true;
            if (me == s && op == p) return true;
            return false;
        }

        private static bool IsDraw(int op, int me)
        {
            return op == me;
        }

        private static bool IsLoss(int op, int me)
        {
            if (me == r && op == p) return true;
            if (me == p && op == s) return true;
            if (me == s && op == r) return true;
            return false;
        }
    }
}