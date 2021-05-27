using UnityEngine;

namespace Gun
{
    public class GunAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void PlayShootAnimation()
        {
            animator.Play("Shoot");
        }
    }
}
