using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(item.itemName, item.quantity, item.sprite, item.itemDesciption);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else 
            {
                item.quantity = leftOverItems;
            }             
        }
    }

}
