using System.Text.RegularExpressions;

namespace A5P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var txt = File.ReadAllText("input.txt").Trim();

            var arrangement = txt.Split("\r\n\r\n")[0];
            var lines = arrangement.Split("\r\n").ToList();

            var indexLine = lines[^1];
            List<int> stackLocations = indexLine
                .Where(x => !char.IsWhiteSpace(x))
                .Select(x => indexLine.IndexOf(x))
                .ToList();
            lines.RemoveAt(lines.Count - 1);

            var stacks = new Stack<char>[stackLocations.Count];
            for (int i = 0; i < stackLocations.Count; i++)
            {
                stacks[i] = new Stack<char>();
            }

            for (var li = lines.Count - 1; li >= 0; li--)
            {
                var line = lines[li];
                for (var sli = 0; sli < stackLocations.Count; sli++)
                {
                    var stackLocation = stackLocations[sli];
                    var c = line[stackLocation];
                    if (char.IsWhiteSpace(c)) continue;
                    stacks[sli].Push(c);
                }
            }

            var instructions = txt.Split("\r\n\r\n")[1].Split("\r\n");
            foreach (var instruction in instructions)
            {
                var match = Regex.Match(instruction, @"move (\d+) from (\d+) to (\d+)");
                var amount = int.Parse(match.Groups[1].Value);
                var src = int.Parse(match.Groups[2].Value) - 1;
                var dst = int.Parse(match.Groups[3].Value) - 1;

                for (int i = 0; i < amount; i++)
                {
                    var pop = stacks[src].Pop();
                    stacks[dst].Push(pop);
                }
            }

            var result = "";
            foreach (var stack in stacks)
            {
                result += stack.First();
            }

            Console.WriteLine(result);
        }
    }
}