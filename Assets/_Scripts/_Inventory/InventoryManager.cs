using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject EquipmentPanel, ItemInfoPanel, EquipmentInfoPanel, WeaponStatsContainer, ShieldStatsContainer, minAttackDamageTextContainer, minAttackDamageTextSeperatorContainer;

    public ItemSlot[] itemSlots;
    public EquipmentSlot[] equipmentSlots;
    public List<StackableItemData> consumables;
    public List<EquipmentData> equipment;
    public List<WeaponData> weapons;

    private void Awake()
    {
        consumables = new(Resources.LoadAll<StackableItemData>("ScriptableObjects/Items/Consumables"));
        equipment = new(Resources.LoadAll<EquipmentData>("ScriptableObjects/Items/Equipment"));
        weapons = new(Resources.LoadAll<WeaponData>("ScriptableObjects/Items/Weapons"));
    }
    public void Inventory()
    {
        if (EquipmentPanel.activeSelf)
        {
            Time.timeScale = 1f;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = true;
            EquipmentPanel.SetActive(false);

        }
        else
        {
            Time.timeScale = 0f;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = false;
            DeselectAllSlots();
            EquipmentPanel.SetActive(true);
        }
    }

    public bool UseItem(string itemName)
    {
        foreach (var item in consumables)
        {
            if (item.itemName == itemName)
            {
                bool usable = item.UseItem();
                return usable;
            }
        }
        return false;
    }

    public void AddItem(WeaponData weaponData)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.isFull && itemSlot.quantity == 0)
            {
                itemSlot.AddItem(weaponData);
                return;
            } 
        }
    }
    public void AddItem(EquipmentData equipmentData)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.isFull && itemSlot.quantity == 0)
            {
                itemSlot.AddItem(equipmentData);
                return;
            }
        }
    }
    public int AddItem(StackableItemData stackableItemData, int leftOverItems)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.isFull && itemSlot.name == stackableItemData.itemName || itemSlot.quantity == 0)
            {
                leftOverItems = itemSlot.AddItem(stackableItemData);
                if (leftOverItems > 0) leftOverItems = AddItem(stackableItemData, leftOverItems);
                return leftOverItems;
            }
        }
        return stackableItemData.quantity;
    }

    public void DeselectAllSlots()
    {
        ItemInfoPanel.SetActive(false);
        EquipmentInfoPanel.SetActive(false);
        WeaponStatsContainer.SetActive(false);
        ShieldStatsContainer.SetActive(false);

        foreach (var itemSlot in itemSlots)
        {
            itemSlot.selectedPanel.SetActive(false);
            itemSlot.itemSelected = false;
            itemSlot.itemDescriptionNameText.text = null;
            itemSlot.itemDescriptionText.text = null;
        }

        foreach (var equipmentSlot in equipmentSlots)
        {
            equipmentSlot.selectedPanel.SetActive(false);
            equipmentSlot.itemSelected = false;
            equipmentSlot.equipmentDescriptionNameText.text = null;
            equipmentSlot.weaponMinAttackDamageText.text = null;
            equipmentSlot.weaponMaxAttackDamageText.text = null;
            equipmentSlot.shieldDefenseText.text = null;
        }
    }
}

public enum ItemType
{
    stackable,
    weapon,
    equipment,
    none
};