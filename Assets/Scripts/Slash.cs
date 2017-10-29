using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {
    public float lifetime = 1;

    public SpriteRenderer sprite { get; private set; }

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var c = sprite.color;
        c.a -= Time.deltaTime / lifetime;
        sprite.color = c;

        if (c.a <= 0) {
            Destroy(gameObject);
        }
	}
}
