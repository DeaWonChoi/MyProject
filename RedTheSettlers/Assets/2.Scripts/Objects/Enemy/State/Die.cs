using RedTheSettlers.GameSystem;
using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class Die : EnemyState
    {
        public Die(Animator animator, GameTimer deadTimer, float timeToReturn, DeadTimerCallback deadTimerCallback)
        {
            this.animator = animator;
            this.deadTimer = deadTimer;
            this.timeToReturn = timeToReturn;
            this.deadTimerCallback = deadTimerCallback;
        }

        public override void DoAction()
        {
            animator.SetTrigger("Dead");

            deadTimer = GameTimeManager.Instance.PopTimer();
            deadTimer.SetTimer(timeToReturn, false);
            deadTimer.Callback = new TimerCallback(deadTimerCallback);
            deadTimer.StartTimer();
        }
    }
}