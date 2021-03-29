using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    // prefab of the bullet
    private GameObject Bullet;
    [SerializeField]
    // spawn the bullet at this location
    private Transform BulletSpawnLocation;
    [SerializeField]
    // is the parent of the bullet that will be created
    private Transform BulletParentTransform;

    void Update()
    {
        Rotate();
        Shoot();
    }

    /// <summary>
    /// Rotate the weapon towards mouse position
    /// </summary>
    void Rotate()
    {
        //The worldspace point created by converting the screen space point at the provided distance z from the camera plane
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
        //subtract the distance between the current gameobject and the camera to the initial mouse position:
        Vector3 difference = mouseToWorld - transform.position;
        // caculate angleHorizon between z and x ass
        float angleHorizon = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        // caculate angleVertical between z and y ass
        float angleVertical = Mathf.Atan2(difference.z, difference.y) * Mathf.Rad2Deg;
        //rotate the gameobject based on the angleHorizon and angleVertical
        transform.rotation = Quaternion.Euler(angleVertical - 90, -angleHorizon + 90, 0f);
    }
    /// <summary>
    /// pressing the left mouse button creates an object based on the prefab
    /// it will be spawn at the location of BulletSpawnLocation
    /// it will be added to the BulletParentTransform as a child
    /// </summary>
    void Shoot()
    {
        if ( Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, BulletSpawnLocation.position, transform.rotation).transform.parent = BulletParentTransform;
        }
    }
}


