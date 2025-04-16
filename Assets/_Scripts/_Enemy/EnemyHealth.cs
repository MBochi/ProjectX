using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private EnemyController enemyController;
    private Stats stats;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        stats = GetComponent<Stats>();
        Init();
    }

    private void Init()
    {
        stats.currentHealth = stats.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        stats.currentHealth -= stats.currentHealth = damage;

        if (stats.currentHealth <= 0)
        {
            enemyController.OnObjectDestroyed();
        }
    }

    public void Heal(int amount)
    {
        stats.currentHealth += amount;
        if (stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }
    }
}
