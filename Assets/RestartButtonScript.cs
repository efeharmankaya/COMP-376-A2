using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Text https://www.youtube.com/watch?v=JC59tDg4tmo
public class RestartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    {
        // Reset all static variables
        MainScoreScript.score = 0;
        LevelTextScript.level = 1;
        LevelTextScript.slimeKills = 0;
        // SlimeCreatorScript.StartDeathTime = 60f;
        SlimeCreatorScript.slimeSpreadSpeed = 2;
        SceneManager.LoadScene("SampleScene");
    }
}
