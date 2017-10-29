using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavCharacterProxy : CharacterProxy {
    protected override void Awake() {
        base.Awake();

        RegisterInputChannel("Attack", false, true);
    }

    public bool attack {
        get {
            return GetBool("Attack");
        }

        set {
            SetBool("Attack", value);
        }
    }
}
