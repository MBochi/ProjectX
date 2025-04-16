using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    private PlayerController playerController;
    private Stats stats;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        stats = GetComponent<Stats>();
        Init();
    }

    private void Init()
    {
        stats.currentHealth = stats.maxHealth;
        healthbar.maxValue = stats.maxHealth;
        healthbar.minValue = 0f;
        healthbar.value = stats.currentHealth;
    }

    public void TakeDamage(int damage)
    {
        stats.currentHealth -= damage;
        healthbar.value = stats.currentHealth;

        if (stats.currentHealth <= 0)
        {
            playerController.OnObjectDestroyed();
        }
    }

    public void Heal(int amount)
    {
        stats.currentHealth += amount;
        if (stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }
        healthbar.value = stats.currentHealth;
    }
}
