using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy {
    public CharacterMotor2D motor { get; private set; }

    protected override void Awake() {
        base.Awake();

        motor = GetComponent<CharacterMotor2D>();
    }

    public override void Launch(Vector3 velocity) {
        motor.velocity = velocity;
    }
}
