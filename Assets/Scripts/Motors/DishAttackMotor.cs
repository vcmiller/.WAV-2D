using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAttackMotor : BasicMotor<CharacterAttackProxy> {
    public float attackTime = 1.5f;

    public float attackCooldown = 2;

    private ExpirationTimer attackTimer;
    private CooldownTimer attackCooldownTimer;

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


}
