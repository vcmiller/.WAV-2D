using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class WavCharacterController : PlayerController {
    private WavCharacterChannels character;

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        character = channels as WavCharacterChannels;
    }

    public void Axis_Horizontal(float value) {
        character.movement += Vector3.right * value;
    }

    public void Axis_Vertical(float value) {
        character.movement += Vector3.up * value;
    }

    public void ButtonDown_Jump() {
        character.jump = true;
    }

    public void ButtonUp_Jump() {
        character.cancelJump = true;
    }

    public void ButtonDown_Fire1() {
        character.attack = 1;
    }

    public void ButtonDown_Fire3() {
        character.attack = 2;
    }

    public void ButtonDown_AltAttackChord()
    {
        character.altAttack = true;
    }

    public void ButtonUp_AltAttackChord()
    {
        character.altAttack = false;
    }
}
