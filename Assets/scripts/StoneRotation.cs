using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotation : MonoBehaviour {

    // Use this for initialization
    public Rigidbody rb;
    public float speed=5;
	void Start () {
        rb.angularVelocity = Random.insideUnitSphere*5;
        rb.velocity = new Vector3(0, 0, -speed);
    }
	
	
}
