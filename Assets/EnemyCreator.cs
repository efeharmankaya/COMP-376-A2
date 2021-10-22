using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    // public SpriteRenderer spriteRenderer;
    // public Sprite EnemyMasked;
    // public Sprite EnemyUnmasked;
    // public Sprite EnemyInfected;

    // prefabs
    public GameObject EnemyBase;
    // public GameObject UnvaxEnemy; // 2 versions( masked / unmasked )
    // public GameObject OldEnemy; // highly susceptable

    // // Enemy Params
    // public float moveSpeed = 5f;
    // public bool hasCovid;
    // public bool hasMask;
    // public bool hasVax;
    // public bool isOld;

    // TEMP 
    public Transform spawnTest;

    public Transform[] spawns;

    // Spawning params
    public int[] spawnNumbers = {
        2, // Vax
        1, // Unvax mask
        2, // Unvax no mask
        1, // Old (high risk)
        0, // Infected
    };

    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        startLevelSpawning();
    }

    // TODO???: make static to be called from other script????
    public void startLevelSpawning(){
        Debug.Log("NEW LEVEL SPAWNING #" + LevelTextScript.level);
        foreach(GameObject e in enemies){ Destroy(e); }
        enemies.Clear();

        for(int i = 0; i < spawnNumbers.Length; i++){
            int n = spawnNumbers[i]; // amount of current index enemy to spawn
            switch(i){
                case 0: // vax enemy
                    createEnemy(n, "Vax", true, true, false, false);
                    break;
                case 1: // unvax masked enemy
                    createEnemy(n, "Unvax", false, true, false, false);
                    break;
                case 2: // unvax no mask enemy
                    createEnemy(n, "Unvax", false, false, false, false);
                    break;
                case 3: // old enemy
                    createEnemy(n, "Old", false, true, true, false);
                    break;
                case 4: // infected enemy
                    createEnemy(n, "Infected", false, false, false, true);
                    break;
            }
        }
    }

    private void createEnemy(int n, string tag, bool vax, bool mask, bool old, bool covid){
        for(int i = 0; i < n; i++){
            GameObject enemy = Instantiate(EnemyBase, spawns[Random.Range(0, spawns.Length)]);
            EnemyScript es = enemy.GetComponent<EnemyScript>();
            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
            es.hasVax = vax;
            es.hasMask = mask;
            es.isOld = old;
            es.hasCovid = covid;

            // Changed: removed other descriptive tags, only Infected / Enemy now
            enemy.tag = covid ? "Infected" : "Enemy"; // Double check correct tag for infected
            enemies.Add(enemy);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
