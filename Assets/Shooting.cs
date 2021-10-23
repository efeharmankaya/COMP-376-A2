using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject baseBullet;
    public GameObject baseBatteringRam;

    public Sprite vaxSprite, quarantineSprite;
    public float force = 20f;
    public bool coroutineAllowed = true;
    public float shootSpeedCooldown = 0.5f;
    public float batteringRamCooldown = 20f;

    public bool batteringRamAllowed = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && batteringRamAllowed)
                CreateBatteringRam();

        if(coroutineAllowed){
            if(Input.GetButtonDown("Fire1"))
                Shoot("QuarantineBullet");
            else if(Input.GetKeyDown(KeyCode.E))
                Shoot("VaxBullet");  
            else if(Input.GetKeyDown(KeyCode.Q))
                Shoot("MaskBullet");
            else
                return;

            StartCoroutine(ShootCooldown());
            
        }
    }

    void Shoot(string bulletType){
        GameObject bullet = Instantiate(baseBullet, firePoint.position, firePoint.rotation);
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        switch(bulletType){
            case "QuarantineBullet":
                spriteRenderer.sprite = quarantineSprite;
                break;
            case "VaxBullet":
                spriteRenderer.sprite = vaxSprite;
                break;
            case "MaskBullet":
                // Already set to mask continue
                break;
        }
        // BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        // bulletScript.bulletType = "Mask";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        // bullet.transform.parent = firePoint.transform;
        bullet.transform.tag = bulletType;
    }

    void CreateBatteringRam(){
        GameObject batteringRam = Instantiate(baseBatteringRam, firePoint);
        batteringRam.transform.parent = firePoint.transform;
        StartCoroutine(BatteringRamCooldown());
    }

    IEnumerator ShootCooldown()
    {
        coroutineAllowed = false; // mutex lock
        yield return new WaitForSeconds(shootSpeedCooldown);
        coroutineAllowed = true; // mutex unlock
    }

    IEnumerator BatteringRamCooldown(){
        batteringRamAllowed = false;
        yield return new WaitForSeconds(batteringRamCooldown);
        batteringRamAllowed = true;
    }
}
