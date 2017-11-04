using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class PlayerAnimMotor : BasicMotor<WavCharacterChannels> {
    public Animator anim { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    public CharacterMotor2D motor { get; private set; }
    public PlayerAttackMotor attackMotor { get; private set; }

    public override void TakeInput() {
        if (channels.movement.x != 0 && !attackMotor.attacking) {
            sprite.flipX = channels.movement.x < 0;
        }

        if (attackMotor.attacking) {
            if (attackMotor.attackDir == Vector2.up) {
                PlayAttack(State.Up);
            } else if (attackMotor.attackDir == Vector2.down) {
                PlayAttack(State.Down);
            } else {
                PlayAttack(State.Side);
            }
        } else if (!motor.grounded) {
            Play(State.Jump);
        } else if (channels.movement.x == 0) {
            Play(State.Idle);
        } else {
            Play(State.Run);
        }
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        motor = GetComponent<CharacterMotor2D>();
        
        attackMotor = GetComponent<PlayerAttackMotor>();
    }

    public enum State {
        Idle, Run, Jump, Side, Up, Down
    }

    private void Play(State state) {
        anim.Play(state.ToString());
    }

    private void PlayAttack(State state) {
        anim.Play(attackMotor.curWeapon.name + state.ToString());
    }
}
