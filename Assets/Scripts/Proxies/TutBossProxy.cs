using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossProxy : CharacterProxy {
    protected override void Awake() {
        base.Awake();

        RegisterInputChannel("Awoken", false, false);
        RegisterInputChannel("Attack", 0, true);
        RegisterInputChannel("AttackDir", 1, false);
    }

    public bool awoken {
        get {
            return GetBool("Awoken");
        }
        
        set {
            SetBool("Awoken", value);
        }
    }

    public int attack {
        get {
            return GetInt("Attack");
        }

        set {
            SetInt("Attack", value);
        }
    }

    public int attackDir {
        get {
            return GetInt("AttackDir");
        }

        set {
            SetInt("AttackDir", value);
        }
    }
}
