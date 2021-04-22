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

        public void UpdateProgressBar(float currentProgress, float maxProgress)
        {
            progressImage.fillAmount = currentProgress / maxProgress;
        }
    }
}
