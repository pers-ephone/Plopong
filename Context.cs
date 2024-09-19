
class Context
{
    public bool playing;
    public Player player1;
    public Player player2;
    public Ball ball;
    public int score = -1;
    public int winner = 0;

    public int sizex;
    public int sizey;

    public Context(int sizex, int sizey)
    {
        this.sizex = sizex;
        this.sizey = sizey;
        playing = false;
        ball = new Ball(sizex / 2, sizey / 2);
        Reset();
    }

    public void Reset()
    {
        player1 = new Player(30, sizey / 2);
        player2 = new Player(sizex - 30, sizey / 2);
        ball.Reset();
    }
}
