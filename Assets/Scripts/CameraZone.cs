using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour {
    public GameObject camera;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && camera) {
            camera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && camera) {
            camera.SetActive(false);
        }
    }
}
