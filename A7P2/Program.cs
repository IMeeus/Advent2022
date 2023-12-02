using System.Text.RegularExpressions;

namespace A7P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var reader = new StreamReader("input.txt");

            Dir curDir = new("/");
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line == "$ cd /") continue;
                if (line.Contains("$ ls")) continue;

                if (line.Contains("$ cd"))
                {
                    if (line == "$ cd ..")
                    {
                        curDir = curDir.Parent;
                    }
                    else
                    {
                        var match = Regex.Match(line, @"\$ cd (\w+)");
                        curDir = curDir.Dirs.Single(x => x.Name == match.Groups[1].Value);
                    }

                    continue;
                }
                else if (line.Contains("dir"))
                {
                    var match = Regex.Match(line, @"dir (\w+)");
                    var name = match.Groups[1].Value;

                    curDir.TryAddDir(name);

                    continue;
                }
                else
                {
                    var match = Regex.Match(line, @"(\d+) (\w+).(\w+)");
                    var size = int.Parse(match.Groups[1].Value);
                    var name = match.Groups[2].Value;
                    var extension = match.Groups[3].Value;
                    var file = new File($"{name}.{extension}", size);

                    curDir.TryAddFile(file);

                    continue;
                }
            }

            Dir rootDir = curDir;
            do
            {
                rootDir = rootDir.Parent;
            } while (rootDir.Parent is not null);
            var allDirs = rootDir.GetAllChilds();

            const int MAX_SIZE = 70_000_000;
            const int UPD_SPACE = 30_000_000;

            var unUsedSpace = MAX_SIZE - rootDir.GetSize();
            var reqSpace = UPD_SPACE - unUsedSpace;

            var winner = allDirs
                .Select(x => x.GetSize())
                .Where(x => x >= reqSpace)
                .Min();

            Console.WriteLine(winner);
        }

        private class Dir
        {
            public string Name { get; set; }
            public List<File> Files { get; set; } = new List<File>();
            public List<Dir> Dirs { get; set; } = new List<Dir>();
            public Dir? Parent { get; set; }

            public Dir(string name)
            {
                Name = name;
            }

            public Dir(string name, Dir parent)
            {
                Name = name;
                Parent = parent;
            }

            public void TryAddFile(File file)
            {
                if (Files.Any(x => x.FullName == file.FullName))
                    return;

                Files.Add(file);
            }

            public void TryAddDir(string name)
            {
                if (Dirs.Any(x => x.Name == name))
                    return;

                Dirs.Add(new Dir(name, this));
            }

            public int GetSize()
            {
                var sizeOfFiles = Files.Sum(x => x.Size);
                var sizeOfDirs = Dirs.Sum(x => x.GetSize());

                return sizeOfFiles + sizeOfDirs;
            }

            public Dir GetParent()
            {
                if (Parent is not null)
                    return Parent;

                return this;
            }

            public List<Dir> GetAllChilds()
            {
                var result = new List<Dir>() { this };
                result.AddRange(Dirs.SelectMany(x => x.GetAllChilds()));
                return result;
            }
        }

        private record File
        {
            public string FullName { get; set; }
            public int Size { get; set; }

            public File(string fullName, int size)
            {
                FullName = fullName;
                Size = size;
            }
        }
    }
}