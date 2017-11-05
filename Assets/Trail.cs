using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    CooldownTimer ct;
    public LineRenderer lr;
    float speed;

    private void Update()
    {
        var oldPos = lr.GetPosition(1);
        var endPos = lr.GetPosition(0);
        lr.SetPosition(1, Vector2.MoveTowards(oldPos, endPos, Vector2.Distance(oldPos, endPos) * Time.deltaTime * speed));

        if (ct != null && ct.Use())
        {
            Sudoku();
        }
    }

    public void SetTrail(Vector2 position, float timeTilDestroyed)
    {
        lr = GetComponent<LineRenderer>();
        this.speed = 1 / timeTilDestroyed;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, position);
        ct = new CooldownTimer(timeTilDestroyed);
    }

    public void Sudoku()
    {
        Destroy(gameObject);
    }
}
