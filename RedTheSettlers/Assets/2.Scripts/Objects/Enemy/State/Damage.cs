using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class Damage : EnemyState
    {
        public Damage(Animator animator)
        {
            this.animator = animator;
        }

        public override void DoAction()
        {
            animator.SetTrigger("Damage");
        }
    }
}