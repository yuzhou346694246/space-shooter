using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        GameObject target = other.gameObject;
        if (target.tag.Equals("Env"))
        {
            return;
        }
        if (target.tag.Equals("Player"))
        {
            //StoneHealth health = target.GetComponent<StoneHealth>();
            //health.Demage(10.0f);
            Destroy(gameObject);
        }

        


    }
}
