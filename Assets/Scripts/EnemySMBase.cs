using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySMBase<T> : StateMachineImpl<T> where T : ControlProxy {

    private SpriteRenderer _sprite;

    protected SpriteRenderer sprite {
        get {
            if (!_sprite) {
                _sprite = GetComponent<SpriteRenderer>();
            }

            return _sprite;
        }
    }

    protected virtual Vector2 forward {
        get {
            return sprite.flipX ? Vector2.left : Vector2.right;
        }
    }

    protected virtual void Flip() {
        sprite.flipX = !sprite.flipX;
    }
}
