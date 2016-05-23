using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveText : MonoBehaviour {

    public static int WaveNumber;
    public static float WaveCounter;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        WaveNumber = 0;
    }

    void Update()
    {
        text.text = "Wave: " + WaveNumber + "\r\n" + "Next Wave In: " + WaveCounter;
    }
}
