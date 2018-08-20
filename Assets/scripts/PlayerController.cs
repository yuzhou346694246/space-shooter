using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerController : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public Boundary boundary;
    public float rotation;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public GameObject explosion;
    public AudioSource fireAudio;
    public GameController gameController;
    public float fireDelay = 0.2f;//开火间隔

    private bool isFiring;
    private void Start()
    {
        
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        isFiring = false;
        
        if(gameController == null){
            Debug.Log("GameController is empty");
        }
        StartCoroutine(Fire());
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0.0f, y);
        rb.velocity = movement * speed;

        //控制移动范围
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.yMin, boundary.yMax)
            );
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x*rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFiring = true;

        }
        if(Input.GetKeyUp(KeyCode.Space)){
            isFiring = false;
        }
    }

    IEnumerator Fire()
    {
        while(true){
            while (isFiring)
            {//只有在允许开火的时候才能开火
             //产生新的子弹
                GameObject bullet = (GameObject)Instantiate<GameObject>(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bullet.transform.forward * bulletSpeed;
                fireAudio.Play();
                // Destroy(bullet, 2.0f);
                yield return new WaitForSeconds(fireDelay);
            }
            yield return new WaitWhile(() => isFiring);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.StartsWith("Enemy"))
        {
            Boom();
            gameController.GameOver();
        }
    }

    private void Boom()
    {
        GameObject ex =Instantiate(explosion, transform.position, transform.rotation);
        Destroy(ex, 2.0f);
        Destroy(gameObject);
        
    }
}
