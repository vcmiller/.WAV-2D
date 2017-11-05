using UnityEngine;
using SBR;

public class WavCharacterChannels : CharacterChannels {
    public WavCharacterChannels() {
        RegisterInputChannel("attack", 0, true);
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

    public bool altAttack {
        get {
            return GetInput<bool>("altAttack");
        }

        set {
            SetInput("altAttack", value);
        }
    }

}
