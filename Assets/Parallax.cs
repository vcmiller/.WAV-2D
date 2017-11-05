using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public Camera camera;

    [Range(0, 1)]
    public float parallaxFactor;

    private void LateUpdate() {
        transform.position = camera.transform.position * parallaxFactor;
    }
}
