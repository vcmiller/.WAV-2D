using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class DroneSMImpl : ScoutSM {
    public Vector2 dir;

    private CharacterChannels control;

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        control = channels as CharacterChannels;
    }

    protected override void State_Patrol() {
        control.movement = dir;

        sprite.flipX = dir.x < 0;
    }

    void HitWall(RaycastHit2D hit) {
        dir = Vector2.Reflect(dir, hit.normal);
    }
}
