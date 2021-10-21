using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCreatorScript : MonoBehaviour
{
    private float StartDeathTime = 60f;
    public Transform[] spawnPoints;
    public GameObject slime; // prefab for dynamic slime creation
    public PlayerMovement player;
    public List<GameObject> slimes = new List<GameObject>();
    public float timeRemaining;
    private int frameCount = 1;
    private int lastRandom;

    public static int slimeSpreadSpeed = 2;

    void Start()
    {
        this.spawnSlime(true);
    }

    public void spawnSlime(bool isStart = false){
        for(int i = 0; i < Mathf.Abs(slimes.Count - getNumSlimes()); i++){
            timeRemaining = StartDeathTime;
        
            int index = Random.Range(0,spawnPoints.Length);

            if(index == lastRandom){ 
                if(index + 1 > spawnPoints.Length - 1)
                    index--;
                else
                    index++;
            }
            lastRandom = index;

            GameObject x = Instantiate(slime, spawnPoints[index]) as GameObject;
            x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, 0);
            x.transform.parent = transform;
            slimes.Add(x);

            SlimeScript sc = x.GetComponent<SlimeScript>();
            sc.setDeathTime(60 - (LevelTextScript.level + 5));
            StartDeathTime = 60 - (LevelTextScript.level + 5);

            player.addIndicator(x);

            if(isStart)
                break;
        }


        // if(isStart)
        //     spawnSlime();
    }

    int getNumSlimes(){
        return LevelTextScript.level + slimeSpreadSpeed;
    }

    public void RemoveSlime(GameObject s){
        timeRemaining = StartDeathTime;
        player.removeIndicator(s);
        slimes.Remove(s);
        Destroy(s);
        spawnSlime();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining <= 0){
            SlimeReset();
        }
    }

    void SlimeReset(){
        frameCount = 0;
        timeRemaining = StartDeathTime;
        GameObject temp = slimes[0];
        slimes.RemoveAt(0);
        Destroy(temp);
        spawnSlime();
    }

    void FixedUpdate()
    {
        if(frameCount == 0)
            timeRemaining--;
        
        if(frameCount <= 50)
            frameCount++;
        else
            frameCount = 0;
    }

    
}
