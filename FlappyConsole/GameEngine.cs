using FlappyConsole.Models;
using System.Text;

namespace FlappyConsole;

public static class GameEngine
{
    public static async Task RunLoop(TimeSpan period, Field field, Bird bird, User user, Action<string> onFrame)
    {
        for (int i = 0; i < field.Width; i++)
            field.AddEmptyGate();

        while (!user.IsGameOver)
        {
            await Task.Delay(period);

            user.MoveBird(bird);
            field.AddGatesToFieldRandomly();
            field.MoveField();

            onFrame.Invoke(Render(field, bird, user));
            user.Score++;
        }
    }
    public static string Render(Field field, Bird bird, User user)
    {
        var render = string.Empty;

        var gates = field.Gates.ToArray();
        var bitMap = new bool[field.Height, field.Width];

        for (int i = 0; i < field.Height; i++)
            for (int j = 0; j < field.Width; j++)
                bitMap[i, j] = gates[j].Values[i];

        var renders = new StringBuilder[field.Height];
        Console.SetCursorPosition(0, 0);

        for (int i = 0; i < bitMap.GetLength(0); i++)
        {
            renders[i] = new StringBuilder();

            for (int j = 0; j < bitMap.GetLength(1); j++)
            {
                if (bitMap[i, j])
                    renders[i].Append('|');
                else
                    renders[i].Append(' ');
            }

            renders[i].Append('\n');
        }

        if (bird.Height < 0 || bird.Width < 0 ||
            bird.Height >= field.Height || bird.Width >= field.Width)
            user.IsGameOver = true;

        if (!user.IsGameOver)
        {
            renders[bird.Height][bird.Width] = '0';
            var tailWidth = bird.Width - 1;
            var tailHeight = bird.Height + (bird.IsGoingDown is null ? 0 : (bool)bird.IsGoingDown ? -1 : 1);

            if (tailWidth >= 0 && tailHeight >= 0 && tailWidth < field.Width && tailHeight < field.Height)
                renders[tailHeight][tailWidth] = (bird.IsGoingDown is null ? '-' : (bool)bird.IsGoingDown ? '\\' : '/');

            if (bitMap[bird.Height, bird.Width])
                user.IsGameOver = true;

            foreach (var line in renders)
                render += line.ToString();

            render += $"\nScore: {user.Score}";
        }

        if (user.IsGameOver)
            render += $"{string.Join('\n', Enumerable.Range(0, field.Height).Select(i => string.Empty))}\n\n\nGAME OVER";

        return render;
    }
}
