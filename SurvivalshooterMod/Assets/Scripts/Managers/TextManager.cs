using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour
{
    public static int WaveNumber;

    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        WaveNumber = 0;
    }


    void Update()
    {
        text.text = "Wave: " + WaveNumber;
    }
}
