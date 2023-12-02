using System.Text.RegularExpressions;

namespace A9P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var txt = File.ReadAllText("input.txt").Trim();

            var lines = txt.Split("\r\n");

            (int X, int Y)[] knots = Enumerable.Repeat((0, 0), 10).ToArray();
            List<(int, int)> posLog = new() { (0, 0) };

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(\w) (\d+)");
                var direction = match.Groups[1].Value;
                var amount = int.Parse(match.Groups[2].Value);

                for (int i = 0; i < amount; i++)
                {
                    switch (direction)
                    {
                        case "L": knots[0].X--; break;
                        case "R": knots[0].X++; break;
                        case "U": knots[0].Y--; break;
                        case "D": knots[0].Y++; break;
                    }

                    for (int ki = 1; ki < knots.Length; ki++)
                    {
                        var horStretch = knots[ki - 1].X - knots[ki].X;
                        if (Math.Abs(horStretch) == 2)
                        {
                            if (horStretch > 0)
                            {
                                knots[ki].X++;
                            }

                            if (horStretch < 0)
                            {
                                knots[ki].X--;
                            }

                            if (knots[ki - 1].Y > knots[ki].Y)
                            {
                                knots[ki].Y++;
                            }

                            if (knots[ki - 1].Y < knots[ki].Y)
                            {
                                knots[ki].Y--;
                            }
                        }

                        var verStretch = knots[ki - 1].Y - knots[ki].Y;
                        if (Math.Abs(verStretch) == 2)
                        {
                            if (verStretch > 0)
                            {
                                knots[ki].Y++;
                            }

                            if (verStretch < 0)
                            {
                                knots[ki].Y--;
                            }

                            if (knots[ki - 1].X > knots[ki].X)
                            {
                                knots[ki].X++;
                            }

                            if (knots[ki - 1].X < knots[ki].X)
                            {
                                knots[ki].X--;
                            }
                        }
                    }

                    posLog.Add(knots[9]);
                }
            }

            var posCount = posLog.Distinct().Count();
            Console.WriteLine(posCount.ToString());
        }
    }
}