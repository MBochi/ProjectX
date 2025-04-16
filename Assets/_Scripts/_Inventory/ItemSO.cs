using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            Stats stats = GameObject.FindWithTag("Player").GetComponent<Stats>();
            PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            if (stats.currentHealth == stats.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.Heal(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        defense
    };
}
