using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {


    public string tagName;

    // Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        // AimWithTag(tagName);
        AimWithTag();
    }

    private void AimWithTag(string tag)
    {
        GameObject tObject = GameObject.FindWithTag(tag);
        if (tObject == null)
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

    private void AimWithTag()
    {
        GameObject[] tObjects = GameObject.FindGameObjectsWithTag(tagName);
        if (tObjects.Length == 0)
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

    private GameObject getNearestObject(GameObject[] objs)
    {
        float minDistance = Vector3.Distance(transform.position, objs[0].transform.position);
        int minIndex = 0;
        for (int i = 1; i < objs.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, objs[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        return objs[minIndex];
    }
}
