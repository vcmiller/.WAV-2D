using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenTrigger : MonoBehaviour {
    public GameObject[] targets;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            foreach (var obj in targets) {
                obj.SendMessage("Awaken", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
