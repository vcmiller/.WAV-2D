using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamera : MonoBehaviour {
    public Camera otherCamera;

    private Camera myCamera;

    private void Awake() {
        myCamera = GetComponent<Camera>();
    }

    private void LateUpdate() {
        myCamera.transform.position = otherCamera.transform.position;
        myCamera.orthographicSize = otherCamera.orthographicSize;
    }
}
