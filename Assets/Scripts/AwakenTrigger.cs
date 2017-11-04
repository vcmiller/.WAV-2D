using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class AwakenTrigger : MonoBehaviour {
    public Brain[] targets;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            foreach (var brain in targets) {
                brain.SendMessageToControllers("Awaken");
            }

            Destroy(gameObject);
        }
    }
}
