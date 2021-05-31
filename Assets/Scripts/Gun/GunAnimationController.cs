using UnityEngine;

namespace Gun
{
    public class GunAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private static readonly int OverheatingStateName = Animator.StringToHash("Overheating");

        public void PlayShootAnimation()
        {
            animator.Play("Shoot");
        }

        public void StartOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, true);
        }

        public void StopOverheatAnimation()
        {
            animator.SetBool(OverheatingStateName, false);
        }
    }
}
