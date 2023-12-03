using System.Text.RegularExpressions;

var monkeys = File
    .ReadAllText("input.txt")
    .Split(Environment.NewLine + Environment.NewLine);

Match match;
List<int> totalInspects = [0, 0, 0, 0, 0, 0, 0, 0];
List<long>[] monkeyBags = [[], [], [], [], [], [], [], []];
for (int i = 0; i < 10_0000; i++)
{
    foreach (var monkey in monkeys)
    {
        var lines = monkey.Split(Environment.NewLine);

        match = Regex.Match(monkey, @"Monkey (\d+)");
        int monkeyId = int.Parse(match.Groups[1].Value);

        if (i == 0)
        {
            monkeyBags[monkeyId] = lines[1]
            .Split(':')[1]
            .Split(',')
            .Select(x => long.Parse(x))
            .Concat(monkeyBags[monkeyId])
            .ToList();
        }

        var items = monkeyBags[monkeyId].ToList();

        foreach (var item in items)
        {
            long x = item;

            // inspect

            match = Regex.Match(monkey, @"Operation: new = (\S+) (.) (\S+)");

            var o1 = match.Groups[1].Value == "old"
                ? x
                : int.Parse(match.Groups[1].Value);

            var o2 = match.Groups[3].Value == "old"
                ? x
                : int.Parse(match.Groups[3].Value);

            var opp = match.Groups[2].Value;

            if (opp == "+")
            {
                x = o1 + o2;
            }
            else
            {
                x = o1 * o2;
            }

            totalInspects[monkeyId]++;

            // relief

            var mod = (17 * 13 * 19 * 7 * 11 * 3 * 2 * 5);
            if (x % mod == 0)
            {
                x /= mod;
            }

            // test

            var div = int.Parse(Regex
                .Match(monkey, @"Test: divisible by (\d+)")
                .Groups[1]
                .Value);

            var mTrue = int.Parse(Regex.Match(monkey, @"If true: throw to monkey (\d+)").Groups[1].Value);
            var mFalse = int.Parse(Regex.Match(monkey, @"If false: throw to monkey (\d+)").Groups[1].Value);

            // throw

            if (x % div == 0)
            {
                monkeyBags[mTrue].Add((long)x);
            }
            else
            {
                monkeyBags[mFalse].Add((long)x);
            }
        }

        monkeyBags[monkeyId].Clear();
    }
}

totalInspects.Sort();

var m1 = totalInspects[7];
var m2 = totalInspects[6];

Console.WriteLine(m1 * m2);