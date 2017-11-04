using UnityEngine;
using SBR;

public class TutBossSMImpl : TutBossSM {
    public ExpirationTimer aggroExpiration { get; private set; }
    public Player player { get; private set; }
    private TutBossChannels control;

    public float aggroTime;
    public float chargeDist = 7;

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        aggroExpiration = new ExpirationTimer(aggroTime);
        player = FindObjectOfType<Player>();
        control = channels as TutBossChannels;
    }

    public void Awaken() {
        control.awoken = true;
    }

    protected override void State_Wait() {
        control.attackDir = player.transform.position.x < transform.position.x ? -1 : 1;
    }

    protected override bool TransitionCond_Wait_Aggro() {
        return control.awoken;
    }

    protected override void TransitionNotify_Wait_Aggro() {
        aggroExpiration.Set();
    }

    protected override bool TransitionCond_Aggro_Combat() {
        return aggroExpiration.expired;
    }

    protected override void State_Combat() {

        control.attackDir = player.transform.position.x < transform.position.x ? -1 : 1;

        if (Vector3.Distance(transform.position, player.transform.position) < chargeDist) {
            control.attack = Random.Range(0, 3) == 0 ? 1 : 2;
        } else {
            control.attack = 1;
        }

    }

}
