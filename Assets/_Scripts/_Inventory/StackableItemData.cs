using UnityEngine;

[CreateAssetMenu(fileName = "New StackableItem", menuName = "New StackableItem")]
public class StackableItemData : ItemData
{
    public ItemType itemType = ItemType.stackable;
    [TextArea] public string itemDescription;

    public bool UseItem()
    {
        Stats stats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (stats.currentHealth == stats.maxHealth) return false;
        else
        {
            // Heal Player
            return true;
        }
    }
}
