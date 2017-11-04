using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class DishAnimMotor : BasicMotor<EnemyAttackChannels> {
    public Animator anim { get; private set; }

    protected override void Start() {
        base.Start();

        anim = GetComponent<Animator>();
    }

    public override void TakeInput() {
        if (channels.attackInProgress) {
            Play(State.Attack);
        } else if (channels.movement.x != 0) {
            Play(State.Move);
        } else {
            Play(State.Idle);
        }
    }

    public enum State {
        Move, Attack, Idle
    }

    private void Play(State state) {
        anim.Play(state.ToString());
    }
}
