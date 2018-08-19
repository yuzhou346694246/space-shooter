using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        
        GameObject target = other.gameObject;
        if (target.tag.Equals("Env"))
        {
            return;
        }
        if (target.tag.Equals("Enemy"))
        {
            StoneHealth health = target.GetComponent<StoneHealth>();
            health.Demage(10.0f);
        }
        if (target.tag.Equals("EnemyShip"))
        {
            EnemyController controller = target.GetComponent<EnemyController>();
            Debug.Log("EnemyShip");
            controller.Demage(10.0f);
        }
        
        Destroy(gameObject);
        
       
    }
}
