using System.Collections.Generic;
using System;

public class Maze {

    int sizeOfMazeX;
    int sizeOfMazeZ;

    private List<List<Cell>> cells = new List<List<Cell>>();

    public Maze(int x, int z)
    {
        sizeOfMazeX = x;
        sizeOfMazeZ = z;

        // Generate cells
        for (int i = 0; i < x; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < x; j++)
            {
                Cell cell = new Cell(i,j);
                row.Add(cell);
            }
            cells.Add(row);
        }

        // Create maze using Prim's algorithm
        int currentX = x / 2;
        int currentZ = z / 2;
        Cell currentCell = cells[currentX][currentZ];
        currentCell.visit();

        List<Wall> walls = new List<Wall>();

        foreach (Wall wall in currentCell.getWalls()) {
            walls.Add(wall);
        }

        Random random = new Random();
        while(walls.Count != 0)
        {
            int selectWall = random.Next(walls.Count);
            Wall currentWall = walls[selectWall];
            Cell adjacentCell = null;
            try
            {
                switch (currentWall.getDirection())
                {
                    case 0:
                        adjacentCell = cells[currentWall.getX() - 1][currentWall.getZ()];
                        break;
                    case 1:
                        adjacentCell = cells[currentWall.getX()][currentWall.getZ() + 1];
                        break;
                    case 2:
                        adjacentCell = cells[currentWall.getX() + 1][currentWall.getZ()];
                        break;
                    case 3:
                        adjacentCell = cells[currentWall.getX()][currentWall.getZ() - 1];
                        break;
                }
            } catch (Exception e)
            {
                walls.Remove(currentWall);
                continue;
            }

            if (!adjacentCell.getVisited())
            {
                adjacentCell.visit();
                currentWall.setIsActive(false);
                List<Wall> adjacentCellWalls = adjacentCell.getWalls();
                adjacentCellWalls[(currentWall.getDirection() + 2) % 4].setIsActive(false);
                
                foreach(Wall wall in adjacentCellWalls)
                {
                    if (wall.getIsActive())
                    {
                        walls.Add(wall);
                    }
                }
            }
            walls.Remove(currentWall);

        }

    }

    public List<List<Cell>> getCells()
    {
        return cells;
    }
}
