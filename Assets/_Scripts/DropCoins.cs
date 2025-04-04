using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    public void Drop(int amount)
    {
        for (int i = 0; i < amount; i++) 
        {
            Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
        }  
    }
}
