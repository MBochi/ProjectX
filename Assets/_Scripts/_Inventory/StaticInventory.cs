using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StaticInventory : MonoBehaviour
{
    public StaticInventoryData staticInventoryData;
    public TextMeshProUGUI coinCounterText;
    public TextMeshProUGUI potionCounterText;

    public int coinAmount;
    public int healthPotionAmount;
    public int healAmount;

    private void Awake()
    {
        coinAmount = staticInventoryData.coinAmount;
        healthPotionAmount = staticInventoryData.healthPotionAmount;
        healAmount = staticInventoryData.healAmount;
    }

    private void Start()
    {
        coinCounterText = GameObject.Find("CoinCounterText").GetComponent<TextMeshProUGUI>();
        coinCounterText.text = coinAmount.ToString();

        potionCounterText = GameObject.Find("PotionCounterText").GetComponent<TextMeshProUGUI>();
        potionCounterText.text = healthPotionAmount.ToString();
    }
}