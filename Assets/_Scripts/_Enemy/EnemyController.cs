using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private Stats enemyStats;
    private EnemyHealth enemyHealth;
    private EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;
    private Rigidbody2D rb;
    private KnockBack kb;
    private ImpactFlash flash;
    private DropCoins dropCoins;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private float flashDuration = 0.1f;

    void Start()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
        kb = GetComponent<KnockBack>();
        enemyStats = GetComponent<Stats>();
        flash = GetComponent<ImpactFlash>();
        dropCoins = GetComponent<DropCoins>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 targetPosition = GameObject.Find("Player").transform.position;
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (!kb.IsBeingKnockedBack)
        {
            if (distance >= 2)
            {
                enemyMovement.MoveTowardsTarget(targetPosition, rb, enemyStats.movementSpeed);
                animator.SetBool("isActive", true);
            }
            else
            {
                enemyCombat.Attack(enemyStats.attackDamage);
                animator.SetBool("isActive", false);
            }
        }
    }

    public void OnHit(int damage, Vector2 direction, Vector2 knockback) 
    {
        kb.CallKnockBack(direction, knockback, 0f);
        flash.Flash(spriteRenderer, flashDuration, Color.red);
        enemyHealth.TakeDamage(damage);
    }

    public void OnHit(int damage) 
    {
        enemyHealth.TakeDamage(damage);
    }

    public void OnObjectDestroyed() 
    {
        dropCoins.Drop(enemyStats.bounty);
        GameObject.Destroy(gameObject);
    }
}
