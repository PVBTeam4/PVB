using UnityEngine;

namespace Gun
{
    public class ScopeAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator leftArrowAnimator, rightArrowAnimator;

        private static readonly int Zoom = Animator.StringToHash("Zoom");

        public void PlayZoomInAnimation()
        {
            leftArrowAnimator.SetBool(Zoom, true);
            rightArrowAnimator.SetBool(Zoom, true);
        }
        
        public void PlayZoomOutAnimation()
        {
            leftArrowAnimator.SetBool(Zoom, false);
            rightArrowAnimator.SetBool(Zoom, false);
        }
    }
}
