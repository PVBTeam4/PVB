using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTool : Tool
{
    // Prefab of the bullet projectile
    [SerializeField]
    private GameObject bullet;

    // Spawnlocation of the projectile
    [SerializeField]
    private Transform bulletSpawnLocation;

    // A parent object is used to organize all the active projectiles in the scene
    [SerializeField]
    private Transform bulletParentTransform;

    //Configuration options regarding the difficulty of handling the gun
    [SerializeField]
    private float rateOfFire, bulletAccuracy;

    //Configuration options regarding the ammunition of the gun
    [SerializeField]
    private int currentAmmo, maxAmmo;

    /// <summary>
    /// Will utilize 'bulletAccuracy' and/or 'rateOfFire' to configure the spray of bullets using a radius.
    /// </summary>
    private void HandleAccuracy()
    {}

    /// <summary>
    /// Fires/instantiates a bullet projectile and parents it to the 'bulletParentTransform' object.
    /// </summary>
    private void FireProjectile()
    {
        HandleAccuracy();
        Instantiate(bullet, bulletSpawnLocation.position, transform.rotation).transform.parent = bulletParentTransform;
    }

    /// <summary>
    /// Called by 'ToolController'. Will indirectly fire a bullet projectile.
    /// </summary>
    public override void UseLeftAction()
    {
        FireProjectile();
    }

    /// <summary>
    /// Called by 'ToolController'.Unused as of yet, but could potentially be used to reload the gun.
    /// </summary>
    public override void UseRightAction()
    {}

    /// <summary>
    /// Rotates the weapon based on the mouse position
    /// </summary>
    public override void MoveTarget(Vector3 location)
    {
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(location - new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 difference = mouseToWorld - transform.position;
        float angleHorizon = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        float angleVertical = Mathf.Atan2(difference.z, difference.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(angleVertical - 90, -angleHorizon + 90, 0f);
    }
}
