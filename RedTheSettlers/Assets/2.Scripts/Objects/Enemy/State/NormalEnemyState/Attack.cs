using UnityEngine;

namespace RedTheSettlers.Enemys.Normal
{
    public class Attack : EnemyState
    {
        public Attack(Animator animator)
        {
            this.animator = animator;
        }

        public override void DoAction()
        {
            animator.SetTrigger("Attack");
        }
    }
}