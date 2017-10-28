using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSM_Impl : GroundEnemySMBase<CharacterAttackProxy> {
    public Transform player;
    public float desiredDist;
    public float range;
    public float outRange;

    public void StateEnter_Attack() {
        sprite.flipX = player.position.x < transform.position.x;
    }

	public void State_Attack() {
        control.attack = distFromRange < outRange;
    }

    public void StateExit_Attack() {
        control.attack = false;
    }

    public void State_Move() {
        if (transform.position.x < player.position.x) {
            control.movement = new Vector2(Mathf.Sign(player.position.x - desiredDist - transform.position.x), 0);
        } else {
            control.movement = new Vector2(Mathf.Sign(player.position.x + desiredDist - transform.position.x), 0);
        }

        sprite.flipX = control.movement.x < 0;
    }

    public bool TransitionCond_Attack_Move() {
        return (distFromRange > outRange || sprite.flipX != player.position.x < transform.position.x) && control.attackInProgress == false; ;
    }

    public bool TransitionCond_Move_Attack() {
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
