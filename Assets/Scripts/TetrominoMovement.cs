using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TetrominoMovement : MonoBehaviour
{
    // The falling speed of tetrominoes.
    float infinity = 0.5f;

    // Check if the tetromino can go down
    bool goDown = false;

    // The cooldown of right and left actions.
    float cooldownAction;

    // The cooldown of down action.
    float cooldownAccelerate;

    // Check if the cooldown of right, left and rotation actions is up.
    bool readyAction = true;

    // Check if the cooldown of down actions is up.
    bool readyAccelerate = true;

    /* 
    * Update the tetromino movement according to the user inputs.
    */
    void Update()
    {
        ResetCooldownAction();
        ResetCooldownAccelerate();
        Tick();
        
        if (goDown)
        {
            Fall();
        }

        if ((Input.GetKey("d") || Input.GetKey("right")) && readyAction && FindObjectOfType<TetrisGrid>().MoveTetromino("right"))
        {
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            cooldownAction = 0.125f;
            readyAction = false;
        }

        if ((Input.GetKey("q") || Input.GetKey("left")) && readyAction && FindObjectOfType<TetrisGrid>().MoveTetromino("left"))
        {
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            cooldownAction = 0.125f;
            readyAction = false;
        }

        if ((Input.GetKey("s") || Input.GetKey("down")) && readyAccelerate)
        {
            infinity = -1f;
            cooldownAccelerate = 0.06f;
            readyAccelerate = false;
        }

        if ((Input.GetKey("z") || Input.GetKey("up") || Input.GetKey("space")) && readyAction)
        {
            transform.Rotate(0, 0, FindObjectOfType<TetrisGrid>().Rotate(tag));
            cooldownAction = 0.125f;
            readyAction = false;
        }
    }

    /* 
    * Reset the cooldown of right, left and rotation actions.
    */
    void ResetCooldownAction()
    {
        if (cooldownAction > 0)
        {
            cooldownAction -= Time.deltaTime;
        }
        if (cooldownAction < 0)
        {
            cooldownAction = 0;
            readyAction = true;
        }
    }

    /* 
    * Reset the cooldown of down action.
    */
    void ResetCooldownAccelerate()
    {
        if (cooldownAccelerate > 0)
        {
            cooldownAccelerate -= Time.deltaTime;
        }
        if (cooldownAccelerate < 0)
        {
            cooldownAccelerate = 0;
            readyAccelerate = true;
        }
    }

    /* 
    * Advance the game state by one tick
    */
    void Tick()
    {
        if (infinity > 0)
        {
            infinity -= Time.deltaTime;
        }
        if (infinity < 0)
        {
            infinity = 0;
            goDown = true;
        }
    }

    /* 
    * Lower the tetromino.
    */
    void Fall()
    {
        if (FindObjectOfType<TetrisGrid>().MoveTetromino("down"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        }
        else
        {
            enabled = false;
            readyAction = false;

            Snap();
            if (!FindObjectOfType<GameManager>().CheckIfGameOver())
            {
                FindObjectOfType<GameManager>().CheckIfLinesDeleted();
                FindObjectOfType<GameManager>().NewTetromino();
                FindObjectOfType<Score>().EditScore(100);
                FindObjectOfType<AudioManager>().tetrominoPlaced.Play(0);
            }
            return;
        }
        infinity = 0.5f;
        goDown = false;
    }

    /* 
    * Ensure that the tetromino is properly placed on the ground.
    */
    void Snap()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
    }


    /* 
    * Lower the blocks whose position is higher than 'y',  'n' times.
    * 
    * Parameters :
    *   int number - Number of times the blocks have to be lowered
    *   int y - Position in y from which the blocks must be lowered.
    */
    public void Down(int number, int y)
    {
        int blocks = gameObject.transform.GetChild(0).transform.childCount;

        for (int i = 0; i < number; i++)
        {
            for (int j = 0; j < blocks; j++)
            {
                Transform block = gameObject.transform.GetChild(0).transform.GetChild(j);

                if (Mathf.Round(block.position.y) > y && Mathf.Round(block.position.x) < 10)
                {
                    block.position = new Vector3(block.position.x, block.position.y - 1f, block.position.z);
                }
            }
        }
    }
}
