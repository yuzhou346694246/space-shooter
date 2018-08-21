using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5;
    public GameObject explosionPref;

    public Health health;
    public int value = 10;

    private GameController gameController;

    void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        rb.angularVelocity = Random.insideUnitSphere * 5;
        rb.velocity = new Vector3(0, 0, -speed);

        StartCoroutine(Death());//等待死亡
    }


    IEnumerator Death()
    {
        yield return new WaitWhile(() => !health.isDead);
        gameController.AddScore(value);
        Boom();
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
        GameObject ex = Instantiate<GameObject>(explosionPref, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(ex, 2.0f);
        Destroy(gameObject);
    }
}
