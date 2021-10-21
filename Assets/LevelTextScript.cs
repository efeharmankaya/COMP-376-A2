using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelTextScript : MonoBehaviour
{
    public static int level = 1;
    Text levelText;


    // Level params
    public static int slimeKills = 0;
    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + level;
        if(slimeKills >= (level  * 5 )){ // 5 * #level to increase level
            slimeKills = 0;
            level++;
            GameObject.Find("Player").gameObject.GetComponent<PlayerMovement>().Heal();
            EnemyCreator em = GameObject.Find("EnemyCreator").gameObject.GetComponent<EnemyCreator>();
            em.startLevelSpawning(); // Destroy all current enemies and start new spawning
        }
    }
}
