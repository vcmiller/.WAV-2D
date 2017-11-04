using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

public class TutBossAttackMotor : BasicMotor<TutBossChannels> {
    public bool charging { get; private set; }
    public bool meleeing { get; private set; }
    public CharacterMotor2D motor { get; private set; }
    public int facing { get; private set; }

    public float chargeWindup;
    public float chargeTime;
    public float chargeEnd;
    public float chargeSpeed;

    public float meleeWindup;
    public float meleeDashTime;
    public float meleeMid;
    public float meleeEnd;
    public float meleeDashSpeed;

    public BoxCollider2D chargeHitbox;
    public BoxCollider2D meleeHitbox;

    public LayerMask hitMask;
    public float damage;

    protected override void Start() {
        base.Start();

        motor = GetComponent<CharacterMotor2D>();
        facing = 1;
    }

    public override void TakeInput() {
        if (channels.attack == 1 && !charging && !meleeing) {
            StartCoroutine(doCharge());
        } else if (channels.attack == 2 && !charging && !meleeing) {
            StartCoroutine(doMelee());
        }
    }

    private IEnumerator doMelee() {
        meleeing = true;
        facing = channels.attackDir;

        yield return new WaitForSeconds(meleeWindup);

        float t = Time.time;

        while (Time.time - t < meleeDashTime) {
            motor.velocity = Vector2.right * facing * meleeDashSpeed;

            yield return null;
        }

        motor.velocity = Vector2.zero;

        hitTest(meleeHitbox.offset, meleeHitbox.size);

        t = Time.time;
        while (Time.time - t < meleeMid) {
            yield return null;
        }

        if (channels.attackDir != facing) {
            meleeing = false;
            yield break;
        }

        t = Time.time;

        while (Time.time - t < meleeDashTime) {
            motor.velocity = Vector2.right * facing * meleeDashSpeed;

            yield return null;
        }

        motor.velocity = Vector2.zero;
        
        hitTest(meleeHitbox.offset, meleeHitbox.size);

        t = Time.time;
        while (Time.time - t < meleeMid) {
            yield return null;
        }

        yield return new WaitForSeconds(meleeEnd);

        meleeing = false;
    }

    private IEnumerator doCharge() {
        charging = true;
        facing = channels.attackDir;

        yield return new WaitForSeconds(chargeWindup);

        float t = Time.time;

        while (Time.time - t < chargeTime) {
            motor.velocity = Vector2.right * facing * chargeSpeed;

            hitTest(chargeHitbox.offset, chargeHitbox.size);

            yield return null;
        }

        motor.velocity = Vector2.zero;

        yield return new WaitForSeconds(chargeEnd);
        charging = false;
    }

    private void hitTest(Vector3 offset, Vector3 size) {
        Vector3 o = offset;
        o.x *= facing;

        foreach (var hit in Physics2D.OverlapBoxAll(transform.position + o, size, 0, hitMask)) {
            var h = hit.GetComponent<Health>();
            if (h) {
                h.ApplyDamage(new Damage(damage, transform.position, Vector3.right * facing));
            }
        }
    }
}
