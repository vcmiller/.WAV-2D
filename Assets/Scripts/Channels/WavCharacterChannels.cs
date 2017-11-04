using UnityEngine;
using SBR;

public class WavCharacterChannels : CharacterChannels {
    public WavCharacterChannels() {
        RegisterInputChannel("attack", 0, true);
        RegisterInputChannel("cancelJump", false, true);
        RegisterInputChannel("altAttack", false, false);

    }
    

    public int attack {
        get {
            return GetInput<int>("attack");
        }

        set {
            SetInt("attack", value);
        }
    }

    public bool cancelJump {
        get {
            return GetInput<bool>("cancelJump");
        }

        set {
            SetInput("cancelJump", value);
        }
    }

    public bool altAttack {
        get {
            return GetInput<bool>("altAttack");
        }

        set {
            SetInput("altAttack", value);
        }
    }

}
