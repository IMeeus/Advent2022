using System.Text.RegularExpressions;

namespace A9P1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var txt = File.ReadAllText("input.txt").Trim();

            var lines = txt.Split("\r\n");

            (int X, int Y) head = (0, 0);
            (int X, int Y) tail = (0, 0);
            List<(int, int)> posLog = new() { tail };
            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(\w) (\d+)");
                var direction = match.Groups[1].Value;
                var amount = int.Parse(match.Groups[2].Value);

                for (int i = 0; i < amount; i++)
                {
                    switch (direction)
                    {
                        case "L": head.X--; break;
                        case "R": head.X++; break;
                        case "U": head.Y--; break;
                        case "D": head.Y++; break;
                    }

                    var horStretch = head.X - tail.X;
                    if (Math.Abs(horStretch) == 2)
                    {
                        if (horStretch > 0)
                        {
                            tail.X++;
                        }

                        if (horStretch < 0)
                        {
                            tail.X--;
                        }

                        if (head.Y > tail.Y)
                        {
                            tail.Y++;
                        }

                        if (head.Y < tail.Y)
                        {
                            tail.Y--;
                        }
                    }

                    var verStretch = head.Y - tail.Y;
                    if (Math.Abs(verStretch) == 2)
                    {
                        if (verStretch > 0)
                        {
                            tail.Y++;
                        }

                        if (verStretch < 0)
                        {
                            tail.Y--;
                        }

                        if (head.X > tail.X)
                        {
                            tail.X++;
                        }

                        if (head.X < tail.X)
                        {
                            tail.X--;
                        }
                    }

                    posLog.Add(tail);
                }
            }

            var posCount = posLog.Distinct().Count();
            Console.WriteLine(posCount.ToString());
        }
    }
}