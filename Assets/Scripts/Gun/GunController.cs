using UnityEngine;

namespace Gun
{
    /// <summary>
    /// this class allows you to rotate the weapon and create an instance of the prefab (BulletForShip)
    /// the weapon rotate towards the mouse positio
    /// the location of the prefab (BulletForShip) will be spawn at BulletSpawnLocation
    /// the prefab (BulletForShip) it will be add as chilld  to BulletParentTransform
    /// this script wil be added to the Player
    /// </summary>
    public class GunController : MonoBehaviour
    {
        [SerializeField]
        // prefab of the bullet
        private GameObject bullet;
        [SerializeField]
        // spawn the bullet at this location
        private Transform bulletSpawnLocation;
        [SerializeField]
        // is the parent of the bullet that will be created
        private Transform bulletParentTransform;

        private void Update()
        {
            WeaponRotation();
            ShootBullet();
        }

        /// <summary>
        /// Rotate the weapon towards mouse position
        /// </summary>
        private void WeaponRotation()
        {
            //The worldspace point created by converting the screen space point at the provided distance z from the camera plane
            Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
            //subtract the distance between the current gameObject and the camera to the initial mouse position:
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
        private void ShootBullet()
        {
            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                Instantiate(bullet, bulletSpawnLocation.position, transform.rotation).transform.parent = bulletParentTransform;
            }
        }
    }
}


