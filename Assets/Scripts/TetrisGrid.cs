using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    // The tetris grid.
    GameObject[,] grid = new GameObject[10, 25];

    // The positions of the blocks of the current tetromino.
    List<int[,]> currentTetrominoPosition = new List<int[,]>();

    // The x position of the pivot block of the current tetromino.
    int pivotXPos;

    // The y position of the pivot block of the current tetromino.
    int pivotYPos;

    /* 
    * Add a tetromino to the grid.
    * 
    * Parameters :
    *   GameObject tetromino - A tetromino.
    */
    public void AddTetromino(GameObject tetromino)
    {
        currentTetrominoPosition.Clear();
        GameObject obj = tetromino.transform.GetChild(0).gameObject;
        for (int i = 0; i < 4; i++)
        {
            GameObject block = obj.transform.GetChild(i).gameObject;
            int x = (int) Mathf.Round(block.transform.position.x);
            int y = (int) Mathf.Round(block.transform.position.y);
            currentTetrominoPosition.Add(new int[,] { { x, y } });
            grid[x, y] = block;
        }
        SetPivotPosition(tetromino);
    }

    /* 
    * Set the pivot position of a tetromino.
    * 
    * Parameters :
    *   GameObject tetromino - A tetromino.
    */
    public void SetPivotPosition(GameObject tetromino)
    {
        if (tetromino.tag == "I")
        {
            pivotXPos = 4;
            pivotYPos = 23;
        }
        else
        {
            pivotXPos = 5;
            pivotYPos = 23;
        }
    }

    /* 
    * Move the current tetromino in a direction inside the grid.
    * 
    * Parameters :
    *   string direction - The direction in which we want to move.
    *   
    * Return :
    *   bool - Verify that the movement has been made.
    */
    public bool MoveTetromino(string direction)
    {
        switch (direction)
        {
            case "down":
                return Down();

            case "right":
                return Right();

            case "left":
                return Left();
            default:
                return false;
        }
    }

    /* 
    * Move the current tetromino down inside the grid.
    */
    bool Down()
    {
        bool test = true;

        foreach (int[,] coordinates in currentTetrominoPosition)
        {

            int x = coordinates[0, 0];
            int y = coordinates[0, 1];

            if (!IsBlockUnder(x, y) && (y - 1 < 0 || grid[x, y - 1] != null))
            {
                test = false;
            }
        }

        if (test)
        {
            List<int[,]> copy = new List<int[,]>();
            copy.AddRange(currentTetrominoPosition);
            EditCurrentTetrominoPosition(copy, 0, -1); ;
        }

        return test;
    }

    /* 
    * Move the current tetromino right inside the grid.
    */
    bool Right()
    {
        bool test = true;

        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            int x = coordinates[0, 0];
            int y = coordinates[0, 1];

            if (!IsBlockRight(x, y) && (x + 1 > 9 || grid[x + 1, y] != null))
            {
                test = false;
            }
        }

        if (test)
        {
            List<int[,]> copy = new List<int[,]>();
            copy.AddRange(currentTetrominoPosition);
            EditCurrentTetrominoPosition(copy, 1, 0); ;
        }

        return test;
    }

    /* 
    * Move the current tetromino left inside the grid.
    */
    bool Left()
    {
        bool test = true;

        foreach (int[,] coordinates in currentTetrominoPosition)
        {

            int x = coordinates[0, 0];
            int y = coordinates[0, 1];

            if (!IsBlockLeft(x, y) && (x - 1 < 0 || grid[x - 1, y] != null))
            {
                test = false;
            }
        }

        if (test)
        {
            List<int[,]> copy = new List<int[,]>();
            copy.AddRange(currentTetrominoPosition);
            EditCurrentTetrominoPosition(copy, -1, 0);
        }

        return test;
    }

    /* 
    * Edit the position of the current tetromino in the grid after a movement.
    * 
    * Parameters :
    *   List<int[,]> list - A list containing the positions of the blocks of the current tetromino.
    *   int xOffset - Offset value of the x position of the pivot block.
    *   int yOffset - Offset value of the y position of the pivot block.
    */
    void EditCurrentTetrominoPosition(List<int[,]> list, int xOffset, int yOffset)
    {
        currentTetrominoPosition.Clear();

        GameObject[] go = new GameObject[4];
        List<int[,]> oldPosition = new List<int[,]>();
        int i = 0;

        foreach (int[,] coordinates in list)
        {
            int x = coordinates[0, 0];
            int y = coordinates[0, 1];

            go[i] = grid[x, y];
            oldPosition.Add(new int[,] { { x, y } });

            grid[x, y] = null;
            i++;
        }

        i = 0;

        foreach (int[,] pos in oldPosition)
        {
            grid[pos[0, 0] + xOffset, pos[0, 1] + yOffset] = go[i];
            currentTetrominoPosition.Add(new int[,] { { pos[0, 0] + xOffset, pos[0, 1] + yOffset } });
            i++;
        }

        pivotXPos += xOffset;
        pivotYPos += yOffset;
    }

    /* 
    * Check if the coordinates passed in parameters correspond to that of a block under the current tetromino. 
    * 
    * Parameters :
    *   int x - A x position to test.
    *   int y - A y position to test.
    *   
    * Return :
    *   bool - The result of the check.
    */
    bool IsBlockUnder(int x, int y)
    {
        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            int tmpX = coordinates[0, 0];
            int tmpY = coordinates[0, 1];
            
            if (x == tmpX && (y == tmpY + 1))
            {
                return true;
            }
        }

        return false;
    }

    /* 
    * Check if the coordinates passed in parameters correspond to that of a block on the right of the current tetromino. 
    * 
    * Parameters :
    *   int x - A x position to test.
    *   int y - A y position to test.
    *   
    * Return :
    *   bool - The result of the check.
    */
    bool IsBlockRight(int x, int y)
    {
        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            int tmpX = coordinates[0, 0];
            int tmpY = coordinates[0, 1];

            if (x == (tmpX - 1) && y == tmpY)
            {
                return true;
            }
        }

        return false;
    }

    /* 
    * Check if the coordinates passed in parameters correspond to that of a block on the left of the current tetromino. 
    * 
    * Parameters :
    *   int x - A x position to test.
    *   int y - A y position to test.
    *   
    * Return :
    *   bool - The result of the check.
    */
    bool IsBlockLeft(int x, int y)
    {
        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            int tmpX = coordinates[0, 0];
            int tmpY = coordinates[0, 1];

            if (x == (tmpX + 1) && y == tmpY)
            {
                return true;
            }
        }

        return false;
    }

    /* 
    * Remove a completed line from the grid.
    * 
    * Return :
    *   int[] - Information on the number of deleted lines and the minimum y position of the deleted lines.
    */
    public int[] DeleteLines()
    {
        List<int> lines = LinesComplete();
        int[] info = new int[2];
        info[0] = 0;
        int min = 24;
        foreach (int line in lines)
        {
            info[0]++;
            for (int i = 0; i < 10; i++)
            {
                GameObject go = grid[i, line];
                Destroy(go);
                grid[i, line] = null;
            }

            if (line < min)
            {
                min = line;
            }
        }
        info[1] = min;
        DownEachBlock(info);
        return info;
    }

    /* 
    * Find the completed lines in the grid.
    * 
    * Return :
    *   List<int> - The list of completed lines.
    */
    public List<int> LinesComplete()
    {
        List<int> lines = new List<int>();
        for(int j = 0; j < 25; j++)
        {
            bool test = true;
            for (int i = 0; i < 10; i++)
            {
                if (grid[i, j] == null)
                {
                    test = false;
                }
            }

            if (test)
            {
                lines.Add(j);
            }
        }

        return lines;
    }

    /* 
    * Lower the blocks position whose is higher than 'info[0]',  'info[1]' times.
    * 
    * Parameters :
    *   int[] info - Information on the number of deleted lines and the minimum y position of the deleted lines.
    */
    void DownEachBlock(int[] info)
    {
        for (int number = 0; number < info[0]; number++)
        {
            for (int j = info[1] + 1; j < 25; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (grid[i, j] != null)
                    {
                        GameObject go = grid[i, j];
                        grid[i, j] = null;
                        grid[i, j - 1] = go;
                    }
                }            
            }
        }
    }

    /* 
    * Rotate the current tetromino in a direction inside the grid.
    * 
    * Parameters :
    *   string tag - The tag of the tetromino to rotate.
    *   
    * Return :
    *   int - Angle of rotation to be performed.
    */
    public int Rotate(string tag)
    {
        if (tag == "O")
        {
            return 0;
        }
        else
        {
            int rotation = 0;

            List<int[,]> list1 = new List<int[,]>();
            List<int[,]> list2 = new List<int[,]>();
            list2.AddRange(currentTetrominoPosition);

            bool test = false;

            while (rotation != 360 && !test)
            {
                test = true;
                list1.Clear();
                list1.AddRange(list2);
                list2.Clear();

                foreach (int[,] coordinates in list1)
                {
                    int oldX = coordinates[0, 0];
                    int oldY = coordinates[0, 1];

                    int newX = pivotXPos + pivotYPos - oldY;
                    int newY = oldX + pivotYPos - pivotXPos;

                    list2.Add(new int[,] { { newX, newY } });

                    if (!IsInCurrentTetromino(newX, newY) && (newX > 9 || newX < 0 || newY < 0 || grid[newX, newY] != null))
                    {
                        test = false;
                    }
                }
                rotation += 90;
            }

            if (test)
            {
                EditCurrentTetrominoRotation(list2);
            }

            return rotation;
        }
    }

    /* 
    * Check if the coordinates passed in parameters are in the current tetromino. 
    * 
    * Parameters :
    *   int x - A x position to test.
    *   int y - A y position to test.
    *   
    * Return :
    *   bool - The result of the check.
    */
    bool IsInCurrentTetromino(int x, int y)
    {
        bool test = false;

        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            if (x == coordinates[0,0] && y == coordinates[0, 1])
            {
                test = true;
            }
        }

        return test;
    }

    /* 
    * Edit the position of the current tetromino in the grid after a rotation.
    * 
    * Parameters :
    *   List<int[,]> list - A list containing the new positions of the current tetromino.
    */
    void EditCurrentTetrominoRotation(List<int[,]> list)
    {
        GameObject[] go = new GameObject[4];
        int i = 0;

        foreach (int[,] coordinates in currentTetrominoPosition)
        {
            int x = coordinates[0, 0];
            int y = coordinates[0, 1];

            go[i] = grid[x, y];
            grid[x, y] = null;
            i++;
        }

        i = 0;
        currentTetrominoPosition.Clear();

        foreach (int[,] coordinates in list)
        {
            grid[coordinates[0, 0], coordinates[0, 1]] = go[i];
            currentTetrominoPosition.Add(new int[,] { { coordinates[0, 0], coordinates[0, 1] } });
            i++;
        }
    }

    /* 
    * Check if the coordinates passed in parameters are outside the grid. 
    * 
    * Parameters :
    *   int x - A x position to test.
    *   int y - A y position to test.
    *   
    * Return :
    *   bool - The result of the check.
    */
    public bool IsBlockOutside()
    {
        bool test = false;

        for (int j = 22; j < 25; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                if (grid[i, j] != null)
                {
                    test = true;
                }
            }
        }

        return test;
    }
}
