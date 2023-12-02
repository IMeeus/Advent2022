namespace A1P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fullText = File.ReadAllText("calories.txt");
            var elfBlocks = fullText.Trim().Split("\r\n\r\n");

            List<int> calsPerElf = new();
            foreach (var elfBlock in elfBlocks)
            {
                var calBlocks = elfBlock.Split("\r\n");
                int cals = 0;
                foreach (var calBlock in calBlocks)
                {
                    cals += int.Parse(calBlock);
                }
                calsPerElf.Add(cals);
            }

            calsPerElf.Sort();
            //Console.WriteLine(
            //    calsPerElf[calsPerElf.Count] +
            //    calsPerElf[calsPerElf.Count - 1] +
            //    calsPerElf[calsPerElf.Count - 2]);

            var total =
                calsPerElf[calsPerElf.Count - 1] +
                calsPerElf[calsPerElf.Count - 2] +
                calsPerElf[calsPerElf.Count - 3];

            Console.WriteLine(total);
        }
    }
}