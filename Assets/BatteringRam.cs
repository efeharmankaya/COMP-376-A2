using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteringRam : MonoBehaviour
{

    public float duration = 8f;
    public int wallLayer, batteringRamLayer;

    public int enemyCollisionCount = 0;
    public List<GameObject> collided;
    // Start is called before the first frame update
    void Start()
    {
        batteringRamLayer = this.gameObject.layer;
        wallLayer = LayerMask.NameToLayer("Map");
        Physics2D.IgnoreLayerCollision(batteringRamLayer, wallLayer, true);
        StartCoroutine(Decay());
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(!collided.Contains(other.gameObject) 
            && (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Infected"))
        ){
            enemyCollisionCount++;
            collided.Add(other.gameObject);
        }
    }

    IEnumerator Decay(){
        float step = duration / 3;
        yield return new WaitForSeconds(step); // if duration = 3; step = 1
        DecayAllChildren(0.75f);
        yield return new WaitForSeconds(step/2); // 0.5
        DecayAllChildren(0.55f);
        yield return new WaitForSeconds(step/2); // 0.5
        DecayAllChildren(0.35f);
        yield return new WaitForSeconds(step); // 1
        MainScoreScript.score += (enemyCollisionCount / 2) * 5; // 5 additional points for every 2 distinct enemies hit w/ battering ram
        Destroy(gameObject);
    }

    void DecayAllChildren(float a){
        foreach(Transform child in this.transform){
            Renderer r = child.gameObject.GetComponent<Renderer>();
            Color color = r.material.color;
            color.a = a;
            r.material.color = color;
        }
    }
}
