using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackPoint : MonoBehaviour {
    public SpriteRenderer sprite { get; private set; }

    public float dist = 1.5f;
    public float speed = 5;

	// Use this for initialization
	void Start () {
        sprite = GetComponentInParent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 t;
		if (sprite.flipX) {
            t = new Vector3(-dist, 0, 0);
        } else {
            t = new Vector3(dist, 0, 0);
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, t, Time.deltaTime * speed);
	}
}
