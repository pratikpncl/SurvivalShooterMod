using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public int highScore = 0;
    string highScoreKey = "HighScore";

    Text text;


    void Awake ()
    {
        //Get the highScore from player prefs if it is there, 0 otherwise.
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        //Get text component to print scores
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        //Update score
        text.text = "Score: " + score + "        HighScore: " + highScore;
    }

    void OnDisable()
    {

        //If our scoree is greter than highscore, set new higscore and save.
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}
