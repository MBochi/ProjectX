using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private TMP_Text slotName;

    public Sprite itemSprite;
    public string itemName;
    public ItemType itemType;
    public string itemDescription;
    public int minWeaponDamage, maxWeaponDamage, defense;

    public bool slotInUse;

    public GameObject selectedPanel;
    public bool itemSelected;

    // Item description slot
    public TMP_Text equipmentDescriptionNameText, weaponMinAttackDamageText, weaponMaxAttackDamageText, shieldDefenseText;

    private InventoryManager inventoryManager;
    private EquipmentLibrary equipmentLibrary;
    private WeaponLibrary weaponLibrary;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentLibrary>();
        weaponLibrary = GameObject.Find("InventoryCanvas").GetComponent<WeaponLibrary>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) OnLeftClick();
        if (eventData.button == PointerEventData.InputButton.Right) OnRightClick();
    }
    private void OnLeftClick()
    {
        if (itemSelected && slotInUse)
        {
            UnEquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedPanel.SetActive(true);
            itemSelected = true;
        }

        if (slotInUse) 
        {
            inventoryManager.EquipmentInfoPanel.SetActive(true);
            equipmentDescriptionNameText.text = itemName;

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
    }

    private void OnRightClick()
    {
        if (itemSelected && slotInUse)
        {
            UnEquipGear();
        } 
    }
    public void EquipWeapon(string itemName, Sprite itemSprite, ItemType itemType, int maxWeaponDamage, int minWeaponDamage)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotImage.enabled = true;
        slotName.enabled = false;
        this.itemType = itemType;

        this.maxWeaponDamage = maxWeaponDamage;
        this.minWeaponDamage = minWeaponDamage;

        foreach (var item in weaponLibrary.weapons)
        {
            if (item.name == this.itemName)
                item.Equip();
        }

        slotInUse = true;
        inventoryManager.DeselectAllSlots();
    }
    public void EquipGear(string itemName, Sprite itemSprite, ItemType itemType, int defense)
    {    
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotImage.enabled = true;
        slotName.enabled = false;
        this.itemType = itemType;

        this.defense = defense;

        foreach (var item in equipmentLibrary.equipmentItems)
        {
            if (item.name == this.itemName)
                item.Equip();
        }

        slotInUse = true;
        inventoryManager.DeselectAllSlots();
    }

    private void UnEquipGear()
    {
        if (itemType == ItemType.weapon)
        {
            WeaponData weaponSlotData = WeaponData.CreateInstance<WeaponData>();

            weaponSlotData.itemName = itemName;
            weaponSlotData.itemSprite = itemSprite;
            weaponSlotData.maxWeaponDamage = maxWeaponDamage;
            weaponSlotData.minWeaponDamage = minWeaponDamage;

            inventoryManager.AddItem(weaponSlotData);
        }

        if (itemType == ItemType.equipment)
        {
            EquipmentData equipmentSlotData = EquipmentData.CreateInstance<EquipmentData>();

            equipmentSlotData.itemName = itemName;
            equipmentSlotData.itemSprite = itemSprite;
            equipmentSlotData.defense = defense;

            inventoryManager.AddItem(equipmentSlotData);
        }

        equipmentDescriptionNameText.text = "";
        weaponMinAttackDamageText.text = "";
        weaponMaxAttackDamageText.text = "";
        shieldDefenseText.text = "";

        itemSprite = null;
        slotImage.sprite = itemSprite;
        slotImage.enabled = false;
        slotName.enabled = true;
        minWeaponDamage = 0;
        maxWeaponDamage = 0;
        defense = 0;
        slotInUse = false;

        if (itemType == ItemType.weapon)
        {
            foreach (var item in weaponLibrary.weapons)
            {
                if (item.name == this.itemName)
                    item.UnEquip();
            }
        }
        if (itemType == ItemType.equipment) 
        {
            foreach (var item in equipmentLibrary.equipmentItems)
            {
                if (item.name == this.itemName)
                    item.UnEquip();
            }
        }

        inventoryManager.DeselectAllSlots();
    }
}
