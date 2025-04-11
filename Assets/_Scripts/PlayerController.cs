using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Stats stats;
    [SerializeField] private Slider healthbar;

    void Start()
    {
        stats = GetComponent<Stats>();
        healthbar.maxValue = stats.GetMaxHealth();
        healthbar.minValue = 0f;
    }

    public void OnHit(int damage, Vector2 direction, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(int damage)
    {
        TakeDamage(damage);
        healthbar.value = stats.GetCurrentHealth();
    }

    public void OnObjectDestroyed()
    {
        GameObject.Destroy(gameObject);
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
    private void TakeDamage(int damage)
    {
        stats.SetCurrentHealth(stats.GetCurrentHealth() - damage);

        if (stats.GetCurrentHealth() <= 0)
        {
            OnObjectDestroyed();
        }
    }
}
