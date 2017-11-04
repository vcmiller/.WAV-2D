using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class DishSMImpl : DishSM {
    public Transform player;
    public float desiredDist;
    public float range;
    public float outRange;

    private EnemyAttackChannels control;

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        control = channels as EnemyAttackChannels;
    }

    public void StateEnter_Attack() {
        sprite.flipX = player.position.x < transform.position.x;
    }

	protected override void State_Attack() {
        control.attack = distFromRange < outRange;
    }

    protected override void StateExit_Attack() {
        control.attack = false;
    }

    protected override void State_Move() {
        if (transform.position.x < player.position.x) {
            control.movement = new Vector2(Mathf.Sign(player.position.x - desiredDist - transform.position.x), 0);
        } else {
            control.movement = new Vector2(Mathf.Sign(player.position.x + desiredDist - transform.position.x), 0);
        }

        sprite.flipX = control.movement.x < 0;
    }

    protected override bool TransitionCond_Attack_Move() {
        return (distFromRange > outRange || sprite.flipX != player.position.x < transform.position.x) && control.attackInProgress == false; ;
    }

    protected override bool TransitionCond_Move_Attack() {
        return distFromRange < range;
    }

    private float distFromRange {
        get {
            return Mathf.Abs(xDist - desiredDist);
        }
    }

    private float xDist {
        get {
            return Mathf.Abs(player.position.x - transform.position.x);
        }
    }
}
