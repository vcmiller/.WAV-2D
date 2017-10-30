using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnTriangleBlade : MonoBehaviour {

    public CharacterMotor2D characterMotor;
    public TriangleBlade blade
    {
        get
        {
            return FindObjectOfType<TriangleBlade>();
        }
    }
    public Collider2D footPoint;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !characterMotor.grounded && blade)
        {
            bool prevVal = Physics2D.queriesHitTriggers;
            Physics2D.queriesHitTriggers = true;
            foreach(Collider2D col in Physics2D.OverlapBoxAll((Vector2)footPoint.bounds.center, footPoint.bounds.extents, 0))
            {
                if (col.GetComponent<TriangleBlade>())
                {
                    characterMotor.velocity.y = characterMotor.jumpSpeed;
                    Destroy(blade.gameObject);
                    break;
                }
            }

            Physics2D.queriesHitTriggers = prevVal;

        }
    }


}
