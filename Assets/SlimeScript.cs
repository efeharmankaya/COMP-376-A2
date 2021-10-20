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
            Destroy(this.gameObject);
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
}
