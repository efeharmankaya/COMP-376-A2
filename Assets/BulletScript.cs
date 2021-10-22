using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // public string bulletType;
    
    public int playerLayer, bulletLayer;
    void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
        bulletLayer = this.gameObject.layer;
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, true);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Wall"))
            Destroy(gameObject);
        if(other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Infected")){
            CheckBulletCollision(other);
        }
    }

    void CheckBulletCollision(Collision2D other){
        bool delete = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // Check enemy params
        GameObject enemy = other.gameObject;
        EnemyScript es = enemy.GetComponent<EnemyScript>();
        switch(transform.tag){
            case "QuarantineBullet":
                // TODO: move enemy out of map w/ touches spawning slime
                if(!es.hasCovid) // Skip if enemy doesn't have covid
                    break;
                
                // TEMP
                es.hasCovid = false;
                delete = true;
                break;
            case "VaxBullet":
                if(!es.hasVax) // check if the bullet is used
                    delete = true;
                es.hasVax = true;
                break;
            case "MaskBullet":
                if(!es.hasMask) // check if the bullet is used
                    delete = true;
                es.hasMask = true;
                break;
        }
        if(delete)
            Destroy(gameObject);
        else
            StartCoroutine(WaitForDelete());
    }

    // void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.tag.Equals("Wall"))
    //         Destroy(gameObject);
    //     if(other.gameObject.tag.Equals("Enemy")/* && Vector2.Distance(other.transform.position, transform.position) <= 1f */){
            // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // // Check enemy params
            // GameObject enemy = other.gameObject;
            // EnemyScript es = enemy.GetComponent<EnemyScript>();
            // // Debug.Log("BULLET IN ENEMY RANGE");
            // switch(bulletType){
            //     case "QuarantineBullet":
            //         // TODO: move enemy out of map w/ touches spawning slime
            //         if(!es.hasCovid) // Skip if enemy doesn't have covid
            //             break;
            //         break;
            //     case "VaxBullet":
            //         es.hasVax = true; // set to true regardless of previous state
            //         break;
            //     case "MaskBullet":
            //         es.hasMask = true;
            //         break;
            // }
    //     }
    // }

    // void OnTriggerStay2D(Collider2D other) {
    //     if(other.gameObject.tag.Equals("Enemy") && Vector2.Distance(other.transform.position, transform.position) <= 1f){
    //         gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //         // Check enemy params
    //         GameObject enemy = other.gameObject;
    //         EnemyScript es = enemy.GetComponent<EnemyScript>();
    //         // Debug.Log("BULLET IN ENEMY RANGE");
    //         switch(bulletType){
    //             case "QuarantineBullet":
    //                 // TODO: move enemy out of map w/ touches spawning slime
    //                 if(!es.hasCovid) // Skip if enemy doesn't have covid
    //                     break;
    //                 break;
    //             case "VaxBullet":
    //                 es.hasVax = true; // set to true regardless of previous state
    //                 break;
    //             case "MaskBullet":
    //                 es.hasMask = true;
    //                 break;
    //         }
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other) {
    //     if(other.gameObject.tag.Equals("Player") && Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) >= 3f) {
    //         gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //         StartCoroutine(WaitForDelete());
    //     }
    // }


    IEnumerator WaitForDelete(){
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = 0.5f;
        renderer.material.color = color;
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject);
    }
}
