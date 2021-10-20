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

    void Start()
    {
        this.spawnSlime();
    }

    public void spawnSlime(){
        timeRemaining = StartDeathTime;
        GameObject x = Instantiate(slime, spawnPoints[Random.Range(0,spawnPoints.Length)]) as GameObject;
        x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, 0);
        slimes.Add(x);

        // player.playerHealth = 1;
        player.addIndicator(x);
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
