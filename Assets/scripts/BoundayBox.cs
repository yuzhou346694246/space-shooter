using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundayBox : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        // Debug.Log(other.gameObject.name);
    }
}
