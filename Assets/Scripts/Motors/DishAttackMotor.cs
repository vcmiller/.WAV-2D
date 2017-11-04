using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class DishAttackMotor : BasicMotor<EnemyAttackChannels> {
    public float attackTime = 1.5f;

    public float attackCooldown = 2;

    private ExpirationTimer attackTimer;
    private CooldownTimer attackCooldownTimer;

    public GameObject projPrefab;
    public Transform target;

    protected override void Start() {
        base.Start();

        attackTimer = new ExpirationTimer(attackTime);
        attackCooldownTimer = new CooldownTimer(attackCooldown);
    }

    public override void TakeInput() {
        if (channels.attack && attackCooldownTimer.Use()) {
            attackTimer.Set();
        }

        channels.attackInProgress = !attackTimer.expired;
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
