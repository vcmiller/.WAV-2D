using UnityEngine;
using SBR;

public class TutBossChannels : CharacterChannels {
    public TutBossChannels() {
        RegisterInputChannel("awoken", false, false);
        RegisterInputChannel("attack", 0, true);
        RegisterInputChannel("attackDir", 1, false);

    }
    

    public bool awoken {
        get {
            return GetInput<bool>("awoken");
        }

        set {
            SetInput("awoken", value);
        }
    }

    public int attack {
        get {
            return GetInput<int>("attack");
        }

        set {
            SetInt("attack", value);
        }
    }

    public int attackDir {
        get {
            return GetInput<int>("attackDir");
        }

        set {
            SetInt("attackDir", value);
        }
    }

}
