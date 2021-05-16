using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

namespace UI
{
    /// <summary>
    /// This class is designed to have lifetime in the game
    /// this shows the hp of the character it is added to
    /// hp loses when hit by a trigger prefab and when hp is 0 it wi be destroyed
    /// for now i have used this class for enemy and the trigger is the prefab (BulletForShip)
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {   
        [SerializeField]
        // is an image that shows the character's life
        private Image progressImage;

        [SerializeField] private bool hideWhenFull;

        private void OnEnable()
        {
            SetImageActive(!hideWhenFull);
        }

        /// <summary>
        /// Update ProgressImage
        /// </summary>
        /// <param name="currentProgress"></param>
        /// <param name="maxProgress"></param>
        public void UpdateProgressBar(float currentProgress, float maxProgress)
        {
            if (hideWhenFull)
            {
                bool isNotFull = currentProgress < maxProgress;
                SetImageActive(isNotFull);
            }
            progressImage.fillAmount = currentProgress / maxProgress;
        }
        
        /// <summary>
        /// Hide Image GameObjEct
        /// </summary>
        /// <param name="active">Should show image</param>
        private void SetImageActive(bool active)
        {
            progressImage.transform.parent.gameObject.SetActive(active);
        }
    }
}
