using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float launchSpeed;
    
    public Controller ctrl { get; private set; }

    protected virtual void Awake() {
        ctrl = GetComponent<Controller>();
    }

    protected virtual void DamageNotify(Damage dmg) {
        Launch(dmg.dir * launchSpeed);
    }

    protected virtual void ZeroHealth() {
        Destroy(gameObject);
    }

    public abstract void Launch(Vector3 velocity);
}
