using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossAnimMotor : BasicMotor<TutBossProxy> {
    public GameObject aggroGlow;

    public Animator anim { get; private set; }
    public TutBossAttackMotor attack { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    private bool spawnedAnim = false;

    private State lastPlayed { get; set; }

    protected override void Awake() {
        base.Awake();

        anim = GetComponent<Animator>();
        attack = GetComponent<TutBossAttackMotor>();
        sprite = GetComponent<SpriteRenderer>();
        lastPlayed = State.Aggro;
    }

    public override void TakeInput() {

        if (!control.awoken) {
            sprite.flipX = control.attackDir < 0;
            Play(State.Wait);
        } else if (attack.charging) {
            sprite.flipX = attack.facing < 0;
            Play(State.Charge);
        } else if (attack.meleeing) {
            sprite.flipX = attack.facing < 0;
            Play(State.Melee);
        } else {
            sprite.flipX = control.attackDir < 0;
            Play(State.Aggro);

            if (!spawnedAnim) {
                spawnedAnim = true;
                Invoke("AggroAnim", 0.75f);
            }
        }
    }

    private void AggroAnim() {
        Instantiate(aggroGlow, transform.position, Quaternion.identity);
    }

    public enum State {
        Wait, Aggro, Charge, Melee
    }

    private void Play(State state) {
        if (state != lastPlayed) {
            anim.Play(state.ToString());
            lastPlayed = state;
        }
    }
}
