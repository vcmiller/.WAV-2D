using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAbsoluteZ : MonoBehaviour {
    public float z;
	
	// Update is called once per frame
	void Update () {
        Vector3 v = transform.position;
        v.z = z;
        transform.position = v;
	}
}
