using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHealth : MonoBehaviour {

    static  float maxHealth = 10f;
    private float currenthealth = maxHealth;
    public GameObject explosionPref;
    public GameController gameController;
    public int score=10;
    // public AudioSource explosionVoice;

    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
    }

    public void Demage(float value)
    {
        currenthealth -= value;
        if (currenthealth <= 0.0f)
        {
            Boom();
            gameController.AddScore(score);//被子弹击中才给分
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Boom();
            
        }
    }
    private void Boom()
    {
        GameObject ex =Instantiate<GameObject>(explosionPref, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(ex, 2.0f);
        Destroy(gameObject);
    }
}
