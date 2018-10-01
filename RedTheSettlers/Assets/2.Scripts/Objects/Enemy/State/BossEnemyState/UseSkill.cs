using RedTheSettlers.GameSystem;
using UnityEngine;
using System.Collections.Generic;

namespace RedTheSettlers.Enemys.Boss
{
    public class UseSkill : EnemyState
    {
        public UseSkill(Animator animator, Explode explode, GameTimer explodeLifeTimer, float Power, 
            float explodeLifeTime, TimerCallback pushFireball, int bossPhase)
        {
            this.animator = animator;
            this.explode = explode;
            this.explodeLifeTimer = explodeLifeTimer;
            this.Power = Power;
            this.explodeLifeTime = explodeLifeTime;
            this.pushFireball = pushFireball;
            this.bossPhase = bossPhase;
        }

        public override void DoAction()
        {
            animator.SetTrigger("UseSkill");
            explode = ObjectPoolManager.Instance.ExplodeQueue.Dequeue();
            explode.Setting(bossPhase+1);
            explode.gameObject.SetActive(true);
            explode.AttackArea.Power = (int)Power;

            explodeLifeTimer = GameTimeManager.Instance.PopTimer();
            explodeLifeTimer.SetTimer(explodeLifeTime, false);
            explodeLifeTimer.Callback = pushFireball;
            explodeLifeTimer.StartTimer();
        }
    }
}