using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collision + Text https://www.youtube.com/watch?v=JC59tDg4tmo

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;

    public SlimeCreatorScript slimeSpawner;
    public GameObject indicatorPrefab;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    public GameObject gameOverText, restartButton;
    /*
        Controlling lives
    */
    public GameObject heart1, heart2, heart3;
    public int playerHealth = 3;
    int playerLayer, enemyLayer; // temporary collision removal
    bool coroutineAllowed = true;
    Color color;
    Renderer renderer;

    public List<TargetIndicator> indicators = new List<TargetIndicator>();

    void Start()
    {
        gameOverText.SetActive(false);
        restartButton.SetActive(false);

        playerLayer = this.gameObject.layer;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        
        heart1 = GameObject.Find("heart1");
        heart2 = GameObject.Find("heart2");
        heart3 = GameObject.Find("heart3");
        
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);

        renderer = GetComponent<Renderer>();
        color = renderer.material.color;

        
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // if(indicators.Count != slimeSpawner.slimes.Count){
        //     indicators.Clear();
        //     foreach(GameObject s in slimeSpawner.slimes){
                // GameObject i = Instantiate(indicatorPrefab) as GameObject;
                // TargetIndicator t = i.GetComponent<TargetIndicator>();
                // t.setTarget(s);
                // // i.setTarget(s);
                // indicators.Add(i);
        //     }
        // }
           
    }

    public void addIndicator(GameObject s){
        GameObject i = Instantiate(indicatorPrefab, transform) as GameObject;
        TargetIndicator t = i.GetComponent<TargetIndicator>();
        t.setTarget(s);
        // i.transform.position = new Vector3(0,0,0);
        i.transform.parent = transform;
        indicators.Add(t);
    }

    public void removeIndicator(GameObject s){
        foreach(TargetIndicator i in indicators){
            if(i.Target == s){
                indicators.Remove(i);
                break;
            }
        }
    }


    // Called 50 times per second
    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("Enemy")){
            col.gameObject.transform.Rotate(0,0,0);

            playerHealth -= 1;
            switch(playerHealth){
                case 2:
                    heart3.gameObject.SetActive(false);
                    if(coroutineAllowed){ StartCoroutine("Immortal"); }
                    break;
                case 1:
                    heart2.gameObject.SetActive(false);
                    if(coroutineAllowed){ StartCoroutine("Immortal"); }
                    break;
                case 0:
                    heart1.gameObject.SetActive(false);
                    if(coroutineAllowed){ StartCoroutine("Immortal"); }
                    break;
            }
            
            if(playerHealth < 1){
                gameOverText.SetActive(true);
                restartButton.SetActive(true);
                gameObject.SetActive(false);
            }
            
        }
    }

    void OnTriggerStay2D(Collider2D col){ // Slime cleaning
        if(col.gameObject.tag.Equals("Slime")){
            // if(Input.GetKeyDown(KeyCode.Space))
            slimeSpawner.RemoveSlime(col.gameObject);
            Destroy(col.gameObject);
        }
    }

    IEnumerator Immortal()
    {
        coroutineAllowed = false; // mutex lock
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        color.a = 0.5f;
        renderer.material.color = color;
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        color.a = 1f;
        renderer.material.color = color;
        coroutineAllowed = true; // mutex unlock
    }
}
