using System;
using UnityEngine;
using Utils;
using System.Collections;
using Gun;

namespace ToolSystem.Tools
{
    /// <summary>
    /// Handles the functionality of the cannon tool.
    /// Controlled by the ToolController.
    /// </summary>
    public class CannonTool : Tool
    {
    
        // Spawn location of the projectile
        [SerializeField]
        private Transform bulletSpawnLocation;

        [SerializeField] private Transform debugRay;

        // A parent object is used to organize all the active projectiles in the scene
        private GameObject _bulletHolderObject;

        //Configuration options regarding the difficulty of handling the gun
        [SerializeField]
        private float waitToFire = 0.75f, bulletAccuracy;

        private bool canShoot = true;

        private ZoomGun _zoomGun;

        private Vector3 _intersectionPoint;

        [Header("Gun Movement"), SerializeField, Range(0, 1)]
        private float lerpValue;
        
        [SerializeField, Header("Gun Movement Constraints")]
        private float minimalTargetDistanceFromGun;
        [SerializeField, Range(0, 1)]
        private float minimalForwardDirection;

        private void Awake()
        {
            _zoomGun = GetComponent<ZoomGun>();
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
            muzzleflash.transform.rotation = Quaternion.Euler(90 + transform.eulerAngles.x, transform.eulerAngles.y, 0);
            muzzleflash.transform.position += transform.forward.Multiply(0.43f);

            // Bullet Spawn
            GameObject bulletGameObject = ParticleUtil.SpawnParticle("BulletForShip", bulletSpawnPosition);
            bulletGameObject.gameObject.transform.rotation = transform.rotation;
            bulletGameObject.transform.parent = _bulletHolderObject.transform;
            bulletGameObject.transform.position += transform.forward.Multiply(0.2f);
        }

        /// <summary>
        /// Called by 'ToolController'. Will indirectly fire a bullet projectile.
        /// </summary>
        public override void UseLeftAction(float pressedValue)
        {
            // Only fire when key is pressed
            if (pressedValue == 0) return;

            if (!canShoot) return;

            // Disable shooting
            canShoot = false;

            IEnumerator coroutine = EnableShooting(waitToFire);
            StartCoroutine(coroutine);

            FireProjectile();
        }

        /// <summary>
        /// Called by 'ToolController'.Unused as of yet, but could potentially be used to reload the gun.
        /// </summary>
        public override void UseRightAction(float pressedValue)
        {
            HandleZoom(pressedValue > 0);
        }

        /// <summary>
        /// Wait the given amount of time to enable the shooting
        /// </summary>
        /// <param name="waitTime">float how long we need to wait to shoot again</param>
        /// <returns></returns>
        private IEnumerator EnableShooting(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            canShoot = true;
        }

        /// <summary>
        /// Rotate the weapon based on the mouse position
        /// </summary>
        /// <param name="location">the mouse location</param>
        public override void MoveTarget(Vector3 location)
        {
            _intersectionPoint = Vector3.Lerp(_intersectionPoint, GetTargetPointWithConstraints(), lerpValue);
            debugRay.transform.position = _intersectionPoint;
            //draw line from bulletSpawnLocation.position to _intersectionPoint position
            Debug.DrawLine(bulletSpawnLocation.position, _intersectionPoint, Color.red);
            // rotate gun to _intersectionPoint
            transform.LookAt(_intersectionPoint);
        }

        /// <summary>
        /// Creates bullet holder, that holds all Instantiated bullets
        /// </summary>
        private void InitializeBulletHolder()
        {
            _bulletHolderObject = new GameObject {name = "Bullet Holder"};
        }

        /// <summary>
        /// Handles zoom of gun
        /// </summary>
        /// <param name="pressed">Whether right click is pressed or not</param>
        private void HandleZoom(bool pressed)
        {
            if (_zoomGun == null)
            {
                Debug.LogError("ZoomGun component could not be found!");
                return;
            }

            if (pressed)
            {
                _zoomGun.ZoomIn();
            }
            else
            {
                _zoomGun.ZoomOut();
            }
        }

        /// <summary>
        /// Calculate position gun should shoot at
        /// </summary>
        /// <returns>Position to shoot at</returns>
        private Vector3 GetTargetPointWithConstraints()
        {
            // Point mouse raycast hit
            Vector3 intersectionPoint = GetRayIntersectionPoint();
            // Gun origin position
            Vector3 gunOrigin = transform.position;
            gunOrigin.y = intersectionPoint.y;

            // If intersection point is within minimalTargetDistance from gunOrigin
            if (Vector3.Distance(gunOrigin, intersectionPoint) < minimalTargetDistanceFromGun)
            {
                Vector3 direction = intersectionPoint - gunOrigin;
                
                // Prevents target point from getting behind gun
                direction.z = Math.Max(minimalForwardDirection, direction.z);
                
                direction.Normalize();
                
                // Calculate offset and add this offset to gunOrigin
                Vector3 offset = direction * minimalTargetDistanceFromGun;
                return gunOrigin + offset;
            }
            
            return intersectionPoint;
        }

        /// <summary>
        /// Calculate position mouse is aiming at, with a raycast
        /// </summary>
        /// <returns>Position mouse is aiming at</returns>
        private Vector3 GetRayIntersectionPoint()
        {
            // Shoot raycast & return intersection point
            Ray ray = Camera.main.ScreenPointToRay (UnityEngine.Input.mousePosition);
            return Physics.Raycast(ray, out var hit, 10000, ~LayerMask.GetMask($"Player", $"Bullet", $"Ignore Raycast")) ? hit.point : Vector3.zero;
        }
    }
}
