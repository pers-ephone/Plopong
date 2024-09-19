
class Player : Body
{
    public Player(float x, float y) : base(x, y)
    {
        sizey = 50;
        sizex = 8;
        this.x = (int)Math.Round(x - sizex / 2);
        this.y = (int)Math.Round(y - sizey / 2);
    }
}
