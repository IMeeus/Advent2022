var lines = File.ReadAllLines("input.txt");

List<Node> _spots = [];
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        _spots.Add(new(x, y, lines[y][x]));
    }
}

int shortestPath = int.MaxValue;
Dictionary<(int x, int y), int> _visited = [];
var root = _spots.Single(spot => spot.c == 'S');
root.c = 'a';
var destination = _spots.Single(spot => spot.c == 'E');
destination.c = 'z';

void Search(Node node, int distance)
{
    _visited.Add((node.x, node.y), distance);

    List<Node> candidates = [
        _spots.SingleOrDefault(spot => spot.x == node.x - 1 && spot.y == node.y), // left
        _spots.SingleOrDefault(spot => spot.x == node.x + 1 && spot.y == node.y), // right
        _spots.SingleOrDefault(spot => spot.x == node.x && spot.y == node.y - 1), // up
        _spots.SingleOrDefault(spot => spot.x == node.x && spot.y == node.y + 1), // down
    ];

    foreach (var candidate in candidates)
    {
        if (candidate is null) continue;
        if (candidate.c > node.c + 1) continue;
        if (candidate == destination)
        {
            if (distance < shortestPath)
            {
                shortestPath = distance + 1;
            }
            continue;
        }
        if (_visited.ContainsKey((candidate.x, candidate.y))) continue;

        Search(candidate, distance + 1);
    }
}

Search(root, 0);

Console.WriteLine(shortestPath);

internal class Node
{
    public int x { get; set; }
    public int y { get; set; }
    public char c { get; set; }

    public Node(int x, int y, char c)
    {
        this.x = x;
        this.y = y;
        this.c = c;
    }
}