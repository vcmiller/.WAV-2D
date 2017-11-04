using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBR;

[CreateAssetMenu(menuName = "Weapon Info/Generic")]
public class Weapon : ScriptableObject {
    [Header("Capabilities")]
    public bool canAttackUp;
    public bool canAttackDown;

    [Header("Effectiveness")]
    public float damage;
    public float force;
    public float range;

    [Header("Timing")]
    public float attackDur;
    public float attackCooldown;

    [Header("Sounds")]
    public AudioClip swingSound;
    public AudioClip hitSound;

    public virtual void AltAttack() { }
}

[CreateAssetMenu(menuName = "Weapon Info/Sawtooth")]
public class Sawtooth : Weapon {
    public LayerMask onlyTerrain;
    public LayerMask onlyEnemies;
    public TriangleBlade platformPrefab;
    public Slash slashPrefab;

    public override void AltAttack()
    {
        Player p = FindObjectOfType<Player>();

        TriangleBlade tb;
        if (tb = FindObjectOfType<TriangleBlade>())
        {
            Slash(tb.transform.position, (p.transform.position - tb.transform.position), onlyEnemies);
            Destroy(tb.gameObject);
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(p.transform.position, p.facing, float.PositiveInfinity, onlyTerrain.value);
        if (hit.collider)
        {
            TriangleBlade blade = Instantiate(platformPrefab, hit.point, Quaternion.identity);
            blade.Embed(hit.point, p.facing.x < 0);
        }

        Slash(p.transform.position, p.facing, onlyEnemies);

    }

    private void Slash(Vector2 position, Vector2 direction, int layer)
    {
        var hitTriggers = Physics2D.queriesHitTriggers;
        Physics2D.queriesHitTriggers = true;

        foreach (RaycastHit2D enemyHit in Physics2D.RaycastAll(position, direction, float.PositiveInfinity, layer))
        {
            Enemy enemy;
            MonoBehaviour.print(enemyHit.collider.name);
            if (enemy = enemyHit.collider.GetComponent<Enemy>())
            {
                enemy.GetComponent<Health>().ApplyDamage(new Damage(1, enemyHit.point, (direction.normalized + Vector2.up) / 6));
                Instantiate(slashPrefab.gameObject, enemy.transform.position, Quaternion.identity);
            }
        }

        Physics2D.queriesHitTriggers = hitTriggers;
    }
}

[CreateAssetMenu(menuName = "Weapon Info/Square")]
public class Square : Weapon
{

}

[CreateAssetMenu(menuName = "Weapon Info/Sine")]
public class Sine : Weapon
{

}