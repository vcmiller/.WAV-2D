using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public abstract class Enemy : MonoBehaviour {
    public float launchSpeed;
    
    public Brain brain { get; private set; }

    protected virtual void Awake() {
        brain = GetComponent<Brain>();
    }

    protected virtual void DamageNotify(Damage dmg) {
        Launch(dmg.dir * launchSpeed);
    }

    protected virtual void ZeroHealth() {
        Destroy(gameObject);
    }

    public abstract void Launch(Vector3 velocity);
}
