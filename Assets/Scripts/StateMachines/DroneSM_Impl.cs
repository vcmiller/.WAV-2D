using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSM_Impl : EnemySMBase<BasicControlProxy> {
    public Vector2 dir;

	public void State_Patrol() {
        control.movement = dir;

        sprite.flipX = dir.x < 0;
    }

    void HitWall(RaycastHit2D hit) {
        dir = Vector2.Reflect(dir, hit.normal);
    }
}
