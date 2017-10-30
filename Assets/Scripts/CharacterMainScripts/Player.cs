using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public WavCharacterProxy proxy { get; private set; }
    public Controller ctrl { get; private set; }

    public CharacterMotor2D motor { get; private set; }
    public PlayerAnimMotor anim { get; private set; }
    public PlayerAttackMotor attack { get; private set; }
    public Health health { get; private set; }

    public ExpirationTimer stunTimer { get; private set; }
    public ExpirationTimer invulnTimer { get; private set; }
    public ExpirationTimer slowTimer { get; private set; }

    public Vector2 hitLaunchSpeed = new Vector2(10, 3);
    public float hitStunTime = 0.5f;

    public float hitInvuln = 1.5f;
    public float hitSlowRatio = 0.25f;
    public float hitSlowDuration = 0.125f;

    public AudioClip dmgSound;
    public AudioClip stepSound;

    public Vector2 facing
    {
        get
        {
            return (anim.sprite.flipX ? Vector2.left : Vector2.right);
        }
    }

    private bool air = false;

    private void Awake() {
        proxy = GetComponent<WavCharacterProxy>();
        ctrl = GetComponent<Controller>();

        motor = GetComponent<CharacterMotor2D>();
        anim = GetComponent<PlayerAnimMotor>();
        attack = GetComponent<PlayerAttackMotor>();
        health = GetComponent<Health>();

        stunTimer = new ExpirationTimer(hitStunTime);
        invulnTimer = new ExpirationTimer(hitInvuln);
        slowTimer = new ExpirationTimer(hitSlowDuration);
        slowTimer.unscaled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            health.ApplyDamage(new Damage(1, transform.position, (transform.position - collision.transform.position).normalized + Vector3.up));
        }
    }

    private void Update() {
        if (stunTimer.expired) {
            motor.enabledAirControl = true;
        }

        if (invulnTimer.expired) {
            health.enabled = true;
        }

        if (slowTimer.expired) {
            Time.timeScale = 1.0f;
            attack.enabled = true;
        }

        if (!motor.grounded) {
            air = true;
        }

        if (motor.grounded && air) {
            air = false;
            Footstep();
        }
    }

    private void DamageNotify(Damage dmg) {
        float y = Mathf.Sign(dmg.dir.y) * hitLaunchSpeed.y;
        float x = Mathf.Sign(dmg.dir.x) * hitLaunchSpeed.x;

        Util.PlayClipAtPoint(dmgSound, transform.position, 1, 0.5f, false, transform);

        motor.velocity = new Vector2(x, y);
        motor.enabledAirControl = false;
        health.enabled = false;
        attack.enabled = false;
        Time.timeScale = hitSlowRatio;

        stunTimer.Set();
        invulnTimer.Set();
        slowTimer.Set();
    }

    public void Footstep() {
        Util.PlayClipAtPoint(stepSound, transform.position, 1, 0.5f, false, transform);
    }
}
