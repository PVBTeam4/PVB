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
        {   //set constrain to false
            Rc.constraintActive = false;
            //play animation
            animator.Play("Shoot");
            //invoke if animation is done
            Invoke("ConstrainObject", animator.GetCurrentAnimatorStateInfo(0).length);


        }
        private void ConstrainObject()
        {   //set constrain to true
            Rc.constraintActive = true;
        }
    }
}
