using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item
/// </summary>
public abstract class Item : MonoBehaviour
{
    /// <summary>
    /// Item information
    /// </summary>
    public ItemInfo itemInfo;
    /// <summary>
    /// GameObject for item
    /// </summary>
    public GameObject itemGameObject;

    /// <summary>
    /// Using item
    /// </summary>
    public abstract void Use();
}
