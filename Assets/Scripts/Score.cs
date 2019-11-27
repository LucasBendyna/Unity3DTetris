using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // The text where the score is displayed.
    public Text scoreText;

    // The value of the score.
    int value = 0;

    /*
    * Edit the score.
    * 
    * Paramaters :
    *   int score - A value to add to the score.
    */
    public void EditScore(int score)
    {
        value += score;
        DisplayAddScore(score);
        scoreText.text = (int.Parse(scoreText.text) + score).ToString("0");
    }

    /*
    * Display the value to be added to the score.
    * 
    * Paramaters :
    *   int value - The value to display.
    */
    void DisplayAddScore(int value)
    {
        GameObject addScore = (GameObject)Resources.Load("Prefabs/AddScore");
        addScore = Instantiate(addScore, new Vector3(Random.Range(200, 600f), Random.Range(200f, 800f), 0), Quaternion.identity);
        addScore.transform.SetParent(GameObject.Find("UI").transform);
        addScore.GetComponent<AddScore>().EditValue(value);
    }

    /*
    * Get the score.
    * 
    * Return :
    *   int - The score.
    */
    public int GetScore()
    {
        return value;
    }

}
