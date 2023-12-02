using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace A9P2_Refactor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var txt = File.ReadAllText("input.txt").Trim();

            var lines = txt.Split("\r\n");

            Pos[] knots = Enumerable.Repeat(new Pos(0, 0), 10).ToArray();
            List<Pos> posLog = new() { new(0, 0) };

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(\w) (\d+)");
                var direction = match.Groups[1].Value;
                var amount = int.Parse(match.Groups[2].Value);

                for (int i = 0; i < amount; i++)
                {
                    var head = knots[0];

                    switch (direction)
                    {
                        case "L": head.X--; break;
                        case "R": head.X++; break;
                        case "U": head.Y--; break;
                        case "D": head.Y++; break;
                    }

                    for (int ki = 1; ki < knots.Length; ki++)
                    {
                        var cKnot = knots[ki];
                        var pKnot = knots[ki - 1];

                        var horStretch = pKnot.X - cKnot.X;
                        if (Math.Abs(horStretch) == 2)
                        {
                            if (horStretch > 0)
                            {
                                cKnot.X++;
                            }

                            if (horStretch < 0)
                            {
                                cKnot.X--;
                            }

                            if (pKnot.Y > cKnot.Y)
                            {
                                cKnot.Y++;
                            }

                            if (pKnot.Y < cKnot.Y)
                            {
                                cKnot.Y--;
                            }
                        }

                        var verStretch = pKnot.Y - cKnot.Y;
                        if (Math.Abs(verStretch) == 2)
                        {
                            if (verStretch > 0)
                            {
                                cKnot.Y++;
                            }

                            if (verStretch < 0)
                            {
                                cKnot.Y--;
                            }

                            if (pKnot.X > cKnot.X)
                            {
                                cKnot.X++;
                            }

                            if (pKnot.X < cKnot.X)
                            {
                                cKnot.X--;
                            }
                        }
                    }

                    posLog.Add(knots[9]);
                }
            }

            var posCount = posLog.Distinct(new PosComparer()).Count();
            Console.WriteLine(posCount.ToString());
        }

        internal class Pos
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Pos(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        internal class PosComparer : IEqualityComparer<Pos>
        {
            public bool Equals(Pos? a, Pos? b)
            {
                return a?.X == b?.X && a?.Y == b?.Y;
            }

            public int GetHashCode([DisallowNull] Pos obj)
            {
                return obj.X + obj.Y;
            }
        }
    }
}