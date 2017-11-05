using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Parallax : MonoBehaviour {

    [Range(0, 1)]
    public float parallaxFactor;
    
    private void OnRenderObject() {
        Vector3 v = Camera.current.transform.position * parallaxFactor;
        v.z = 0;
        transform.position = v;
    }
}
