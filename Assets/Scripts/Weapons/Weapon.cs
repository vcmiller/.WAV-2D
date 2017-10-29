using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Info")]
public class Weapon : ScriptableObject {
    public bool canAttackUp;
    public bool canAttackDown;

    public string stateName;
}
