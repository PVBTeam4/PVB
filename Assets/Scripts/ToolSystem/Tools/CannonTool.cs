using UnityEngine;
using Utils;

namespace ToolSystem.Tools
{
    /// <summary>
    /// Handles the functionality of the cannon tool.
    /// Controlled by the ToolController.
    /// </summary>
    public class CannonTool : Tool
    {
        [SerializeField]
        //the bottom of the Gun 
        private Transform Gunfloor;

        // Spawnlocation of the projectile
        [SerializeField]
        private Transform bulletSpawnLocation;

        // A parent object is used to organize all the active projectiles in the scene
        private GameObject _bulletHolderObject;

        //Configuration options regarding the difficulty of handling the gun
        [SerializeField]
        private float rateOfFire, bulletAccuracy;

        //Configuration options regarding the ammunition of the gun
        [SerializeField]
        private int currentAmmo, maxAmmo;

        private void Awake()
        {
            InitializeBulletHolder();
        }

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
            
            Vector3 bulletSpawnPosition = bulletSpawnLocation.position;

            // Muzzle flash spawn
            GameObject muzzleflash = ParticleUtil.SpawnParticle("MuzzleFlash", bulletSpawnPosition);
            Transform onderkantGun = transform.parent;
            muzzleflash.transform.rotation = Quaternion.Euler(90 + transform.eulerAngles.x, onderkantGun.eulerAngles.y, 0);
            muzzleflash.transform.position += transform.forward.Multiply(0.43f);

            // Bullet Spawn
            GameObject bulletGameObject = ParticleUtil.SpawnParticle("BulletForShip", bulletSpawnPosition);
            bulletGameObject.gameObject.transform.rotation = transform.rotation;
            bulletGameObject.transform.parent = _bulletHolderObject.transform;
            bulletGameObject.transform.position += transform.forward.Multiply(1.9f);
        }

        /// <summary>
        /// Called by 'ToolController'. Will indirectly fire a bullet projectile.
        /// </summary>
        public override void UseLeftAction(float pressedValue)
        {
            // Only fire when key is pressed
            if (pressedValue == 0) return;
            FireProjectile();
        }

        /// <summary>
        /// Called by 'ToolController'.Unused as of yet, but could potentially be used to reload the gun.
        /// </summary>
        public override void UseRightAction(float pressedValue)
        {}

        /// <summary>
        /// Rotate the weapon based on the mouse position
        /// </summary>
        /// <param name="location">the mouse location</param>
        public override void MoveTarget(Vector3 location)
        {
            Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
            //subtract the distance between the current gameObject and the camera to the initial mouse position:
            Debug.DrawLine(bulletSpawnLocation.position, mouseToWorld, Color.red);
            Vector3 difference = mouseToWorld - bulletSpawnLocation.position;
            // caculate angleHorizon between z and x ass
            float angleHorizon = Mathf.Atan(difference.x / difference.z) * Mathf.Rad2Deg;
            // caculate angleVertical between z and y ass
            float angleVertical = Mathf.Atan(difference.y / difference.z) * Mathf.Rad2Deg;
            //rotate the gameobject based on the angleHorizon and angleVertical
            transform.rotation = Quaternion.Euler(-angleVertical, angleHorizon, 0f);
            Gunfloor.rotation = Quaternion.Euler(0f, angleHorizon, 0f);
        }
        
        /// <summary>
        /// Creates bullet holder, that holds all Instantiated bullets
        /// </summary>
        private void InitializeBulletHolder()
        {
            _bulletHolderObject = new GameObject {name = "Bullet Holder"};
        }
    }
}
