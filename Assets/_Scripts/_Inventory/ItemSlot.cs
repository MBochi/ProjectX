using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item data
    public string itemName;
    public int quantity = 0;
    public Sprite itemSprite;
    public bool isFull = false;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    [SerializeField] private int maxNumberOfItems;

    // Item slot
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image itemImage;

    // Item description slot
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    // Equipment Slots
    [SerializeField] private EquipmentSlot weaponSlot, shieldSlot;

    public GameObject selectedPanel;
    public bool itemSelected = false;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDesciption, ItemType itemType)
    {
        if (isFull)
        {
            return quantity;
        }

        return itemType == ItemType.collectable ? AddCollectableToSlot(itemName, quantity, itemSprite, itemDesciption, itemType) : AddEquipmentToSlot(itemName, quantity, itemSprite, itemDesciption, itemType);
    }

    private int AddCollectableToSlot(string itemName, int quantity, Sprite itemSprite, string itemDesciption, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDesciption;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityTxt.text = maxNumberOfItems.ToString();
            quantityTxt.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityTxt.text = this.quantity.ToString();
        quantityTxt.enabled = true;
        return 0;
    }

    private int AddEquipmentToSlot(string itemName, int quantity, Sprite itemSprite, string itemDesciption, ItemType itemType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDesciption;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.quantity = 1;
        isFull = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnLeftClick();
        if (eventData.button == PointerEventData.InputButton.Right) OnRightClick();
    }
    private void OnLeftClick()
    {
        if (itemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable) 
            {
                this.quantity -= 1;
                quantityTxt.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedPanel.SetActive(true);
            itemSelected = true;

            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
        }
    }

    private void EmptySlot()
    {
        quantityTxt.enabled = false;
        itemImage.sprite = emptySprite;
        itemImage.enabled = false;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
    }

    private void OnRightClick()
    {
        if (itemSelected)
        {
            EquipGear();
        }
    }

    private void EquipGear()
    {
        if (itemType == ItemType.weapon) 
        {
            weaponSlot.EquipGear(itemSprite, itemName, itemDescription);
            EmptySlot();
        }
            
        if (itemType == ItemType.shield) 
        {
            weaponSlot.EquipGear(itemSprite, itemName, itemDescription);
            EmptySlot();
        }
    }

    public void UseItemFromGame()
    {
        bool usable = inventoryManager.UseItem(itemName);
        if (usable)
        {
            this.quantity -= 1;
            quantityTxt.text = this.quantity.ToString();
            
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
    }
}
