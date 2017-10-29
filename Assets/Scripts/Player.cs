using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public WavCharacterProxy proxy { get; private set; }
    public WavCharacterController ctrl { get; private set; }

    public CharacterMotor2D motor { get; private set; }
    public PlayerAnimMotor anim { get; private set; }
    public PlayerAttackMotor attack { get; private set; }
    public Health health { get; private set; }

    public float hitLaunchSpeed = 5;

    private void Awake() {
        proxy = GetComponent<WavCharacterProxy>();
        ctrl = GetComponent<WavCharacterController>();

        motor = GetComponent<CharacterMotor2D>();
        anim = GetComponent<PlayerAnimMotor>();
        attack = GetComponent<PlayerAttackMotor>();
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            health.ApplyDamage(new Damage(1, transform.position, transform.position - collision.transform.position + Vector3.up));
        }
    }

    private void DamageNotify(Damage dmg) {
        motor.velocity = dmg.dir * hitLaunchSpeed;
    }
}
