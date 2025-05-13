using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public int quantity;
    public Sprite sprite;
    [TextArea] public string itemDescription;

    public int minWeaponDamage, maxWeaponDamage, defense;

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

    public void EquipItem()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        if (itemType == ItemType.weapon)
        {
            stats.minAttackDamage = minWeaponDamage;
            stats.maxAttackDamage = maxWeaponDamage;
        }
        if (itemType == ItemType.equipment)
        {
            stats.defense = defense;
        }

        stats.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        foreach (var item in inventoryManager.equipmentSlots)
        {
            if (item.itemType == ItemType.weapon && !item.slotInUse)
            {
                stats.minAttackDamage = 0;
                stats.maxAttackDamage = 0;
            }
            if (item.itemType == ItemType.equipment && !item.slotInUse)
            {
                stats.defense = 0;
            }
        }

        stats.UpdateEquipmentStats();
    }
}
