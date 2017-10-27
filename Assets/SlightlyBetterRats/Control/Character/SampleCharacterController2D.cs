using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacterController2D : PlayerController<CharacterProxy> {
	
	public void Axis_Horizontal(float value) {

        controlled.movement += Vector3.right * value;
    }

    public void Axis_Vertical(float value) {
        controlled.movement += Vector3.up * value;
    }

    public void ButtonDown_Jump() {
        controlled.jump = true;
    }
}
