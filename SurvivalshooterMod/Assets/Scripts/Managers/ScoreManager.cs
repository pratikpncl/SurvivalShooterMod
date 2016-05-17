using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        //Get text 
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        //Update score
        text.text = "Score: " + score;
    }
}
