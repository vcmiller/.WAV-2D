using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoutSMImpl : ScoutSM {
    private CharacterChannels control;

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        control = channels as CharacterChannels;
    }

    protected override void State_Patrol() {

        control.movement = forward;

        if (pitCheck || wallCheck) {
            Flip();
        }
    }
}
