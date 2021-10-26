using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // public string bulletType;
    
    public int playerLayer, bulletLayer, batteringRamLayer;
    void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
        batteringRamLayer = LayerMask.NameToLayer("BatteringRam");
        bulletLayer = this.gameObject.layer;
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, true);
        Physics2D.IgnoreLayerCollision(batteringRamLayer, bulletLayer, true);
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
                es.headingIsolation = true;
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
        if(delete){
            Destroy(gameObject);
            MainScoreScript.score++; // increase score on sucessful bullet shot 
        }
        else{
            StartCoroutine(WaitForDelete()); // start wait for deletion on ground if bullet not used
        }
    }

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
