using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private EquipmentData equipmentData;
    [SerializeField] private StackableItemData stackableItemData;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int leftOverItems = 0;
            if (weaponData != null) inventoryManager.AddItem(weaponData);
            if (equipmentData != null) inventoryManager.AddItem(equipmentData);
            if (stackableItemData != null) leftOverItems = inventoryManager.AddItem(stackableItemData, leftOverItems);

            if (leftOverItems <= 0) Destroy(gameObject);
            else stackableItemData.quantity = leftOverItems;
        }
    }

}
