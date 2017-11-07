using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public int grid_N;  //user defined size of field
    public GameObject AliveCell;//prefab of a alive cell
    public GameObject DeadCell;//prefab of a dead cell
    public GameObject[,] gridArray;//the grid
    public int[,] neigborArray;//alternative grid displaying all the neigbors of the cell

    public Material aliveMaterial;//black material
    public Material deadMaterial;//white material

    private MeshRenderer meshRenderer;


    void Start () {
        createGrid();   //creates the initial grid
        neigborArray = new int[grid_N, grid_N];
    }

    void createGrid()
    {
        gridArray = new GameObject[grid_N, grid_N];

        for (int x = 0; x < grid_N; x++)
        {
            for (int y = 0; y < grid_N; y++)
            {
                float randomNumber = UnityEngine.Random.Range(0, 2);

                if (randomNumber == 0)      //dead
                {
                    gridArray[x, y] = Instantiate(DeadCell, new Vector3(x * 0.1f, y * 0.1f, 0), Quaternion.identity);
                }
                else if (randomNumber > 0)  //alive
                {
                    gridArray[x, y] = Instantiate(AliveCell, new Vector3(x * 0.1f, y * 0.1f, 0), Quaternion.identity);
                }
            }
        }
    }//makes a new random grid of alive and dead cells

    void checkNeigbors(GameObject[,] array)
    {
        for(int x = 0; x < grid_N; x++)
        {
            for(int y = 0; y < grid_N; y++)
            {
                int counter = 0;
                try
                {
                    if (array[x - 1, y - 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//7
                }
                catch (Exception e) { }
                try
                {
                    if (array[x, y - 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//4
                }
                catch (Exception e) { }
                try
                {
                    if (array[x + 1, y - 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//1
                }
                catch (Exception e) { }
                try
                {
                    if (array[x - 1, y].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//8
                }
                catch (Exception e) { }
                try
                {
                    if (array[x + 1, y].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//2
                }
                catch (Exception e) { }
                try
                {
                    if (array[x - 1, y + 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//9
                }
                catch (Exception e) { }
                try
                {
                    if (array[x, y + 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//6
                }
                catch (Exception e) { }
                try
                {
                    if (array[x + 1, y + 1].name == "AliveCell(Clone)")
                    {
                        counter++;
                    }//3
                }
                catch (Exception e) { }

                neigborArray[x, y] = counter;
            }
        }
    }//fills the alternative grid
	
    bool aliveOrDead(int x ,int y, bool alive, GameObject[,] array)
    {
        int aliveN = neigborArray[x, y];
        if (alive == true) {
            if (aliveN == 3 || aliveN == 2)//if cell is alive and has 3 or 2 neighbors, stay alive
            {
                return true;
            }
            else {
                return false;
            }
        }
        else
        {
            if(aliveN == 3)//if cell is dead and has 3 neighbors, become alive
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }//returns if the current cell should be alive in the next update

    void updateArray(GameObject[,] array)
    {
        for(int x = 0; x < grid_N; x++)
        {
            for (int y = 0; y < grid_N; y++)
            {
                bool currentAlive = false;
                if(array[x,y].name == "AliveCell(Clone)")//checks if the selected cell is alive
                {
                    currentAlive = true;
                }

                if (aliveOrDead(x, y, currentAlive, array))//checks if the selected cell should be alive or dead in the next cycle
                {
                    meshRenderer = array[x, y].GetComponent<MeshRenderer>();
                    meshRenderer.sharedMaterial = aliveMaterial;
                    array[x, y].name = "AliveCell(Clone)";
                }
                else
                {
                    meshRenderer = array[x, y].GetComponent<MeshRenderer>();
                    meshRenderer.sharedMaterial = deadMaterial;
                    array[x, y].name = "DeadCell(Clone)";
                }
            }
        }
    }//updates the screen

	void Update () {

        new WaitForSeconds(0.1f);//sets the interval to 1/10 of a second
        checkNeigbors(gridArray);//fill the alternative array
        updateArray(gridArray);//update the screen
    }
}
