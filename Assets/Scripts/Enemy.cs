using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float launchSpeed;

    protected virtual void Awake() {

    }

    protected virtual void DamageNotify(Damage dmg) {

    }

    protected virtual void ZeroHealth() {
        Destroy(gameObject);
    }

    public abstract void Launch(Vector3 velocity);
}
