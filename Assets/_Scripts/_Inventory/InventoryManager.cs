using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;

    public ItemSlot[] itemSlots;
    public ItemSO[] itemSOs;

    public void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            Time.timeScale = 1f;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = true;
            InventoryMenu.SetActive(false);

        }
        else
        {
            Time.timeScale = 0f;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = false;
            DeselectAllSlots();
            InventoryMenu.SetActive(true);
        }
    }

    public bool UseItem(string itemName)
    {
        foreach (var itemSO in itemSOs)
        {
            if (itemSO.itemName == itemName)
            {
                bool usable = itemSO.UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDesciption, ItemType itemType)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.isFull && itemSlot.name == itemName || itemSlot.quantity == 0)
            {
                int leftOverItems = itemSlot.AddItem(itemName, quantity, sprite, itemDesciption, itemType);
                if (leftOverItems > 0) leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDesciption, itemType);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        foreach (var itemSlot in itemSlots)
        {
            itemSlot.selectedPanel.SetActive(false);
            itemSlot.itemSelected = false;
        }
    }
}

public enum ItemType
{
    collectable,
    weapon,
    shield,
    none
};
