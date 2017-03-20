using UnityEngine;
using System.Collections;

public class SpawnCell : MonoBehaviour
{

    public Transform cell;
	public Transform cub;
    public int mazeSize;
	public GameObject cam1;
	public GameObject firstPersonCam;

    // Use this for initialization
	public Maze maze;
    void Start()
    {
        maze = new Maze(mazeSize, mazeSize);
		StartCoroutine(slowSpawn());
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	IEnumerator slowSpawn()
	{
		int count = 0;
		for (int x = 0; x < mazeSize; x++)
        {	
			if(x % 4 == 0)
			{
				Transform clone2 = (Transform) Instantiate(cub, new Vector3 (6 * x, 1, 6 * ((mazeSize - 1) - x)), Quaternion.identity);
			}

            for (int z = 0; z < mazeSize; z++)
            {

                Cell mazeCell = maze.getCells()[x][z];
                Transform clone = (Transform) Instantiate(cell, new Vector3(6 * z, 0, 6 * ((mazeSize - 1) - x)), Quaternion.identity);

				if(z % 4 == 0 && x % 4 == 0 && count == 0)
				{
					Transform clone2 = (Transform) Instantiate(cub, new Vector3 (6 * z, 1, 6 * ((mazeSize - 1) - x)), Quaternion.identity);
				}

                for(int i = 0; i < 4; i++)
                {
                    clone.GetChild(i).gameObject.SetActive(mazeCell.getWalls()[i].getIsActive());

                    Debug.Log("x" + x + "z" + z + "Wall" + i + mazeCell.getWalls()[i].getIsActive());
                }
				
				yield return null;
				count++;
                
            }
        }
		
		firstPersonCam.SetActive(true);
		cam1.SetActive(false);
	}
}
