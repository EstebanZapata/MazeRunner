using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {
    private GameObject player;
    private const int PLAYER_IN_RANGE_DISTANCE = 30;

    private float speed = 1.0f;

	public GameObject cubeBoi;
	public Transform track;
	public int rayDist = 5;
	public bool aware = false;
    private SpawnCell mazeSpawner;

    private bool pathFound = false;

    public int indexOfCellInPath = 0;


    private List<Vector3> path;
	//public CharacterController runner;


	void Awake()
    {
        
        
    }

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() 
	{

 

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }


        if (playerIsInRange())
        {

            Debug.Log("Player in range");


            if (!pathFound)
            {
                path = getPathToPlayer();

                foreach(Vector3 element in path)
                {
                    Debug.Log("(" + element.x + "," + element.y + "," + element.z + ")");
                }
                pathFound = true;
            }

            if (pathFound)
            {
                Debug.Log(path.Count);
                Debug.Log(indexOfCellInPath);
                
                if (indexOfCellInPath < path.Count)
                {
                    Vector3 target = path[indexOfCellInPath];

                    Transform transform = cubeBoi.transform;

                    //    transform.forward = Vector3.RotateTowards(transform.forward, target - transform.position, speed * Time.deltaTime, 0.0f);

                    Debug.Log("Moving towards " + target.ToString());

                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                    if (withinDistanceOfTarget(transform.position, target))
                    {
                        indexOfCellInPath++;
                        target = path[indexOfCellInPath];
                    }
                }

                else
                {
                    pathFound = false;
                    indexOfCellInPath = 0;
                }
            }


        }
        else
        {
            //patrolArea();
        }


	}

    private bool withinDistanceOfTarget(Vector3 position, Vector3 target)
    {
        if (Vector3.Distance(position, target) <= 0.25)
        {
            return true;

        }
        else
            return false;
    }

    private bool playerIsInRange()
    {

        float distanceBetweenCubeAndPlayer = Vector3.Distance(player.transform.position, cubeBoi.transform.position);

        if (distanceBetweenCubeAndPlayer < PLAYER_IN_RANGE_DISTANCE)
        {
            return true;
        }
       else
        {
            return false;
        }
    }

    private List<Vector3> getPathToPlayer()
    {
        List<Vector3> path;

        Vector3 cubePosition = cubeBoi.transform.position;
        Vector3 playerPosition = player.transform.position;

         mazeSpawner = GameObject.FindGameObjectWithTag("Maze Spawner").GetComponent<SpawnCell>();
        
        

        Vector3 cubeMatrixLocation = mazeSpawner.getMatrixVectorFromWorldVector(cubePosition);
        Vector3 playerMatrixLocation = mazeSpawner.getMatrixVectorFromWorldVector(playerPosition);

        Debug.Log(cubeMatrixLocation + " " + playerMatrixLocation);

        Maze maze = mazeSpawner.maze;
        List<List<Cell>> cells = maze.getCells();

        path = dijkstra(cells, cubeMatrixLocation, playerMatrixLocation);

        return path;
        
        
    }

    private List<Vector3> dijkstra(List<List<Cell>> cells, Vector3 cubeMatrixLocation, Vector3 targetMatrixLocation)
    {
        Debug.Log("Called dijkstra");
        Debug.Log(cells.Count);


        List<Cell> pathToTarget = null;

        List<Cell> q = new List<Cell>();

        foreach (List<Cell> row in cells)
        {
            foreach(Cell cell in row)
            {
                cell.distance = int.MaxValue;
                cell.previous = null;
                q.Add(cell);
            }
        }

        cells[(int)cubeMatrixLocation.x][(int)cubeMatrixLocation.z].distance = 0;

        while(q.Count != 0)
        {
            Cell u = null;
            int distance = int.MaxValue;

            foreach (Cell cell in q) {
                if (cell.distance < distance) {
                    u = cell;
                    distance = cell.distance;
                }
            }


            q.Remove(u);

            // For each neighbor of u
            Cell[] neighbors = new Cell[4];

            List<Wall> wallsOfU = u.getWalls();


            if (!wallsOfU[0].getIsActive())
            {
                neighbors[0] = cells[u.getX() - 1][u.getZ()];

            }

            if (!wallsOfU[1].getIsActive())
            {
                neighbors[1] = cells[u.getX()][u.getZ() + 1];

            }

            if (!wallsOfU[2].getIsActive())
            {
                neighbors[2] = cells[u.getX() + 1][u.getZ()];

            }

            if (!wallsOfU[3].getIsActive())
            {
               
                neighbors[3] = cells[u.getX()][u.getZ() - 1];

            }

            foreach (Cell neighbor in  neighbors)
            {
                if (neighbor!= null)
                {
                    int distanceOfNeighbor = u.distance + 1;
                    if (distanceOfNeighbor < neighbor.distance)
                    {
                        neighbor.distance = distanceOfNeighbor;
                        neighbor.previous = u;
                    }
                }
            }

            pathToTarget = null;

            if ((u.getX() == (int)targetMatrixLocation.x) && (u.getZ() == (int)targetMatrixLocation.z)) {
                pathToTarget = new List<Cell>();

                pathToTarget.Insert(0, u);

                while (u.previous != null)
                {
                    pathToTarget.Insert(0, u.previous);
                    u = u.previous;
                }

                pathToTarget.Insert(0, u);

                break;
            }

        }

        List<Vector3> pathInWorld = matrixLocationsToVector(pathToTarget);

        Debug.Log("After dijkstra path length is " + pathInWorld.Count);

        return pathInWorld;





    }

    private List<Vector3> matrixLocationsToVector(List<Cell> cellPath)
    {
        List<Vector3> path = new List<Vector3>();

        foreach(Cell cell in cellPath)
        {
            path.Add(mazeSpawner.getWorldVectorFromMatrixVector(new Vector3(cell.getX(),0,cell.getZ())));
        }

        return path;
    }

}
