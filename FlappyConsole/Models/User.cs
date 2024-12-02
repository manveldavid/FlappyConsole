namespace FlappyConsole.Models;

public class User
{
    public bool IsGameOver { get; set; }
    public int UpForce { get; set; }
    public int UpForcePortion { get; set; }
    public int Score { get; set; }

    public void GetUpForce()
    {
        lock (this)
        {
            UpForce += UpForcePortion;
        }
    }
    public void MoveBird(Bird bird)
    {
        if (UpForce > 0)
        {
            bird.Move(-1);

            lock (this)
            {
                UpForce--;
            }
        }
        else
        {
            bird.Move(1);
        }
    }
}
