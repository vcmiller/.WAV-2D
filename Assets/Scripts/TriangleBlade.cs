using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class TriangleBlade : MonoBehaviour {

    SpriteRenderer sr;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
	}


    public void Embed(Vector2 position, bool flipX)
    {
        transform.position = position;
        sr.flipX = flipX;
    }

}
