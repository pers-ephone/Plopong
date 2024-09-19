using System.Numerics;

class Ball : Body
{
    public Vector2 speed = new Vector2();
    float startSpeed = 3.5f; // change ball start speed here
    Random random = new Random();
    float basex;
    float basey;
    public Ball(float x, float y) : base(x, y)
    {
        basex = x;
        basey = y;
        sizey = 10;
        sizex = 10;
        Reset();
    }

    public void Reset()
    {
        x = (int)Math.Round(basex - sizex / 2);
        y = (int)Math.Round(basey - sizey / 2);

        // Random starting vector
        ////// limit start to 90°left n 90°right
        speed = new Vector2(
            (float)(random.NextDouble() * 2 - 1),
            (float)(random.NextDouble() * 2 - 1)
        );
        speed = Vector2.Normalize(speed) * startSpeed;
    }
}
