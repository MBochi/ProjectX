using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{   
    [SerializeField] private StatsSO statsSO;

    private int currentHealth;

    void Start()
    {
        this.currentHealth = this.GetMaxHealth();
    }

    #region Getters & Setters
    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }
    public void SetCurrentHealth(int amount)
    {
        this.currentHealth = amount;
    }
    public void AddHealth(int amount)
    {
        this.currentHealth += amount;
        if (this.currentHealth > statsSO.maxHealth)
        {
            this.currentHealth = statsSO.maxHealth;
        }
    }
    public void SubHealth(int amount)
    {
        this.currentHealth -= amount;
        if (this.currentHealth < 0)
        {
            this.currentHealth = 0;
        }
    }
    public int GetMaxHealth()
    {
        return statsSO.maxHealth;
    }
    public void SetMaxHealth(int maxHealth)
    {
        statsSO.maxHealth = maxHealth;
    }
    public int GetMovementSpeed()
    {
        return statsSO.movementSpeed;
    }
    public void SetMovementSpeed(int movementSpeed)
    {
        statsSO.movementSpeed = movementSpeed;
    }
    public int GetDefense()
    {
        return statsSO.defense;
    }
    public void SetDefense(int defense)
    {
        statsSO.defense = defense;
    }
    public int GetAttackDamage()
    {
        return statsSO.attackDamage;
    }
    public void SetAttackDamage(int attackDamage)
    {
        statsSO.attackDamage = attackDamage;
    }

    public int GetBounty()
    {
        return statsSO.bounty;
    }
    public void SetBounty(int bounty)
    {
        statsSO.bounty = bounty;
    }
    #endregion
}


