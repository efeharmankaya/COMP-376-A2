using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Target Indicator https://www.youtube.com/watch?v=U1SdjGUFSAI
public class TargetIndicator : MonoBehaviour
{

    public Transform Target;
    // Update is called once per frame
    void Update()
    {
        if(Target == null){
            Destroy(this.gameObject);
        }else{
            var direction = Target.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
            
        
    }

    public void setTarget(GameObject x){
        Target = x.transform;
    }
}
