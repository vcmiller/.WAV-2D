using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimMotor : BasicMotor<WavCharacterProxy> {
    public Animator anim { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    public CharacterMotor2D motor { get; private set; }
    public PlayerAttackMotor attackMotor { get; private set; }

    public override void TakeInput() {
        if (control.movement.x != 0 && !attackMotor.attacking) {
            sprite.flipX = control.movement.x < 0;
        }

        if (attackMotor.attacking) {
            if (attackMotor.attackDir == Vector2.up) {
                Play(State.SawUp);
            } else if (attackMotor.attackDir == Vector2.down) {
                Play(State.SawDown);
            } else {
                Play(State.SawSide);
            }
        } else if (!motor.grounded) {
            Play(State.Jump);
        } else if (control.movement.x == 0) {
            Play(State.Idle);
        } else {
            Play(State.Run);
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        motor = GetComponent<CharacterMotor2D>();
        
        attackMotor = GetComponent<PlayerAttackMotor>();
    }

    public enum State {
        Idle, Run, SawSide, SawUp, SawDown, Jump
    }

    private void Play(State state) {
        anim.Play(state.ToString());
    }
}
