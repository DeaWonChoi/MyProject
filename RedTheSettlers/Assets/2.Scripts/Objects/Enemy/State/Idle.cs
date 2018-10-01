using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class Idle : EnemyState
    {
        public Idle(Animator animator, Rigidbody rigidbodyComponent)
        {
            this.animator = animator;
            this.rigidbodyComponent = rigidbodyComponent;
        }

        public override void DoAction()
        {
            animator.SetBool("IsMoving", false);
        }
    }
}