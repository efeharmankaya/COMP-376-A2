using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Score text https://www.youtube.com/watch?v=QbqnDbexrCw
public class MainScoreScript : MonoBehaviour
{
    public static int score = 0;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
