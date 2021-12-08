using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    /// <summary>
    /// Graphic object
    /// </summary>
    [SerializeField]
    GameObject graphics;



    /// <summary>
    /// Instance initialization
    /// </summary>
    void Awake()
    {
        graphics.SetActive(false);
    }
}
