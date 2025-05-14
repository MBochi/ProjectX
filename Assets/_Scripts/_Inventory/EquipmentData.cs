using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "New Equipment")]
public class EquipmentData : ItemData
{
    public ItemType itemType = ItemType.equipment;
    public string itemHandling = "One-Hand";
    public int defense;

    public void Equip()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        stats.defense = defense;
        stats.UpdateEquipmentStats();
    }

    public void UnEquip()
    {
        Stats stats = GameObject.Find("Player").GetComponent<Stats>();
        InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        foreach (var item in inventoryManager.equipmentSlots)
        {
            if (item.itemType == itemType && !item.slotInUse) stats.defense = 0;
        }
        stats.UpdateEquipmentStats();
    }
}
