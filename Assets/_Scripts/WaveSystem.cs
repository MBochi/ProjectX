using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Level
{
    public WaveSO[] waves;
}
public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Slider progressbar;

    [SerializeField] private Level[] levels;
    [SerializeField] private int currentLevel;
    [SerializeField] private float startOffset = 2f;
    [SerializeField] private float timeBetweenWaves = 10f;

    private List<GameObject> prefabs = null;
    private bool waveIsActive = false;
    private bool waveComplete = false;

    private bool stopSlider = false;

    private void Start()
    {
        prefabs = new(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
        currentLevel -= 1;
    }

    void Update()
    {
        if (!waveIsActive && !waveComplete)
        {
            StartCoroutine(SpawnEnemy());
        }
        if (waveComplete)
        {
            if (CheckIfAllEnemiesKilledAndItemsCollected())
            {
                Debug.Log("Wave Complete");
            }  
        }
        Progessbar();
    }
    private void Progessbar()
    {
        progressbar.minValue = 0f;
        progressbar.maxValue = startOffset + timeBetweenWaves * levels[currentLevel].waves.Length;
        float time = progressbar.minValue + Time.time;

        if (progressbar.maxValue <= time)
        {
            stopSlider = true;
        }
        else if (!stopSlider)
        {
            progressbar.value = time;
        }
    }
    private IEnumerator SpawnEnemy()
    {
        waveIsActive = !waveIsActive;
        int currentWave = 1;
        yield return new WaitForSeconds(startOffset);

        foreach (var wave in levels[currentLevel].waves)
        {
            Debug.Log("Current Wave: " + currentWave);
            List<GameObject> listEnemies = new();

            foreach (var enemy in wave.enemies)
            {
                if (enemy.amount > 0)
                {
                    for (int i = 0; i < enemy.amount; i++) 
                    {
                        listEnemies.Add(FindPrefabWithName(enemy.name));
                    }
                }
            }

            var shuffle = listEnemies.OrderBy(i => Guid.NewGuid()).ToList();

            foreach (var enemy in shuffle)
            {
                Instantiate(enemy, this.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(wave.spawnRate);
            }

            if (currentWave < levels[currentLevel].waves.Length)
            {
                currentWave++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            
        }
        waveIsActive = !waveIsActive;
        waveComplete = !waveComplete;
    }

    private GameObject FindPrefabWithName(string name)
    {
        GameObject prefab = null;

        foreach (var item in prefabs)
        {
            if (item.name == name)
            {
                prefab = item;
            }
        }

        if (prefab != null)
        {
            return prefab;
        }
        else 
        {
            throw new Exception("No Prefabs to associated attribute found " +  name);
        }
    }

    private bool CheckIfAllEnemiesKilledAndItemsCollected()
    {
        if (GameObject.FindWithTag("Enemy") != null || GameObject.FindWithTag("Collectable") != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
