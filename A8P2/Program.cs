namespace A8P2
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var lines = File.ReadAllText("input.txt").Trim().Split("\r\n");
            var rowCount = lines.Length;
            var colCount = lines[0].Length;

            var tasks = new List<Task<int>>();
            for (int x = 0; x < rowCount; x++)
            {
                for (int y = 0; y < colCount; y++)
                {
                    tasks.Add(GetScenicScore(x, y, lines));
                }
            }

            var results = await Task.WhenAll(tasks);
            var maxScenic = results.Max();

            Console.WriteLine(maxScenic.ToString());
        }

        private static Task<int> GetScenicScore(int row, int col, string[] lines)
        {
            var rowCount = lines.Length;
            var colCount = lines[0].Length;
            var currTree = lines[row][col];

            int scenicScore = 1;

            var leftRange = Enumerable
                .Range(0, col)
                .Reverse()
                .Select(t => lines[row][t])
                .ToArray();

            scenicScore *= GetViewScore(leftRange, currTree);

            var rightRange = Enumerable
                .Range(col + 1, colCount - col - 1)
                .Select(t => lines[row][t])
                .ToArray();

            scenicScore *= GetViewScore(rightRange, currTree);

            var upRange = Enumerable
                .Range(0, row)
                .Reverse()
                .Select(t => lines[t][col])
                .ToArray();

            scenicScore *= GetViewScore(upRange, currTree);

            var downRange = Enumerable
                .Range(row + 1, rowCount - row - 1)
                .Select(t => lines[t][col])
                .ToArray();

            scenicScore *= GetViewScore(downRange, currTree);

            return Task.FromResult(scenicScore);
        }

        private static int GetViewScore(char[] range, char fromTree)
        {
            var view = range.TakeWhile(x => x < fromTree).Count();
            if (view == range.Length)
                return view;
            else
                return view + 1;
        }
    }
}