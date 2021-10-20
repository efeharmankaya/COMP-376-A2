using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Pathing https://www.youtube.com/watch?v=8eWbSN2T8TE

public class TreeEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float startWaitTime = 1;
    private float waitTime = 1;
    
    // Set of spots the Enemy will move
    public Transform[] moveSpots;
    public Animator animator;
    public int index;

    void Start()
    {
        index = 0;
    }

    void Update()
    {
        animator.SetFloat("Horizontal", moveSpots[index].position.x - transform.position.x);
        animator.SetFloat("Vertical", moveSpots[index].position.y - transform.position.y);
    }

    // Called 50 times per second
    void FixedUpdate()
    {
        // Movement
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[index].position, moveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[index].position) < 0.5f){
            if(waitTime <= 0){
                // Find new spot to move to
                if(++index > moveSpots.Length -1)
                    index = 0;
                waitTime = startWaitTime;
                animator.SetFloat("Speed", 1);
            }else{
                // Continue waiting
                waitTime -= Time.deltaTime;
                animator.SetFloat("Speed", 0);
            }
        }else{
            animator.SetFloat("Speed", 1);
        }
    }

    // void OnCollisionEnter2D(Collision2D col){
    //     if(col.gameObject.tag.Equals("Wall")){
    //         randomSpot = Random.Range(0, moveSpots.Length);
    //     }
    // }
}
