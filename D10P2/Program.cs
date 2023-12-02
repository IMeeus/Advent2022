var reader = new StreamReader("input.txt");

var cCurr = 0;
var cAddx = 0;
var v = 0;
var x = 1;
string cmd = "";
var busy = false;
while (!reader.EndOfStream)
{
    cCurr++;

    if (cCurr - cAddx == 2)
    {
        x += v;
        busy = false;
    }

    if (!busy)
    {
        cmd = reader.ReadLine();
    }

    var px = (cCurr - 1) % 40;
    if (px >= x - 1 && px <= x + 1)
    {
        Console.Write('#');
    }
    else
    {
        Console.Write('.');
    }

    if (cCurr % 40 == 0)
    {
        Console.Write("\n");
    }

    if (cmd == "noop" || busy)
    {
        continue;
    }

    cAddx = cCurr;
    v = int.Parse(cmd[cmd.IndexOf(' ')..]);
    busy = true;
}