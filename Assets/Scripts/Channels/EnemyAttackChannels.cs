using UnityEngine;
using SBR;

public class EnemyAttackChannels : CharacterChannels {
    public EnemyAttackChannels() {
        RegisterInputChannel("attack", false, false);
        RegisterInputChannel("attackInProgress", false, false);

    }
    

    public bool attack {
        get {
            return GetInput<bool>("attack");
        }

        set {
            SetInput("attack", value);
        }
    }

    public bool attackInProgress {
        get {
            return GetInput<bool>("attackInProgress");
        }

        set {
            SetInput("attackInProgress", value);
        }
    }

}
