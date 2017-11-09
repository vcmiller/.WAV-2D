using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class PlayerAttackMotor : BasicMotor<WavCharacterChannels> {
    public bool attacking {
        get {
            return !attackExpirationTimer.expired;
        }
    }

    public Vector2 attackDir { get; private set; }
    public Weapon curWeapon { get; private set; }

    public CooldownTimer attackCooldownTimer { get; private set; }
    public ExpirationTimer attackExpirationTimer { get; private set; }
    public CharacterMotor2D mainMotor { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    public float knockbackSide = 15;
    public float knockbackDown = 3;
    public float knockbackUp = 10;

    public Weapon[] weapons;

    public LayerMask hitMask;

    public GameObject slashPrefab;

    protected override void Start() {
        base.Start();

        attackCooldownTimer = new CooldownTimer(1);
        attackExpirationTimer = new ExpirationTimer(1);
        mainMotor = GetComponent<CharacterMotor2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void TakeInput() {
        if (channels.attack > 0 && channels.attack <= weapons.Length && attackCooldownTimer.Use()) {
            curWeapon = weapons[channels.attack - 1];

            attackCooldownTimer.cooldown = curWeapon.attackCooldown;
            attackExpirationTimer.expiration = curWeapon.attackDur;

            attackExpirationTimer.Set();

            if (channels.altAttack
                || (curWeapon is Sawtooth && FindObjectOfType<TriangleBlade>()))
            {
                curWeapon.AltAttack();
                return;
            }

            var b = mainMotor.box;

            Vector2 pos, size;
            
            if (channels.movement.y > 0.5f && curWeapon.canAttackUp) {
                attackDir = Vector2.up;
                size = new Vector2(b.size.x, curWeapon.range);
                pos = new Vector2(0, (b.size.y + size.y) / 2);
            } else if (channels.movement.y < -0.5f && !mainMotor.grounded && curWeapon.canAttackDown) {
                attackDir = Vector2.down;
                size = new Vector2(b.size.x, curWeapon.range);
                pos = new Vector2(0, (b.size.y + size.y) / -2);
            } else {
                size = new Vector2(b.size.x + curWeapon.range, b.size.y);

                if (sprite.flipX) {
                    attackDir = Vector2.left;
                    pos = new Vector2(curWeapon.range / -2, 0);
                } else {
                    attackDir = Vector2.right;
                    pos = new Vector2(curWeapon.range / 2, 0);
                }
            }

            bool t = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            var hits = Physics2D.OverlapBoxAll((Vector2)transform.position + pos, size, 0, hitMask);
            Physics2D.queriesHitTriggers = t;

            bool anyHit = false;

            foreach (var hit in hits) {
                var h = hit.GetComponent<Health>();
                if (h) {
                    h.ApplyDamage(new Damage(curWeapon.damage, hit.transform.position, attackDir * curWeapon.force));
                    anyHit = true;
                }
            }

            Util.PlayClipAtPoint(curWeapon.swingSound, transform.position, 1, 0.5f, false, null);

            if (anyHit) {
                Util.PlayClipAtPoint(curWeapon.hitSound, transform.position, 1, 0.5f, false, null);

                var slash = Instantiate(slashPrefab);
                slash.transform.position = transform.position + (Vector3)attackDir;
                slash.transform.right = attackDir;

                if (attackDir == Vector2.down) {
                    mainMotor.velocity = Vector2.up * knockbackUp;
                } else if (attackDir == Vector2.up) {
                    mainMotor.velocity = Vector2.down * knockbackDown;
                } else {
                    mainMotor.velocity = -attackDir * knockbackSide;
                }
            }
            // Attack code goes here!
        }
    }
}
