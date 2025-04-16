using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActive = false;

    public ItemSlot[] itemSlots;

    public ItemSO[] itemSOs;

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActive)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = true;
            Time.timeScale = 1f;
            InventoryMenu.SetActive(false);
            menuActive = !menuActive;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActive)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isAbleToMove = false;
            Time.timeScale = 0f;
            InventoryMenu.SetActive(true);
            menuActive = !menuActive;
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

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDesciption)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (!itemSlot.isFull && itemSlot.name == itemName || itemSlot.quantity == 0)
            {
                int leftOverItems = itemSlot.AddItem(itemName, quantity, sprite, itemDesciption);
                if (leftOverItems > 0) leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDesciption);
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
