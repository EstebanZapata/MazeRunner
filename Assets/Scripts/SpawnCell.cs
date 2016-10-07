using UnityEngine;
using System.Collections;

public class SpawnCell : MonoBehaviour
{

    public Transform cell;
    public int mazeSize;

    // Use this for initialization
    void Start()
    {
        Maze maze = new Maze(mazeSize, mazeSize);
        for (int x = 0; x < mazeSize; x++)
        {
            for (int z = 0; z < mazeSize; z++)
            {

                Cell mazeCell = maze.getCells()[x][z];
                Transform clone = (Transform) Instantiate(cell, new Vector3(6 * z, 0, 6 * ((mazeSize - 1) - x)), Quaternion.identity);

                for(int i = 0; i < 4; i++)
                {
                    clone.GetChild(i).gameObject.SetActive(mazeCell.getWalls()[i].getIsActive());
                    Debug.Log("x" + x + "z" + z + "Wall" + i + mazeCell.getWalls()[i].getIsActive());
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
