using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackProxy : CharacterProxy {
    protected override void Awake() {
        base.Awake();

        RegisterInputChannel("Attack", false, false);
        RegisterInputChannel("Attacking", false, false);
    }

    public bool attack {
        get {
            return GetBool("Attack");
        }

        set {
            SetBool("Attack", value);
        }
    }

    public bool attackInProgress {
        get {
            return GetBool("Attacking");
        }

        set {
            SetBool("Attacking", value);
        }
    }
}
