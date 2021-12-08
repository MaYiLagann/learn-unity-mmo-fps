using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static SpawnManager Instance;



    /// <summary>
    /// List of spawn points
    /// </summary>
    SpawnPoint[] spawnPoints;



    /// <summary>
    /// Get random spawn point transform
    /// </summary>
    /// <returns>Transform of spawn point</returns>
    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }

    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake()
    {
        Instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
}
