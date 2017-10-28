using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAnimMotor : BasicMotor<CharacterAttackProxy> {
    public Animator anim { get; private set; }

    protected override void Awake() {
        base.Awake();

        anim = GetComponent<Animator>();
    }

    public override void TakeInput() {
        if (control.attackInProgress) {
            Play(State.Attack);
        } else if (control.movement.x != 0) {
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
