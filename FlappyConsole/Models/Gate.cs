namespace FlappyConsole.Models;

public class Gate
{
    public bool[] Values { get; set; }

    public static Gate Create(int start, int gateHeight, int fieldHeight)
    {
        if(start < 0 || start + gateHeight > fieldHeight)
            throw new ArgumentOutOfRangeException();

        var gate = new Gate() { Values = new bool[fieldHeight] };

        for (int i = 0; i < start; i++) 
            gate.Values[i] = true;

        for (int i = start + gateHeight; i < fieldHeight; i++)
            gate.Values[i] = true;

        return gate;
    }

    public static Gate GetEmptyGate(int fieldHeight) => 
        new Gate() { Values = new bool[fieldHeight] };

    public static Gate[] CreateSequence(int count, int start, int gateHeight, int fieldHeight)
    {
        if(count < 0) 
            throw new ArgumentOutOfRangeException();

        var sequence = new Gate[count];

        for (int i = 0; i < count; i++)
            sequence[i] = Create(start, gateHeight, fieldHeight);

        return sequence;
    }
}
