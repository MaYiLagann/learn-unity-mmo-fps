using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single shot gun item
/// </summary>
public class SingleShotGun : Gun
{
    /// <summary>
    /// Using single shot gun
    /// </summary>
    public override void Use()
    {
        Debug.Log("Using gun: " + itemInfo.itemName);
    }
}
