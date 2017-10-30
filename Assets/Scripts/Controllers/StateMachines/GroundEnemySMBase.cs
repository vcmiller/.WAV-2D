using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemySMBase<T> : EnemySMBase<T> where T : ControlProxy {
    public float wallDist = 1;
    public float floorDist = 1.1f;

    public LayerMask groundLayers;

    protected virtual bool wallCheck {
        get {
            return Physics2D.Raycast(transform.position, forward, wallDist, groundLayers);
        }
    }

    protected virtual bool grounded {
        get {
            return Physics2D.Raycast(transform.position, Vector2.down, floorDist, groundLayers);
        }
    }

    protected virtual bool pitCheck {
        get {
            return !Physics2D.Raycast((Vector2)transform.position + forward * wallDist, Vector2.down, floorDist, groundLayers);
        }
    }
}
