using FlappyConsole.Models;
using System.Text;

namespace FlappyConsole;

internal class Program
{
    static Bird Bird { get; set; }
    static Field Field { get; set; }
    static User User { get; set; }
    static TimeSpan Period { get; set; }

    static void Main(string[] args)
    {
        Field = new Field()
        {
            CurrentEmptyGateSequence = 0,
            GateWidth = 3,
            GateHeight = 5,
            Height = 10,
            Width = 40,
        };
        Bird = new Bird()
        {
            Height = Field.Height / 2,
            Width = Field.Width / 2,
        };
        User = new User()
        {
            UpForce = 1,
            UpForcePortion = 2,
        };
        Period = TimeSpan.FromMilliseconds(100);

        GameEngine.RunLoop(Period, Field, Bird, User, frame =>
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(frame);
        });

        while (true)
        {
            Console.ReadKey();
            User.GetUpForce();
        }
    }

}
