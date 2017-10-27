using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimMotor : BasicMotor<CharacterProxy> {
    public Animator anim { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    public CharacterMotor2D motor { get; private set; }

    private State attackState;
    private ExpirationTimer attack;

    public override void TakeInput() {
        if (control.movement.x != 0 && attack.expired) {
            sprite.flipX = control.movement.x < 0;
        }

        if (!attack.expired) {
            Play(attackState);
        } else if (!motor.grounded) {
            Play(State.Jump);
        } else if (control.movement.x == 0) {
            Play(State.Idle);
        } else {
            Play(State.Run);
        }

        if (Input.GetMouseButtonDown(0)) {
            attack.Set();

            if (control.movement.y > 0) {
                attackState = State.SawUp;
            } else if (control.movement.y < 0 && !motor.grounded) {
                attackState = State.SawDown;
            } else {
                attackState = State.SawSide;
            }
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        motor = GetComponent<CharacterMotor2D>();

        attack = new ExpirationTimer(0.3f);
    }

    public enum State {
        Idle, Run, SawSide, SawUp, SawDown, Jump
    }

    private void Play(State state) {
        anim.Play(state.ToString());
    }
}
