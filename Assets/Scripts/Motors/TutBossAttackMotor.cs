using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBossAttackMotor : BasicMotor<TutBossProxy> {
    public bool charging { get; private set; }
    public CharacterMotor2D motor { get; private set; }

    public float chargeWindup;
    public float chargeTime;
    public float chargeEnd;

    public float chargeSpeed;

    protected override void Awake() {
        base.Awake();

        motor = GetComponent<CharacterMotor2D>();
    }

    public override void TakeInput() {
        if (control.attack == 1 && !charging) {
            StartCoroutine(doCharge());
        }
    }

    private IEnumerator doCharge() {
        charging = true;
        yield return new WaitForSeconds(chargeWindup);

        float t = Time.time;

        while (Time.time - t < chargeTime) {
            motor.velocity = Vector2.left * chargeSpeed;
            yield return null;
        }

        motor.velocity = Vector2.zero;

        yield return new WaitForSeconds(chargeEnd);
        charging = false;
    }
}
