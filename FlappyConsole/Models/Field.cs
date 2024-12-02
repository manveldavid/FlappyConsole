using System.Threading;

namespace FlappyConsole.Models;

public class Field
{
    private Random _random = new();

    public Queue<Gate> Gates { get; set; } = new();
    public int Height { get; set; }
    public int Width { get; set; }
    public int GateWidth { get; set; }
    public int GateHeight { get; set; }
    public int CurrentEmptyGateSequence { get; set; }
    public int NextEmptyGateSequence { get; set; }

    public void AddGatesToFieldRandomly()
    {
        if (CurrentEmptyGateSequence >= NextEmptyGateSequence)
        {
            var gateSequence = Gate.CreateSequence(GateWidth, _random.Next(0, Height - GateHeight), GateHeight, Height);
            AddGateSequence(gateSequence);
            CurrentEmptyGateSequence = 0;
            NextEmptyGateSequence = _random.Next(GateWidth, Width);
        }
        else
        {
            AddEmptyGate();
            CurrentEmptyGateSequence++;
        }
    }
    public void AddGateSequence(Gate[] gateSequence)
    {
        foreach (var gate in gateSequence)
            Gates.Enqueue(gate);
    }
    public void AddEmptyGate() =>
        Gates.Enqueue(Gate.GetEmptyGate(Height));

    public void MoveField() =>
        Gates.Dequeue();
}
