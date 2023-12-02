using System.Diagnostics;

var reader = new StreamReader("input.txt");

var result = 0;
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

    if ((cCurr - 20) % 40 == 0)
    {
        result += cCurr * x;
    }

    if (cmd == "noop" || busy)
    {
        continue;
    }

    cAddx = cCurr;
    v = int.Parse(cmd[cmd.IndexOf(' ')..]);
    busy = true;
}
Console.WriteLine(result);