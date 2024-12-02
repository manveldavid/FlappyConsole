namespace FlappyConsole.Models;
public class Bird
{
    public bool? IsGoingDown { get; set; }

    public int Height { get; set; }
    public int Width { get; set; }

    public int LastHeight { get; set; }
    public int LastLastHeight { get; set; }

    public void Move(int x, int y = 0)
    {
        LastLastHeight = LastHeight;
        LastHeight = Height;
        Height += x;
        Width += y;

        IsGoingDown = Height == LastLastHeight ? null : Height - LastHeight > 0;
    }
}
