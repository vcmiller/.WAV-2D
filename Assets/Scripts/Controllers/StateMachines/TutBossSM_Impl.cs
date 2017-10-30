using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossSM_Impl : GroundEnemySMBase<TutBossProxy> {
    public ExpirationTimer aggroExpiration { get; private set; }

    public float aggroTime;

    protected override void Awake() {
        base.Awake();

        aggroExpiration = new ExpirationTimer(aggroTime);
    }

    void Awaken() {
        control.awoken = true;
    }

    public bool TransitionCond_Wait_Aggro() {
        return control.awoken;
    }

    public void StateEnter_Aggro() {
        aggroExpiration.Set();
    }

    public bool TransitionCond_Aggro_Combat() {
        return aggroExpiration.expired;
    }

    public void StateEnter_Combat() {
        control.attack = 1;
    }
}
