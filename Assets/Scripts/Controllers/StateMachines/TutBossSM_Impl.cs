using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossSM_Impl : GroundEnemySMBase<TutBossProxy> {
    public ExpirationTimer aggroExpiration { get; private set; }
    public Player player { get; private set; }

    public float aggroTime;
    public float chargeDist = 7;

    protected override void Awake() {
        base.Awake();

        aggroExpiration = new ExpirationTimer(aggroTime);
        player = FindObjectOfType<Player>();
    }

    void Awaken() {
        control.awoken = true;
    }

    public bool TransitionCond_Wait_Aggro() {
        return control.awoken;
    }

    public void TransitionNotify_Wait_Aggro() {
        aggroExpiration.Set();
    }

    public void StateEnter_Aggro() {
        control.attackDir = player.transform.position.x < transform.position.x ? -1 : 1;
    }

    public bool TransitionCond_Aggro_Combat() {
        return aggroExpiration.expired;
    }

    public void State_Combat() {

        control.attackDir = player.transform.position.x < transform.position.x ? -1 : 1;

        if (Vector3.Distance(transform.position, player.transform.position) < chargeDist) {
            control.attack = 2;
        } else {
            control.attack = 1;
        }

    }
}
