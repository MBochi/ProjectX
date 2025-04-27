using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public bool waveCRRunning = false;

    private bool stopSlider = false;

    [SerializeField] private GameObject coinPrefab;
    private TextMeshProUGUI coinCounterText;
    [SerializeField] private StaticInventoryData staticInventoryData;

    private void Start()
    {
        coinCounterText = GameObject.Find("CoinCounterText").GetComponent<TextMeshProUGUI>();
        coinCounterText.text = staticInventoryData.coinAmount.ToString();
        prefabs = new(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
        currentLevel -= 1;
        progressbar.minValue = 0f;
        progressbar.maxValue = startOffset + timeBetweenWaves * levels[currentLevel].waves.Length;
        SpawnCheckpoints();
    }

    private void SpawnCheckpoints()
    {
        float sliderWidth = progressbar.GetComponent<RectTransform>().sizeDelta.x;
        float zeroValue = progressbar.transform.position.x + (sliderWidth / 2);
        float spawnPointIntervall = startOffset + 1;

        foreach (var wave in levels[currentLevel].waves)
        {
            float valueToIncrement = spawnPointIntervall / progressbar.maxValue;
            float newPos = sliderWidth * valueToIncrement;
            float x = zeroValue - newPos;

            GameObject go = Instantiate(coinPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Progressbar").transform, false);
            spawnPointIntervall += timeBetweenWaves;
        }
    }

    void Update()
    {
        if (!waveIsActive && !waveComplete)
        {
            StartCoroutine(SpawnWaves());
        }
        if (waveComplete)
        {
            if (CheckIfAllEnemiesKilledAndItemsCollected())
            {
                Debug.Log("Wave Complete");
                // UI Popup
                staticInventoryData.coinAmount = Int32.Parse(coinCounterText.text);
            }  
        }
        Progessbar();
    }
    private void Progessbar()
    {
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
    private IEnumerator SpawnWaves()
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

            float timer = Time.deltaTime;
            for (int i = 0; i < shuffle.Count; i++) 
            {
                Instantiate(shuffle[i], this.transform.position, Quaternion.identity);

                if (i == 0) StartCoroutine(WaveTimer(timeBetweenWaves));

                yield return new WaitForSeconds(wave.spawnRate);
            }

            while(currentWave < levels[currentLevel].waves.Length)
            {
                yield return new WaitUntil(() => !waveCRRunning);
                currentWave++;
                break;
            }
        }
        waveIsActive = !waveIsActive;
        waveComplete = !waveComplete;
    }

    private IEnumerator WaveTimer(float timeBetweenWaves)
    {
        waveCRRunning = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        waveCRRunning = false;
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
