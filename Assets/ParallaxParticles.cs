using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxParticles : MonoBehaviour {
    public Transform camera;
    public Vector3 offset;

    private ParticleSystem.ShapeModule shape;

	// Use this for initialization
	void Start () {
        shape = GetComponent<ParticleSystem>().shape;
	}
	
	// Update is called once per frame
	void Update () {
        shape.position = camera.position + offset - transform.position;
	}
}
