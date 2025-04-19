using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StaticInventoryData", menuName = "StaticInventoryData")]
public class StaticInventoryData : ScriptableObject
{
    public int coinAmount;
    public int healthPotionAmount;
    public int healAmount;
}
