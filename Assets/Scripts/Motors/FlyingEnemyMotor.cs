using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMotor : BasicMotor<BasicControlProxy> {
    [HideInInspector]
    public Vector2 velocity;

    public float speed = 5;
    public float acceleration = 5;
    public float queryExtraDistance = 0.1f;

    public LayerMask blockingLayers;

    public BoxCollider2D box { get; private set; }

    protected override void Awake() {
        base.Awake();

        box = GetComponent<BoxCollider2D>();
    }

    public override void TakeInput() {
        bool collider = box.enabled;
        bool startIn = Physics2D.queriesStartInColliders;
        bool triggers = Physics2D.queriesHitTriggers;
        //box.enabled = false;
        Physics2D.queriesHitTriggers = false;
        Physics2D.queriesStartInColliders = false;

        int queryMask = blockingLayers;

        Vector2 move = control.movement * speed;
        velocity = Vector2.MoveTowards(velocity, move, acceleration * Time.deltaTime);

        RaycastHit2D hit;

        Vector2 center = box.transform.TransformPoint(box.offset);
        Vector2 size = new Vector2(box.size.x * box.transform.lossyScale.x, box.size.y * box.transform.lossyScale.y);
        float angle = box.transform.eulerAngles.z;

        Vector2 movement = velocity * Time.deltaTime;
        Vector2 moveDir = movement.normalized;
        float d = movement.magnitude;

        if (d > 0 && (hit = Physics2D.BoxCast(center - moveDir * queryExtraDistance, size, angle, movement, d + queryExtraDistance, queryMask))) {
            Vector2 norm = hit.normal;
            
            float dRem = (d + queryExtraDistance) - hit.distance;
            Vector2 badMovement = movement.normalized * dRem;
            Vector2 comp = Vector3.Project(-badMovement, norm);

            movement += comp;

            velocity += comp / Time.deltaTime;

            SendMessage("HitWall", hit, SendMessageOptions.DontRequireReceiver);
            
        }

        transform.Translate(movement, Space.World);

        //box.enabled = collider;
        Physics2D.queriesHitTriggers = triggers;
        Physics2D.queriesStartInColliders = startIn;
    }
}
