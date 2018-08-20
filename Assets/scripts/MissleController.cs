using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 导弹能自动寻找最近的目标进行打击
 * 
 * 
*/
public class MissleController : MonoBehaviour {

    // Use this for initialization
    public string tagName;
    public float speed = 10;//导弹运行速度
    public Rigidbody rb;
	void Start () {
        rb.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        AimWithTag();//寻找敌人
	}

    private void AimWithTag()
    {
        GameObject[] tObjects = GameObject.FindGameObjectsWithTag(tagName);
        if (tObjects == null)
        {
            Debug.Log("没有找到目标");
            return;
        }
        //tObjects.
        GameObject tObject = getNearestObject(tObjects);
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

    private GameObject getNearestObject(GameObject[] objs){
        float minDistance = Vector3.Distance(transform.position,objs[0].transform.position);
        int minIndex = 0;
        for (int i = 1; i < objs.Length;i++){
            float distance = Vector3.Distance(transform.position, objs[i].transform.position);
            if(distance < minDistance){
                minDistance = distance;
                minIndex = i;
            }
        }
        return objs[minIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        // 导弹碰到陨石和敌机都会爆炸
        // 这地方有个问题就是要区分哪种对象，才能进行伤害处理
        // 如果让所有对象都有个Health模块，那么就能处理了
        // 当Heath模块的值小于等于0时，应该启动爆炸
        if(other.gameObject.tag.StartsWith("Enemy",System.StringComparison.CurrentCulture)){
            Boom();
        }
    }

    private void Boom(){
        Destroy(gameObject);
    }
}
