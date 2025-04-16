using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 1f;
    public float decceleration = 1f;
    public float velPower = 1f;

    public bool IsFacingRight { get; private set; }
    private Vector2 _moveInput;

    public bool isAbleToMove = false;
    private Rigidbody2D rb;
    private Stats playerStats;
    private Animator animator;


    private void Start()
    {
        IsFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isAbleToMove)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            if (_moveInput.x != 0) CheckDirectionToFace(_moveInput.x > 0);
            Move(_moveInput, rb);
        }
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    public void Move(Vector2 _moveInput, Rigidbody2D rb)
    {
        float targetSpeed = _moveInput.x * playerStats.movementSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);

        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
