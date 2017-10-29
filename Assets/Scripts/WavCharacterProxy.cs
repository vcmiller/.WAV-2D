using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavCharacterProxy : CharacterProxy {
    protected override void Awake() {
        base.Awake();

        RegisterInputChannel("Attack", 0, true);
        RegisterInputChannel("CancelJump", false, true);
    }

    public int attack {
        get {
            return GetInt("Attack");
        }

        set {
            SetInt("Attack", value, 0, 3);
        }
    }

    public bool cancelJump {
        get {
            return GetBool("CancelJump");
        }

        set {
            SetBool("CancelJump", value);
        }
    }
}
