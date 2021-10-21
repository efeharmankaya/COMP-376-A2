using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
External images/sprites used:
    https://thumbs.dreamstime.com/z/pixel-no-smoking-sign-27815252.jpg
    https://thumbs.dreamstime.com/b/vector-pixel-art-protective-mask-isolated-cartoon-148826982.jpg
    https://thumbs.dreamstime.com/z/syringe-blue-medicine-pixel-art-style-gray-74173847.jpg
    https://cdn5.vectorstock.com/i/thumb-large/99/44/pixel-art-8-bit-hazard-orange-sign-radiation-vector-27629944.jpg
    https://images.cdn4.stockunlimited.net/clipart/pixel-art-skull_1959058.jpg
*/

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float moveSpeed = 5f;
    public bool hasCovid;
    public bool hasMask;
    public bool hasVax;
    public bool isOld;

    private GameObject mask, vax, radiation, skull;
    private SpriteRenderer maskRenderer, vaxRenderer;
    public Sprite maskSprite, nomaskSprite, vaxSprite, novaxSprite;
    public CircleCollider2D circleCollider2D;
    bool coroutineAllowed = true;
    Vector2 movement;
    // public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        mask = transform.Find("mask").gameObject;
        vax = transform.Find("vax").gameObject;
        radiation = transform.Find("radiation").gameObject ;
        skull = transform.Find("skull").gameObject;

        maskRenderer = mask.GetComponent<SpriteRenderer>();
        vaxRenderer = vax.GetComponent<SpriteRenderer>();

        moveSpeed = hasCovid || isOld ? (moveSpeed/2) : (moveSpeed);

        changeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprites();
    }

    void UpdateSprites(){
        maskRenderer.sprite = hasMask ? maskSprite : nomaskSprite;
        vaxRenderer.sprite = hasVax ? vaxSprite : novaxSprite;

        radiation.gameObject.SetActive(hasCovid);
        skull.gameObject.SetActive(isOld);

        string tempTag;
        if(hasCovid)
            tempTag = "Infected";
        else if(isOld)
            tempTag = "Old";
        else if(hasVax)
            tempTag = "Vax";
        else
            tempTag = "Unvax";
        transform.gameObject.tag = tempTag;
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

    void OnCollisionEnter2D(Collision2D col){ // pathing + infecting other enemies
        if(!col.gameObject.tag.Equals("Player")){
            changeDirection();
            checkVirus(col);
        }
    }
    void OnCollisionStay2D(Collision2D col){ // pathing + infecting other enemies
        if(!col.gameObject.tag.Equals("Player")){
            changeDirection();
            checkVirus(col);
        }
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Player") && Vector2.Distance(transform.position, other.transform.position) <= 3f)
            checkPlayerVirus(other);
        else if(other.gameObject.tag.Equals("Slime"))
            checkSlimeVirus(other);
    }

    void checkPlayerVirus(Collider2D col){
        if(hasCovid){
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            playerMovement.getHurt();
        }
    }
    void checkSlimeVirus(Collider2D col){
        if(hasCovid)
            return;
        
        float odds = 0.05f;

        if(!hasVax)
            odds += 0.1f;
        if(!hasMask)
            odds += 0.2f;
        Debug.Log("in checkSlimeVirus odds: " + odds);
        if(Random.Range(0f,1f) <= odds){
            hasCovid = true;
            Debug.Log("CAUGHT COVID FROM SLIME");
        }
    }
    void checkVirus(Collision2D col){
        if(hasVax && hasMask) // mask + vax = fully protected
            return;
        
        // if(col.gameObject.tag.Equals(""))

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
