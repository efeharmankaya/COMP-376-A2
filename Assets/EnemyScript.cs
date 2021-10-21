using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float moveSpeed = 5f;
    public bool hasCovid;
    public bool hasMask;
    public bool hasVax;
    public bool isOld;



    bool coroutineAllowed = true;
    Vector2 movement;
    // public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        changeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void changeDirection(){
        // if(coroutineAllowed)
            StartCoroutine(Wait());
        // StartCoroutine(Wait());
        // movement.x = Random.Range(-1,1);
        // movement.y = Random.Range(-1,1);
        
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void OnCollisionEnter2D(Collision2D col){
        // if(col.gameObject.tag.Equals("Wall") || col.gameObject.tag.Equals("Enemy")){
        if(!col.gameObject.tag.Equals("Player"))
            changeDirection();
    }

    void OnCollisionStay2D(Collision2D col){
        // if(col.gameObject.tag.Equals("Wall") || col.gameObject.tag.Equals("Enemy")){
        if(!col.gameObject.tag.Equals("Player"))
            changeDirection();
    }

    IEnumerator Wait(){
        coroutineAllowed = false;
        movement.x = 0;
        movement.y = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        movement.x = Random.Range(-1f,1f);
        movement.y = Random.Range(-1f,1f);
        coroutineAllowed = true;
    }
}
