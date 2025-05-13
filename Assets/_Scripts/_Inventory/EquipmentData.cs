using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "New Equipment")]
public class EquipmentData : ItemData
{
    public ItemType itemType = ItemType.equipment;
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
