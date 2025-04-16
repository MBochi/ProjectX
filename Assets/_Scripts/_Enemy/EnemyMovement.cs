using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float acceleration = 1f;
    float decceleration = 1f;
    float velPower = 1f;

    public void MoveTowardsTarget(Vector3 targetPosition, Rigidbody2D rb, int movementSpeed)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        float targetSpeed = direction.x * movementSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration: decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }
}
