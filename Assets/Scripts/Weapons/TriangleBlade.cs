using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class TriangleBlade : MonoBehaviour {

    bool begun = false;
    SpriteRenderer sr;
    Rigidbody2D rb;

    public Trail trail;

	// Use this for initialization
	void Start () {
        if (begun) return;
        begun = true;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
	}



    public void Embed(Vector2 position, bool flipX)
    {
        Start();
        transform.position = position;
        sr.flipX = flipX;
    }

    public void CreateTrail(Vector2 from, Vector2 to)
    {
        Trail t = Instantiate(trail, to, Quaternion.identity);
        t.SetTrail(from, 0.05f);
        t.lr.startColor = Color.red;
        t.lr.endColor = Color.clear;
    }
}
