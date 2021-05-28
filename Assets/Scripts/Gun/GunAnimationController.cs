using UnityEngine;
using UnityEngine.Animations;

namespace Gun
{
    public class GunAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private RotationConstraint Rc;


        public void PlayShootAnimation()
        {
            animator.Play("Shoot");

        }

        private void Update()
        {
            Rc.constraintActive = !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
        }
    }
}
