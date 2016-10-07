public class Wall
{
    private bool isActive;
    private int direction;
    // Below doesn't work so lets use coordinates
    //private Cell owner;
    private int x;
    private int z;

    public Wall(int direction, int x, int z)
    {
        this.direction = direction;
        isActive = true;
        //this.owner = owner;
        this.x = x;
        this.z = z;
    }

    public bool getIsActive()
    {
        return isActive;
    }

    public void setIsActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public int getDirection()
    {
        return direction;
    }

   /* public Cell getOwner()
    {
        return owner;
    }*/

    public int getX()
    {
        return x;
    }

    public int getZ()
    {
        return z;
    }
}