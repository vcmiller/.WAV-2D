using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCancel : BasicMotor<WavCharacterProxy> {
    public CharacterMotor2D mainMotor { get; private set; }

    public bool jumping { get; private set; }

    protected override void Awake() {
        base.Awake();

        mainMotor = GetComponent<CharacterMotor2D>();
    }

    public override void TakeInput() {
        if (mainMotor.grounded || mainMotor.velocity.y <= 0) {
            jumping = false;
        }

        if (control.jump && mainMotor.grounded) {
            jumping = true;
        }

        if (control.cancelJump) {
            mainMotor.velocity.y = 0;
        }
    }
}
