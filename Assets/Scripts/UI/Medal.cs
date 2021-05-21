using Global;
using Statistics;
using UnityEngine;

namespace UI
{
    public class Medal : MonoBehaviour
    {
        [SerializeField]
        private GameObject medalImage;
        [SerializeField]
        private GameObject emptyMedalImage;

        [SerializeField] private ToolType toolType;

        private void Awake()
        {
            HideMedalImages();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetupMedal();
        }

        private void HideMedalImages()
        {
            if (medalImage != null)
            {
                medalImage.SetActive(false);
            }

            if (emptyMedalImage != null)
            {
                emptyMedalImage.SetActive(false);
            }
        }

        private void SetupMedal()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager not initialized!");
                return;
            }

            if (GameManager.Instance.StatisticsTracker == null)
            {
                Debug.LogError("StatisticsTracker not initialized!");
                return;
            }

            StatisticsTracker statisticsTracker = GameManager.Instance.StatisticsTracker;

            if (statisticsTracker.isTaskCompleted(toolType))
            {
                medalImage.SetActive(true);
            }
            else
            {
                emptyMedalImage.SetActive(true);
            }
        }
    }
}
