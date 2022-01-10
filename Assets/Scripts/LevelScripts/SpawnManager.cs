using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    SpawnPoint[] spawnPoints;
    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
    public SpawnPoint[] GetSpawnPoint()
    {
        return spawnPoints;
    }
}