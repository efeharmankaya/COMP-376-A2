using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCreatorScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    private float timeLimit = 10f;
    // private GameObject[] slimes;
    public GameObject slime;
    // Start is called before the first frame update

    public List<GameObject> slimes = new List<GameObject>();
    public float timeRemaining = 10f;
    private int frameCount = 1;

    void Start()
    {
        this.spawnSlime();
    }

    public void spawnSlime(){
        GameObject x = Instantiate(slime, spawnPoints[Random.Range(0,spawnPoints.Length)]) as GameObject;
        x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, 0);
        slimes.Add(x);
    }

    public void RemoveSlime(GameObject s){
        timeRemaining = 10f;
        slimes.Remove(s);
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
        timeRemaining = 10f;
        slimes.RemoveAt(0);
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
