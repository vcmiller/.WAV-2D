using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Motor : MonoBehaviour {
    public bool enableInput { get; set; }

    protected virtual void Awake() {
        enableInput = true;
    }

    public abstract void TakeInput();

    public virtual void UpdateAfterInput() { }
}
