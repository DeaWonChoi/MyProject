using RedTheSettlers.Players;
using UnityEngine;

namespace RedTheSettlers.Enemys.Normal
{
    /// <summary>
    /// 기본 공격
    /// </summary>
    public class AttackPattern1 : Attack
    {
        public AttackPattern1(Animator animator,Transform transform, BattlePlayer targetPlayer) : base(animator)
        {
            this.animator = animator;
            this.transform = transform;
            this.targetObject = targetPlayer;
        }

        public override void DoAction()
        {
            Vector3 normalVector = (targetObject.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(normalVector);
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0, transform.rotation.w);
            base.DoAction();
        }
    }
}