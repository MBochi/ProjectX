using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public float spawnRate;
    public List<Enemies> enemies;
}

[System.Serializable]
public struct Enemies
{
    public string name;
    public int amount;
}
