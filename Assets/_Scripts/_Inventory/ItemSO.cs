using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDesciption;

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
        attack,
        defense
    };
}
