using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAttackMotor : BasicMotor<CharacterAttackProxy> {
    public float attackTime = 1.5f;

    public float attackCooldown = 2;

    private ExpirationTimer attackTimer;
    private CooldownTimer attackCooldownTimer;

    public GameObject projPrefab;
    public Transform target;

    protected override void Awake() {
        base.Awake();

        attackTimer = new ExpirationTimer(attackTime);
        attackCooldownTimer = new CooldownTimer(attackCooldown);
    }

    public override void TakeInput() {
        if (control.attack && attackCooldownTimer.Use()) {
            attackTimer.Set();
        }

        control.attackInProgress = !attackTimer.expired;
    }

    public void Fire() {
        Vector2 fwd = Vector2.right;
        Vector3 launchPoint = transform.GetChild(0).localPosition;

        if (GetComponent<SpriteRenderer>().flipX) {
            fwd = Vector2.left;
            launchPoint.x *= -1;
        }

        var proj = Instantiate(projPrefab).GetComponent<WavProjectile>();
        Vector3 v = transform.position + launchPoint;
        proj.transform.position = v;
        proj.FireAt(fwd, target);
    }
}
