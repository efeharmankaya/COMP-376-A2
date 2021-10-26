using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://www.youtube.com/watch?v=0VGosgaoTsw
public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.3f;
    public float slowdownLength = 3f;
    float baseSpeed = 5f;
    public List<GameObject> enemies = new List<GameObject>();

    void Update() {
        // Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;   
        // Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        foreach(GameObject enemy in enemies){
            EnemyScript es = enemy.GetComponent<EnemyScript>();
            es.moveSpeed += (1f / slowdownLength) * Time.deltaTime;
            es.moveSpeed = Mathf.Clamp(es.moveSpeed, 0f, baseSpeed + GameObject.Find("EnemyCreator").GetComponent<EnemyCreator>().enemyMoveSpeedIncrement);
        }
    }

    public void SlowMotion(){
        enemies = GameObject.Find("EnemyCreator").GetComponent<EnemyCreator>().enemies;
        foreach(GameObject enemy in enemies){
            enemy.GetComponent<EnemyScript>().moveSpeed = 1f;
        }
        // Time.timeScale = slowdownFactor;
        // Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }
    
    // Working time slow
    // public float slowdownFactor = 0.3f;
    // public float slowdownLength = 3f;

    // void Update() {
    //     Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;   
    //     Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    // }

    // public void SlowMotion(){
    //     Time.timeScale = slowdownFactor;
    //     Time.fixedDeltaTime = Time.timeScale * 0.2f;
    // }
}
