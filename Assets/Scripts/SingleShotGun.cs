using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single shot gun item
/// </summary>
public class SingleShotGun : Gun
{
    /// <summary>
    /// Camera from player
    /// </summary>
    [SerializeField] new Camera camera;



    /// <summary>
    /// Using single shot gun
    /// </summary>
    public override void Use()
    {
        Shoot();
    }



    /// <summary>
    /// Shoot the gun
    /// </summary>
    private void Shoot()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = camera.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)itemInfo).damage);
        }
    }
}
