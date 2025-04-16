using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamageable
{
    private PlayerHealth playerHealth;
    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();

        if (SceneManager.GetSceneByName("HUB").isLoaded)
        {
            playerMovement.isAbleToMove = true;
        }
    }

    public void Attack()
    {
        playerCombat.Attack();
    }

    public void OnHit(int damage, Vector2 direction, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public void OnObjectDestroyed()
    {
        GameObject.Destroy(gameObject);
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
}
