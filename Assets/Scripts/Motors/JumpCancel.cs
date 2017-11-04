using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class JumpCancel : BasicMotor<WavCharacterChannels> {
    public CharacterMotor2D mainMotor { get; private set; }

    public bool jumping { get; private set; }

    protected override void Start() {
        base.Start();

        mainMotor = GetComponent<CharacterMotor2D>();
    }

    public override void TakeInput() {
        if (mainMotor.grounded || mainMotor.velocity.y <= 0) {
            jumping = false;
        }

        if (mainMotor.jumped) {
            jumping = true;
        }

        if (channels.cancelJump && jumping) {
            mainMotor.velocity.y = 0;
        }
    }
}
