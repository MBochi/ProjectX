using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class StaticInventory : MonoBehaviour
{
    public StaticInventoryData staticInventoryData;

    public int coinAmount;
    public int healthPotionAmount;
    public int healAmount;

    private void Awake()
    {
        coinAmount = staticInventoryData.coinAmount;
        healthPotionAmount = staticInventoryData.healthPotionAmount;
        healAmount = staticInventoryData.healAmount;
    }
}