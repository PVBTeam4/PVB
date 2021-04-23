using UnityEngine;

namespace ToolSystem.Tools
{
    /// <summary>
    /// Handles the functionality of the cannon tool.
    /// Controlled by the ToolController.
    /// </summary>
    public class CannonTool : Tool
    {
        // Prefab of the bullet projectile
        [SerializeField]
        private GameObject bullet;
        
        //the bottom of the Gun 
        private Transform gunFloor;

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
            Instantiate(bullet, bulletSpawnLocation.position, transform.rotation).transform.parent = _bulletHolderObject.transform;
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
        /// <param name="mousePosition">the mouse location</param>
        public override void MoveTarget(Vector3 mousePosition)
        {
            MoveGun(mousePosition, Camera.main);
        }

        public void MoveGun(Vector3 mousePosition, Camera cameraForScreenPosition)
        {
            Vector2 angles = CalculateAngles(mousePosition, cameraForScreenPosition);
            float angleHorizon = angles.x;
            float angleVertical = angles.y;
            
            //rotate the gameobject based on the angleHorizon and angleVertical
            transform.rotation = Quaternion.Euler(-angleVertical, angleHorizon, 0f);
            gunFloor.rotation = Quaternion.Euler(0f, angleHorizon, 0f);
        }

        /// <summary>
        /// Calculate angles to mouse position
        /// </summary>
        /// <param name="mousePositionOnScreen">Mouse position on screen</param>
        /// <param name="cameraForScreenPosition">Camera for screen position</param>
        /// <returns>horizontal (X) & vertical (Y) angles</returns>
        private Vector2 CalculateAngles(Vector3 mousePositionOnScreen, Camera cameraForScreenPosition)
        {
            Vector3 mouseToWorld = cameraForScreenPosition.ScreenToWorldPoint(mousePositionOnScreen - new Vector3(0, 0, cameraForScreenPosition.transform.position.z));
            //subtract the distance between the current gameObject and the camera to the initial mouse position:
            Vector3 bulletSpawnPosition = bulletSpawnLocation.position;
            Debug.DrawLine(bulletSpawnPosition, mouseToWorld, Color.red);
            Vector3 difference = mouseToWorld - bulletSpawnPosition;
            // caculate angleHorizon between z and x ass
            float angleHorizon = Mathf.Atan(difference.x / difference.z) * Mathf.Rad2Deg;
            // caculate angleVertical between z and y ass
            float angleVertical = Mathf.Atan(difference.y / difference.z) * Mathf.Rad2Deg;

            return new Vector2(angleHorizon, angleVertical);
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
