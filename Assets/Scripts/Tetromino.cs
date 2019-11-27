using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    /* 
    * Generate a tetromino.
    * 
    * Return :
    *   GameObject - A newly generated tetromino.
    */
    public GameObject GenTetromino()
    {
        int rand = Random.Range(0, 7);

        GameObject tetromino = null;

        switch (rand)
        {
            case 0:
                tetromino = (GameObject)Resources.Load("Prefabs/I");
                break;
            case 1:
                tetromino = (GameObject)Resources.Load("Prefabs/J");
                break;
            case 2:
                tetromino = (GameObject)Resources.Load("Prefabs/L");
                break;
            case 3:
                tetromino = (GameObject)Resources.Load("Prefabs/O");
                break;
            case 4:
                tetromino = (GameObject)Resources.Load("Prefabs/S");
                break;
            case 5:
                tetromino = (GameObject)Resources.Load("Prefabs/T");
                break;
            case 6:
                tetromino = (GameObject)Resources.Load("Prefabs/Z");
                break;
        }

        return tetromino;
    }

    /* 
    * Generate a tetromino and position it as the next one.
    * 
    * Return :
    *   GameObject - The next tetromino.
    */
    public GameObject SetNext()
    {
        GameObject tetromino = GenTetromino();
        tetromino.GetComponent<TetrominoMovement>().enabled = false;
        return Instantiate(tetromino, GetNextPoint(tetromino), Quaternion.identity);
    }

    /* 
    * Generate the coordinates of the next tetromino according to its tag.
    * 
    * Parameters :
    *   GameObject tetromino - The next tetromino.
    * 
    * Return :
    *   Vector3 - The coordinates of the next tetromino.
    */
    Vector3 GetNextPoint(GameObject tetromino)
    {
        switch (tetromino.tag)
        {
            case "O":
                return new Vector3(21f, 22f, 0);
            case "I":
                return new Vector3(21f, 22f, 0);
            default:
                return new Vector3(21.5f, 22f, 0);
        }
    }

    /* 
    * Set the next tetromino as the current one.
    * 
    * Parameters :
    *   GameObject tetromino - The next tetromino.
    *   
    * Return :
    *   GameObject - The current tetromino.
    */
    public GameObject SetAsCurrent(GameObject tetromino)
    {
        tetromino.transform.position = GetSpawnPoint(tetromino);
        tetromino.GetComponent<TetrominoMovement>().enabled = true;
        FindObjectOfType<TetrisGrid>().AddTetromino(tetromino);
        return tetromino;
    }

    /* 
    * Generate the coordinates of the current tetromino according to its tag.
    * 
    * Parameters :
    *   GameObject tetromino - The current tetromino.
    * 
    * Return :
    *   Vector3 - The coordinates of the current tetromino.
    */
    Vector3 GetSpawnPoint(GameObject tetromino)
    {
        switch (tetromino.tag)
        {
            case "O":
                return new Vector3(4f, 23f, 0);
            case "I":
                return new Vector3(4f, 23f, 0);
            default:
                return new Vector3(5f, 23f, 0);
        }
    }
}
