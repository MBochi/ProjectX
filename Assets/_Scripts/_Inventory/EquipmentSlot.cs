using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private TMP_Text slotName;
    [SerializeField] private ItemType itemType = new();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    private bool slotInUse;

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotImage.enabled = true;
        slotName.enabled = false;

        slotInUse = true;
    }
}
