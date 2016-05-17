using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int highscore;
    string highscorekey = "highscore";

    Text text;
    
    void Awake ()
    {
        //Get text from UI and set up variables for high score
        text = GetComponent <Text> ();
        score = 0;
        highscore = PlayerPrefs.GetInt(highscorekey,0);
    }


    void Update ()
    {
        //Update score & highscore
        text.text = "Score: " + score + "        HighScore: " + highscore;
    }

    void OnDisable()
    {
        //If our scoree is greter than highscore, set new higscore and save.
        if (score > highscore)
        {
            PlayerPrefs.SetInt(highscorekey, score);
            PlayerPrefs.Save();
        }
    }
    
}

