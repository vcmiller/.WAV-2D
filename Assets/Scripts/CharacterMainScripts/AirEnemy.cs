using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : Enemy {
    public FlyingEnemyMotor motor { get; private set; }

    protected override void Awake() {
        base.Awake();

        motor = GetComponent<FlyingEnemyMotor>();
    }

    public override void Launch(Vector3 velocity) {
        motor.velocity = velocity;
    }
}
