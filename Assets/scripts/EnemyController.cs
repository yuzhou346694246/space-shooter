using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour {
    public Rigidbody rb;
    public GameObject bulletPref;//子弹
    public GameObject explosionPref;//爆炸
    public Transform firePoint;//子弹产生位置
    public AudioSource fireAudio;//开火声音
    public int fireNum = 5;//每次停下来开火次数
    public float fireDuration =0.5f;//每次发射子弹间隔
    public float maxHealth = 10;
    public float delayFire = 1.0f;//开火到停下来时间 
    public float speed = 5;//飞船下降速度
    public float bulletSpeed = 10;
    public float currentHealth;
    

    private GameController gameController;
    private bool dead;//这个有必要么？
    private void Start()
    {
        currentHealth = maxHealth;
        dead = false;
        
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        
        AimWithTag("Player");
        rb.velocity = speed * transform.forward;
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update () {
        // AimWithTag("Test");
        //AimWithTag("Player");//不断的进行瞄准
	}

    private void AimWithTag(string tag)
    {
        GameObject tObject = GameObject.FindWithTag(tag);
        if(tObject == null)
        {
            Debug.Log("没有找到目标");
            return;
        }
        Transform target = tObject.transform;
        Vector3 targetDir = target.position - transform.position;

        float step = 5;// Time.deltaTime;
        //Debug.Log(tObject);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        // Debug.DrawRay(transform.position, newDir, Color.red,1.0f,false);
        //Gizmos.DrawRay(transform.position, newDir);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
        //Debug.Log(transform.rotation);
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(delayFire);//进来先延迟一下
        while (true)
        {
            AimWithTag("Player");//瞄准目标开几枪
            for (int i = 0; i < fireNum; i++)
            {
                GameObject bullet =Instantiate(bulletPref, firePoint.transform.position, transform.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bullet.transform.forward * bulletSpeed;
                yield return new WaitForSeconds(fireDuration);
            }
            rb.velocity = speed * transform.forward;
            yield return new WaitForSeconds(delayFire);//延迟开火时间
            rb.velocity = 0 * transform.forward;//停下来准备开火
            if (dead)
            {
                break;
            }
        }
    }
   
    public void Demage(float value)
    {
        currentHealth -= value;
        if(currentHealth <= 0.0f)
        {
            Boom();
            gameController.AddScore(20);//被摧毁加二十分
        }
    }

    //爆炸
    private void Boom()
    {
        GameObject explosion = Instantiate(explosionPref, transform.position, transform.rotation);
        dead = true;
        Destroy(explosion, 2.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))//撞到了Player
        {
            Boom();
        }
    }
}
