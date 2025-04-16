using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class StatsSO : ScriptableObject
{ 
    public int maxHealth;
    public int currentHealth;
    public int defense;
    public int attackDamage;
    public int attackSpeed;
    public int attackRadius;

    public int movementSpeed;

    public int bounty;
}
