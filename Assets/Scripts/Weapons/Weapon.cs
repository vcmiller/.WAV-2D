using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Info")]
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
}
