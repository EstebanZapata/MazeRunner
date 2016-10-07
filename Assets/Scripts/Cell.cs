using System.Collections.Generic;

public class Cell {

    private bool wasVisited;
    private List<Wall> walls;
    private int x;
    private int z;

    public Cell(int x, int z)
    {
        wasVisited = false;
        walls = new List<Wall>();
        for (int i = 0; i < 4; i++)
        {

            walls.Add(new Wall(i, x, z));
        }

        this.x = x;
        this.z = z;
        
    }

    public void visit()
    {
        wasVisited = true;
    }

    public bool getVisited()
    {
        return wasVisited;
    }

    public List<Wall> getWalls()
    {
        return walls;
    }

    public int getX()
    {
        return x;
    }

    public int getZ()
    {
        return z;
    }
}


