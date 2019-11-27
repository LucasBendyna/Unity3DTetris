using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour
{
    // The text where the high score is displayed.
    public Text highScoreValue;

    // The high score.
    HighScore highScore;

    /*
    * Initialize the high score by checking the existence of a save file.
    */
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/highscore.save"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highscore.save", FileMode.Open);
            HighScore hs = (HighScore) bf.Deserialize(file);
            file.Close();

            highScoreValue.text = hs.value.ToString();
            highScore = hs;
        }
        else
        {
            highScore = new HighScore();
            highScore.value = 0;
            highScoreValue.text = "0";
        }
    }

    /*
    * Save a new high score.
    * 
    * Parameters :
    *   int score - The new high score.
    */
    public void SaveHighScore(int score)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/highscore.save");

        highScore.value = score;

        bf.Serialize(file, highScore);
        file.Close();

        highScoreValue.text = score.ToString(); ;
    }

    /*
    * Get the high score.
    * 
    * Return :
    *   int - The high score.
    */
    public int GetHighScore()
    {
        return highScore.value;
    }
}
