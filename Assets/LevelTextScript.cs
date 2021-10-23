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
            

            GameObject player = GameObject.Find("Player").gameObject;
            player.GetComponent<PlayerMovement>().Heal(); // heal player one heart per level increase
            player.GetComponent<Shooting>().shootSpeedCooldown += 0.25f; // increase shooting cooldown by 0.25seconds per level increase

            EnemyCreator em = GameObject.Find("EnemyCreator").gameObject.GetComponent<EnemyCreator>();
            em.enemyMoveSpeedIncrement += 1f; // Increase move speed of new spawned enemies by 1f
            em.increaseSpawns();
            em.baseCovidOdds += 0.1f;

            em.startLevelSpawning(); // Destroy all current enemies and start new spawning
        

        }
    }
}
