using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Stats enemyStats;
    private GameObject player;
    private float distance;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Animator animator;

    float acceleration = 1f;
    float decceleration = 1f;
    float velPower = 1f;

    private KnockBack kb;
    private EnemyCombat combat;

    void Start()
    {
        targetPosition = GameObject.Find("Player").transform.position;
        enemyStats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        kb = GetComponent<KnockBack>();
        combat = GetComponent<EnemyCombat>();
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, targetPosition);
        if (!kb.IsBeingKnockedBack) 
        {
            if (distance >= 2)
            {
                MoveTowardsTarget(targetPosition);
                animator.SetBool("isActive", true);
            }
            else
            {
                combat.Attack();
                animator.SetBool("isActive", false);
            }
        }
        
    }
    public void MoveTowardsTarget(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        float targetSpeed = direction.x * enemyStats.GetMovementSpeed();
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration: decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }
}
