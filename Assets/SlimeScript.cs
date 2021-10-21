using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    private float maxTime = 60f;
    public float timeRemaining;

    private int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining <= 0){
            // Lose points
            MainScoreScript.score -= 1; // Lose points on slime time out death
            SlimeCreatorScript.slimeSpreadSpeed++; // Increase spread speed on time out death
            SlimeCreatorScript sc = transform.parent.gameObject.GetComponent<SlimeCreatorScript>();
            Destroy(this.gameObject);
            sc.spawnSlime();
        }
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

    void OnTriggerStay2D(Collider2D col){ // Slime cleaning
        if(col.gameObject.tag.Equals("Player") && Vector2.Distance(transform.position, col.transform.position) < 1f){
            // if(Input.GetKeyDown(KeyCode.Space))
            Debug.Log("PLAYER KILLED SLIME");
            MainScoreScript.score += 2;
            LevelTextScript.slimeKills++;
            GameObject.Find("SlimeCreator").GetComponent<SlimeCreatorScript>().RemoveSlime(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void setDeathTime(int deathTime){
        maxTime = deathTime;
    }
}
