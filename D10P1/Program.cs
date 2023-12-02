var reader = new StreamReader("input.txt");

var thread = new Thread();
thread.Run();
Console.WriteLine(thread.Signals.Sum());

internal class Thread
{
    private readonly StreamReader _commands;

    private string _currCommand;
    private bool _readNext;
    private int _currCycle;
    private int _x = 1;
    private int _v;

    public List<int> Signals { get; set; } = [];

    public Thread()
    {
        _commands = new StreamReader("input.txt");
        _readNext = true;
    }

    public void Run()
    {
        while (!_commands.EndOfStream)
        {
            _currCycle++;

            if ((_currCycle - 20) % 40 == 0)
            {
                Signals.Add(_currCycle * _x);
            }

            RunCycle();
        }
    }

    private void RunCycle()
    {
        if (_readNext)
        {
            _currCommand = _commands.ReadLine();

            if (_currCommand == "noop")
            {
                return;
            }

            _v = int.Parse(_currCommand.Split(' ')[1]);
            _readNext = false;
        }
        else
        {
            _x += _v;
            _readNext = true;
        }
    }
}

internal enum CycleState
{
    READING = 0,
    RUNNING = 1,
}