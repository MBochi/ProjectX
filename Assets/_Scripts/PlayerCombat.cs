using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayers;
    private Stats stats;
    // TODO AttackRange in stats
    private float attackRange = 1f;
    private Animator animator;
    private Transform attackPoint;
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    [SerializeField] private float knockbackForce = 1f;
    private float animationWindupTime = 0.5f;

    void Start()
    {
        stats = GetComponent<Stats>();
        attackPoint = GameObject.Find("AttackPoint").transform;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Attack(); 
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canAttack)
        {
            animator.SetTrigger("Attack");
            canAttack = false;

            StartCoroutine(AnimationCooldownTimer(animationWindupTime));

        }
    }

    // Align Animation and Hit Detection
    // TODO: Can be deleted if Sprite and Weapon are seperated
    IEnumerator AnimationCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector2 direction = (enemy.transform.position - transform.position).normalized;
                    Vector2 knockback = direction * knockbackForce;

                    damageable.OnHit(stats.GetAttackDamage(), direction, knockback);
                }
            }
        }
        StartCoroutine(AttackCooldownTimer(attackCooldown));
    }

    IEnumerator AttackCooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
