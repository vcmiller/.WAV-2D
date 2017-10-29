using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackMotor : BasicMotor<WavCharacterProxy> {
    public bool attacking {
        get {
            return !attackExpirationTimer.expired;
        }
    }

    public Vector2 attackDir { get; private set; }

    public float attackCooldown;
    public float attackDuration;

    public CooldownTimer attackCooldownTimer { get; private set; }
    public ExpirationTimer attackExpirationTimer { get; private set; }
    public CharacterMotor2D mainMotor { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    public BoxCollider2D triggerUp;
    public BoxCollider2D triggerDown;
    public BoxCollider2D triggerRight;
    public BoxCollider2D triggerLeft;

    public LayerMask hitMask;

    protected override void Awake() {
        base.Awake();

        attackCooldownTimer = new CooldownTimer(attackCooldown);
        attackExpirationTimer = new ExpirationTimer(attackDuration);
        mainMotor = GetComponent<CharacterMotor2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void TakeInput() {
        if (control.attack && attackCooldownTimer.Use()) {
            attackExpirationTimer.Set();

            BoxCollider2D hitbox = null;
            
            if (control.movement.y > 0.5f) {
                attackDir = Vector2.up;
                hitbox = triggerUp;
            } else if (control.movement.y < -0.5f && !mainMotor.grounded) {
                attackDir = Vector2.down;
                hitbox = triggerDown;
            } else {
                if (sprite.flipX) {
                    hitbox = triggerLeft;
                    attackDir = Vector2.left;
                } else {
                    hitbox = triggerRight;
                    attackDir = Vector2.right;
                }
            }

            bool b = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            var hits = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.size, 0, hitMask);
            Physics2D.queriesHitTriggers = b;

            foreach (var hit in hits) {
                var h = hit.GetComponent<Health>();
                if (h) {
                    h.ApplyDamage(new Damage(1, hit.transform.position, attackDir));
                }
            }
            // Attack code goes here!
        }
    }
}
