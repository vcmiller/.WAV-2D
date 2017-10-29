using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy {
    public CharacterMotor2D motor { get; private set; }
    public ExpirationTimer stunTimer { get; private set; }

    public float stunTime = 0.5f;

    protected override void Awake() {
        base.Awake();

        motor = GetComponent<CharacterMotor2D>();
        stunTimer = new ExpirationTimer(stunTime);
    }

    public override void Launch(Vector3 velocity) {
        motor.velocity = velocity;
    }

    private void Update() {
        if (stunTimer.expired) {
            motor.enabledAirControl = true;
            ctrl.enabled = true;
        }
    }

    protected override void DamageNotify(Damage dmg) {
        float f = dmg.dir.magnitude;

        Launch((dmg.dir.normalized + Vector3.up) * launchSpeed * f);
        motor.enabledAirControl = false;
        ctrl.enabled = false;
        stunTimer.Set();
    }
}
