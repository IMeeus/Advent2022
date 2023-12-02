namespace A8P1
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var lines = File.ReadAllText("input.txt").Trim().Split("\r\n");
            var rowCount = lines.Length;
            var colCount = lines[0].Length;

            var tasks = new List<Task<bool>>();
            for (int x = 0; x < rowCount; x++)
            {
                for (int y = 0; y < colCount; y++)
                {
                    tasks.Add(IsVisible(x, y, lines));
                }
            }

            var results = await Task.WhenAll(tasks);
            var visibleCount = results.Where(x => x is true).Count();

            Console.WriteLine(visibleCount.ToString());
        }

        private static Task<bool> IsVisible(int row, int col, string[] lines)
        {
            var rowCount = lines.Length;
            var colCount = lines[0].Length;
            var currTree = lines[row][col];

            var leftRange = Enumerable.Range(0, col)
                .Select(t => lines[row][t])
                .ToArray();

            if (leftRange.All(x => x < currTree))
                return Task.FromResult(true);

            var rightRange = Enumerable
                .Range(col + 1, colCount - col - 1)
                .Select(t => lines[row][t]).ToArray();

            if (rightRange.All(x => x < currTree))
                return Task.FromResult(true);

            var upRange = Enumerable.Range(0, row)
                .Select(t => lines[t][col])
                .ToArray();

            if (upRange.All(x => x < currTree))
                return Task.FromResult(true);

            var downRange = Enumerable.Range(row + 1, rowCount - row - 1)
                .Select(t => lines[t][col])
                .ToArray();

            if (downRange.All(x => x < currTree))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
    }
}