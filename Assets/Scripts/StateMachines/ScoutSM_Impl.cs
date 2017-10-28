using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutSM_Impl : GroundEnemySMBase<CharacterProxy> {

	public void State_Patrol() {

        control.movement = forward;

        if (pitCheck || wallCheck) {
            Flip();
        }
    }
}
