using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private Stats stats;
    // TODO AttackRange in stats
    private float attackRange = 1f;
    private Transform attackPoint;
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    private float knockbackForce = 0.5f;

    void Start()
    {
        stats = GetComponent<Stats>();
        attackPoint = gameObject.transform.GetChild(0).gameObject.transform;
    }
    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AnimationCooldownTimer());
        }
    }

    IEnumerator AnimationCooldownTimer()
    {
        Collider2D[] hitDetection = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D collider in hitDetection)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector2 direction = (collider.transform.position - transform.position).normalized;
                    Vector2 knockback = direction * knockbackForce;

                    damageable.OnHit(stats.GetAttackDamage());
                }
            }
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
