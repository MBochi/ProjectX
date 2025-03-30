using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float respawnTime;
    private bool isSpawning;
    static System.Random rnd = new System.Random();

    void Update()
    {
        if (!isSpawning)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        List<GameObject> list = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
        int randomEntry = rnd.Next(list.Count);
        
        isSpawning = !isSpawning;
        Instantiate(list[randomEntry], this.transform.position, Quaternion.identity);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(respawnTime);
        isSpawning = !isSpawning;
    }
}
