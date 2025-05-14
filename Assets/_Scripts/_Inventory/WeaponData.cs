using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon")]
public class WeaponData : ItemData
{
    public ItemType itemType = ItemType.weapon;
    public string itemHandling = "One-Hand";
    public int maxWeaponDamage, minWeaponDamage;

    public void Equip()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        stats.maxAttackDamage = maxWeaponDamage;
        stats.minAttackDamage = minWeaponDamage;

        stats.UpdateEquipmentStats();
    }

    public void UnEquip()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        foreach (var item in inventoryManager.equipmentSlots)
        {
            if (item.itemType == itemType && !item.slotInUse)
            {
                stats.maxAttackDamage = 0;
                stats.minAttackDamage = 0; 
            }
        }

        stats.UpdateEquipmentStats();
    }
}
