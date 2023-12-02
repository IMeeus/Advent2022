using System.Text.RegularExpressions;

namespace A7P2_Recursive
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var reader = new StreamReader("input.txt");

            Dir root = new("/");
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
                        root = root.ChangeDirectory("..");
                    }
                    else
                    {
                        var match = Regex.Match(line, @"\$ cd (\w+)");
                        root = root.ChangeDirectory(match.Groups[1].Value);
                    }
                }
                else if (line.Contains("dir"))
                {
                    var match = Regex.Match(line, @"dir (\w+)");
                    var name = match.Groups[1].Value;

                    root.TryAddDir(name);
                }
                else
                {
                    var match = Regex.Match(line, @"(\d+) (\w+).(\w+)");
                    var size = int.Parse(match.Groups[1].Value);
                    var name = match.Groups[2].Value;
                    var extension = match.Groups[3].Value;
                    var file = new File($"{name}.{extension}", size);

                    root.TryAddFile(file);
                }
            }

            root = root.GetRootDir();
            var allDirs = root.GetAllSubDirs();

            const int MAX_SIZE = 70_000_000;
            const int UPD_SPACE = 30_000_000;

            var unUsedSpace = MAX_SIZE - root.GetSize();
            var reqSpace = UPD_SPACE - unUsedSpace;

            var winner = allDirs
                .Select(x => x.GetSize())
                .Where(x => x >= reqSpace)
                .Min();

            Console.WriteLine(winner);
        }

        private class Dir
        {
            public Dir? Parent { get; set; }

            public string Name { get; set; }
            public List<Dir> Dirs { get; set; } = new List<Dir>();
            public List<File> Files { get; set; } = new List<File>();

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

            public Dir ChangeDirectory(string dir)
            {
                if (dir == "..")
                {
                    return Parent!;
                }

                return Dirs.Single(x => x.Name == dir);
            }

            public Dir GetRootDir()
            {
                if (Parent is null) return this;
                return Parent.GetRootDir();
            }

            public List<Dir> GetAllSubDirs()
            {
                var result = new List<Dir>() { this };
                result.AddRange(Dirs.SelectMany(x => x.GetAllSubDirs()));
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