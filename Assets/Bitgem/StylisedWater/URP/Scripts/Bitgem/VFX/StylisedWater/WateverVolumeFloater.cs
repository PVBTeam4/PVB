#region Using statements

using UnityEngine;

#endregion

namespace Bitgem.VFX.StylisedWater
{
    public class WateverVolumeFloater : MonoBehaviour
    {
        #region Public fields

        // This value is used to change the Y offset of the floating object
        [SerializeField] private float yOffSet;

        public WaterVolumeHelper WaterVolumeHelper = null;

        #endregion

        #region MonoBehaviour events

        void Update()
        {
            var instance = WaterVolumeHelper ? WaterVolumeHelper : WaterVolumeHelper.Instance;
            if (!instance)
            {
                return;
            }

            transform.position = new Vector3(transform.position.x, instance.GetHeight(transform.position) + yOffSet ?? transform.position.y, transform.position.z);
        }

        #endregion
    }
}