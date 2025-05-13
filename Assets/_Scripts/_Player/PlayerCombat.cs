using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    private Stats playerStats;
    private Animator animator;
    private float attackRange = 1f;
    
    private Transform attackPoint;
    private bool isAttacking = false;
    private float attackCooldown = 0.5f;
    private float knockbackForce = 0.5f;
    private float animationWindupTime = 0.5f;

    void Start()
    {
        attackPoint = gameObject.transform.GetChild(0).gameObject.transform;
        playerStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;

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
                    

                    damageable.OnHit(Random.Range(playerStats.minAttackDamage, playerStats.maxAttackDamage), direction, knockback);
                }
            }
        }
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
