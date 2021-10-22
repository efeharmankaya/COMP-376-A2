using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collision + Text https://www.youtube.com/watch?v=JC59tDg4tmo
// Shooting https://www.youtube.com/watch?v=LNLVOjbrQj4
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;

    public SlimeCreatorScript slimeSpawner;
    public GameObject indicatorPrefab;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    Vector2 movement;
    Vector2 mousePos;

    public GameObject gameOverText, restartButton;
    /*
        Controlling lives
    */
    public GameObject heart1, heart2, heart3;
    public int playerHealth = 3;
    int playerLayer, enemyLayer; // temporary collision removal
    public bool coroutineAllowed = true;
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



    public void addIndicator(GameObject s){
        GameObject i = Instantiate(indicatorPrefab, transform) as GameObject;
        TargetIndicator t = i.GetComponent<TargetIndicator>();
        t.setTarget(s);
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

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
           
    }
    
    // Called 50 times per second
    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        // GameObject.Find("FirePoint").gameObject.GetComponent<Rigidbody2D>().rotation = angle;
        GameObject.Find("FirePoint").gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // rb.rotation = angle;

        if(MainScoreScript.score <= -20){
            playerHealth = 0;
            getHurt();
        }
    }

    // void OnCollisionEnter2D(Collision2D col){
    //     // if(col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Infected")){
    //     if(col.gameObject.tag.Equals("Infected")){
    //         col.gameObject.transform.Rotate(0,0,0);
    //         getHurt();
    //     }
    //     // else if(col.gameObject.tag.Equals("Wall")){
    //     //     //
    //     // }
    //     // else if(col.gameObject.tag.Equals("")){
    //     //     //
    //     // }
    // }

    public void getHurt(){
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
        }else{
            MainScoreScript.score -= 7;
        }
    }

    public void Heal(){
        if(playerHealth >= 3)
            return;

        playerHealth++;

        switch(playerHealth){
            case 3:
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart2.gameObject.SetActive(true);
                break;
        }
    }

    // void OnTriggerStay2D(Collider2D col){ // Slime cleaning
    //     if(col.gameObject.tag.Equals("Infected") && Input.GetKeyDown(KeyCode.Space)){
    //         Debug.Log("Giving Mask to Infected");
    //     }
    // }

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
