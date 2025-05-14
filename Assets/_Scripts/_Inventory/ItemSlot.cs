using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int quantity = 0;
    public Sprite itemSprite;
    public bool isFull = false;
    public string itemDescription;
    public ItemType itemType;
    public string itemHandling;
    public int minWeaponDamage, maxWeaponDamage, defense;
    public int sellingPrice;

    [SerializeField] private int maxNumberOfItems;

    // Item Slot
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image itemImage;

    // Item Description Slot
    public TMP_Text itemDescriptionNameText, itemDescriptionText;

    // Equipment Item Description slot
    public TMP_Text equipmentDescriptionNameText, equipmentHandlingText, weaponMinAttackDamageText, weaponMaxAttackDamageText, shieldDefenseText, sellingPriceText;

    // Equipment Slots
    [SerializeField] private EquipmentSlot weaponSlot, equipmentSlot;

    public GameObject selectedPanel;
    public bool itemSelected = false;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public void AddItem(WeaponData weaponData)
    {
        AddWeaponToSlot(weaponData);
    }
    public void AddItem(EquipmentData equipmentData)
    {
        AddEquipmentToSlot(equipmentData);
    }
    public int AddItem(StackableItemData stackableItemData)
    {
        if (isFull) return quantity;
        return AddStackableToSlot(stackableItemData);
    }
    private void AddWeaponToSlot(WeaponData weaponData)
    {
        this.itemName = weaponData.itemName;
        this.itemType = weaponData.itemType;
        this.itemSprite = weaponData.itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.itemHandling= weaponData.itemHandling;
        this.maxWeaponDamage = weaponData.maxWeaponDamage;
        this.minWeaponDamage = weaponData.minWeaponDamage;
        this.sellingPrice = weaponData.sellingPrice;

        this.quantity = 1;
        isFull = true;
    }
    private void AddEquipmentToSlot(EquipmentData equipmentData)
    {
        this.itemName = equipmentData.itemName;
        this.itemType = equipmentData.itemType;
        this.itemSprite = equipmentData.itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.itemHandling = equipmentData.itemHandling;
        this.defense = equipmentData.defense;
        this.sellingPrice = equipmentData.sellingPrice;

        this.quantity = 1;
        isFull = true;
    }
    private int AddStackableToSlot(StackableItemData stackableItemData)
    {
        this.itemName = stackableItemData.itemName;
        this.itemType = stackableItemData.itemType;
        this.itemSprite = stackableItemData.itemSprite;
        this.itemDescription = stackableItemData.itemDescription;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        this.quantity += stackableItemData.quantity;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnLeftClick();
        if (eventData.button == PointerEventData.InputButton.Right) OnRightClick();
    }
    private void OnLeftClick()
    {
        if (itemSelected)
        {
            if (this.itemType == ItemType.stackable) 
            {
                foreach (var item in inventoryManager.consumables)
                {
                    if (this.itemName == item.itemName)
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
            }

            if (this.itemType == ItemType.weapon || this.itemType == ItemType.equipment)
            {
                EquipGear();
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedPanel.SetActive(true);
            itemSelected = true;

            if (this.quantity == 0)
            {
                return;
            }

            if (this.itemType == ItemType.weapon || this.itemType == ItemType.equipment)
            {
                inventoryManager.EquipmentInfoPanel.SetActive(true);
                equipmentDescriptionNameText.text = itemName;
                equipmentHandlingText.text = itemHandling;
                sellingPriceText.text = sellingPrice.ToString();

                if (this.itemType == ItemType.weapon)
                {
                    inventoryManager.WeaponStatsContainer.SetActive(true);
                    weaponMinAttackDamageText.text = this.minWeaponDamage.ToString();
                    weaponMaxAttackDamageText.text = this.maxWeaponDamage.ToString();
                }
                if (this.itemType == ItemType.equipment) 
                { 
                    inventoryManager.ShieldStatsContainer.SetActive(true);
                    shieldDefenseText.text = this.defense.ToString();
                } 
            }
            else
            {
                inventoryManager.ItemInfoPanel.SetActive(true);
                itemDescriptionNameText.text = itemName;
                itemDescriptionText.text = itemDescription;
            }
        }
    }

    private void OnRightClick()
    {
        if (itemSelected)
        {
            EquipGear();
        }
    }

    private void EmptySlot()
    {
        this.isFull = false;
        this.itemSelected = false;

        this.quantityTxt.enabled = false;
        this.quantity = 0;
        this.itemSprite = null;
        this.itemImage.sprite = null;
        this.itemImage.enabled = false;
        this.minWeaponDamage = 0;
        this.maxWeaponDamage = 0;
        this.defense = 0;

        this.itemName = "";
        this.itemDescription = "";
        this.itemType = ItemType.none;
        this.itemHandling = null;
        this.sellingPrice = 0;
    }

    private void EquipGear()
    {
        if (itemType == ItemType.weapon) 
        {
            WeaponData weaponSlotData = WeaponData.CreateInstance<WeaponData>();

            weaponSlotData.itemName = weaponSlot.itemName;
            weaponSlotData.itemSprite = weaponSlot.itemSprite;
            weaponSlotData.itemHandling = weaponSlot.itemHandling;
            weaponSlotData.maxWeaponDamage = weaponSlot.maxWeaponDamage;
            weaponSlotData.minWeaponDamage = weaponSlot.minWeaponDamage;
            weaponSlotData.sellingPrice = weaponSlot.sellingPrice;

            weaponSlot.EquipWeapon(itemName, itemSprite, itemType, itemHandling, maxWeaponDamage, minWeaponDamage, sellingPrice);
            EmptySlot();

            if (weaponSlotData.itemName != null && weaponSlotData.itemSprite != null)
            {
                AddItem(weaponSlotData);
            }
        }
            
        if (itemType == ItemType.equipment) 
        {
            EquipmentData equipmentSlotData = EquipmentData.CreateInstance<EquipmentData>();

            equipmentSlotData.itemName = equipmentSlot.itemName;
            equipmentSlotData.itemSprite = equipmentSlot.itemSprite;
            equipmentSlotData.itemHandling = equipmentSlot.itemHandling;
            equipmentSlotData.defense = equipmentSlot.defense;
            equipmentSlotData.sellingPrice = equipmentSlot.sellingPrice;

            equipmentSlot.EquipGear(itemName, itemSprite, itemType, itemHandling, defense, sellingPrice);
            EmptySlot();

            if (equipmentSlotData.itemName != null && equipmentSlotData.itemSprite != null)
            {
                AddItem(equipmentSlotData);
            }
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
