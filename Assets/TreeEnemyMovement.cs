using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float startWaitTime = 1;
    private float waitTime = 1;
    
    // Set of spots the Enemy will move
    public Transform[] moveSpots;
    public Animator animator;
    Vector2 movement;
    public int randomSpot;

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Called 50 times per second
    void FixedUpdate()
    {
        // Movement
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.5f){
            if(waitTime <= 0){
                // Find new spot to move to
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }else{
                // Continue waiting
                waitTime -= Time.deltaTime;
            }
        }
    }
}
