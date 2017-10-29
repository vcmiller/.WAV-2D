using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavProjectile : Projectile {
    public float homingAngleSpeed;
    public bool homing;
    public Transform homingTarget;

	public void FireAt(Vector3 dir, Transform target) {
        Fire(dir);
        this.homingTarget = target;
        this.homing = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (homing) {
            velocity = Vector3.RotateTowards(velocity, (homingTarget.position - transform.position).normalized, homingAngleSpeed * Time.deltaTime, 0);
        }

        transform.Translate(velocity * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        OnHitObject(collision, transform.position, velocity);
    }
}
