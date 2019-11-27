using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The text where the pause and resume symbols are displayed.
    public Text pause;

    // The text where the 'game over' is displayed.
    public Text gameOver;

    // The current tetromino.
    public GameObject currentTetromino;

    // A fade-in scene transition effect.
    public GameObject fadeIn;

    // A fade-out scene transition effect.
    public GameObject fadeOut;

    // The next tetromino.
    GameObject nextTetromino;

    /* 
    * Start the game.
    */
    void Start()
    {
        fadeIn.SetActive(true);
        Time.timeScale = 1;
        currentTetromino = GetComponent<Tetromino>().SetAsCurrent(GetComponent<Tetromino>().SetNext());
        nextTetromino = GetComponent<Tetromino>().SetNext();
    }

    /* 
    * Check if one or more lines can be deleted and delete them.
    */
    public void CheckIfLinesDeleted()
    {
        int[] info = FindObjectOfType<TetrisGrid>().DeleteLines();

        if (info[0] > 0)
        {
            foreach (TetrominoMovement tm in FindObjectsOfType<TetrominoMovement>())
            {
                tm.Down(info[0], info[1]);
            }

            int score = 0;

            switch (info[0])
            {
                case 1:
                    score = 1000;
                    break;
                case 2:
                    score = 2500;
                    break;
                case 3:
                    score = 5000;
                    break;
                case 4:
                    score = 10000;
                    break;
            }

            GetComponent<Score>().EditScore(score);
            FindObjectOfType<AudioManager>().lineComplete.Play(0);
        }
    }

    /* 
    * Check if the game is over.
    * 
    * Return :
    *   bool - The result of the check.
    */
    public bool CheckIfGameOver()
    {
        if (FindObjectOfType<TetrisGrid>().IsBlockOutside())
        {
            gameOver.text = "GAME OVER";
            if (GetComponent<Score>().GetScore() > GetComponent<Save>().GetHighScore())
            {
                GetComponent<Save>().SaveHighScore(GetComponent<Score>().GetScore());
            }
        }

        return FindObjectOfType<TetrisGrid>().IsBlockOutside();
    }

    /* 
    * Create a new tetromino and set the next tetromino as the current one.
    */
    public void NewTetromino()
    {
        currentTetromino = GetComponent<Tetromino>().SetAsCurrent(nextTetromino);
        nextTetromino = GetComponent<Tetromino>().SetNext();
    }

    /* 
    * Restart the game.
    */
    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAfterDelay("Game"));
    }

    /* 
    * Pause or resume the game.
    */
    public void PauseOrResume()
    {
        FindObjectOfType<AudioManager>().buttonClicked.Play(0);
        if (pause.text == "II")
        {
            Time.timeScale = 0;
            pause.text = "▶";
            currentTetromino.GetComponent<TetrominoMovement>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pause.text = "II";
            currentTetromino.GetComponent<TetrominoMovement>().enabled = true;
        }
    }

    /*
    * Set the "Menu" scene.
    */
    public void BackToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAfterDelay("Menu"));
    }

    /*
    * Set the scene after a transition animation.
    * 
    * Parameters :
    *   string name - The scene name
    *   
    * Return :
    *   IEnumerator - Allow the use of a coroutine.
    */
    IEnumerator LoadAfterDelay(string name)
    {
        FindObjectOfType<AudioManager>().buttonClicked.Play(0);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
}
