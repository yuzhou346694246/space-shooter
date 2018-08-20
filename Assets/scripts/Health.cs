using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 10;//最大生命值
    public float currentHealth;
    public GameObject explosionPref;
    public bool isDead;

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public void Demage(float value){
        currentHealth -= value;
        if(currentHealth <= 0){
            isDead = true;
            Boom();
        }
    }

    private void Boom(){
        GameObject explosion = Instantiate(explosionPref, transform.position, transform.rotation);
        Destroy(explosion, 2.0f);
        Destroy(gameObject);
    }
}
