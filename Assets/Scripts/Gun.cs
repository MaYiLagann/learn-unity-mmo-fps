using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun item
/// </summary>
public abstract class Gun : Item
{
    /// <summary>
    /// Using gun item
    /// </summary>
    public abstract override void Use();

    /// <summary>
    /// Prefab for bullet impact effect
    /// </summary>
    public GameObject bulletImpactPrefab;
}
