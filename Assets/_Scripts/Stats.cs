using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Stats : MonoBehaviour
{   
    [SerializeField] private StatsSO statsSO;

    public int maxHealth;
    public int currentHealth;
    public int defense;
    public int attackDamage;
    public int attackSpeed;
    public int attackRadius;
    public int movementSpeed;
    public int bounty;

    private void Awake()
    {
        maxHealth = statsSO.maxHealth;
        currentHealth = statsSO.currentHealth;
        defense = statsSO.defense;
        attackDamage = statsSO.attackDamage;
        attackSpeed = statsSO.attackSpeed;
        attackRadius = statsSO.attackRadius;
        movementSpeed = statsSO.movementSpeed;
        bounty = statsSO.bounty;
    }
}


