using UnityEngine;

namespace RedTheSettlers.Enemys.Normal
{
    public class Attack : EnemyState
    {
        public Attack(Animator animator, Rigidbody rigidbodyComponent)
        {
            this.animator = animator;
            this.rigidbodyComponent = rigidbodyComponent;
        }

        public override void DoAction()
        {
            animator.SetTrigger("Attack");
            rigidbodyComponent.velocity = Vector3.zero;
        }
    }
}