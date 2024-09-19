
abstract class Body
{
    public float x;
    public float y;
    public int sizey;
    public int sizex;

    public Body(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public bool Collide(Body other)
    {
        // return : does this collide with other?
        // x collide
        bool collide = (
            this.x <= (other.x + other.sizex) &&
            (this.x + this.sizex) >= other.x
            );

        // y collide
        collide = collide && (
            this.y <= (other.y + other.sizey) &&
            (this.y + this.sizey) >= other.y
        );
        return collide;
    }
}
