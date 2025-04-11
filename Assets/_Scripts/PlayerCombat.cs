using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    private Stats stats;
    // TODO AttackRange in stats
    private float attackRange = 1f;
    private Animator animator;
    private Transform attackPoint;
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    private float knockbackForce = 0.5f;
    private float animationWindupTime = 0.5f;

    void Start()
    {
        stats = GetComponent<Stats>();
        attackPoint = gameObject.transform.GetChild(0).gameObject.transform;
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        if (canAttack)
        {
            animator.SetTrigger("Attack");
            canAttack = false;

            StartCoroutine(AnimationCooldownTimer(animationWindupTime));

        }
    }
    IEnumerator AnimationCooldownTimer(float animationWindupTime)
    {
        yield return new WaitForSeconds(animationWindupTime);

        Collider2D[] hitDetection = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D collider in hitDetection)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector2 direction = (collider.transform.position - transform.position).normalized;
                    Vector2 knockback = direction * knockbackForce;

                    damageable.OnHit(stats.GetAttackDamage(), direction, knockback);
                }
            }
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
