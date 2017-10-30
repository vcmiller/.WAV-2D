using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossAnimMotor : BasicMotor<TutBossProxy> {
    public GameObject aggroGlow;

    public Animator anim { get; private set; }
    public TutBossAttackMotor attack { get; private set; }

    private bool spawnedAnim = false;

    private State lastPlayed { get; set; }

    protected override void Awake() {
        base.Awake();

        anim = GetComponent<Animator>();
        attack = GetComponent<TutBossAttackMotor>();
        lastPlayed = State.Aggro;
    }

    public override void TakeInput() {
        if (!control.awoken) {
            Play(State.Wait);
        } else if (attack.charging) {
            Play(State.Charge);
        } else {
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
        Wait, Aggro, Charge
    }

    private void Play(State state) {
        if (state != lastPlayed) {
            anim.Play(state.ToString());
            lastPlayed = state;
        }
    }
}
