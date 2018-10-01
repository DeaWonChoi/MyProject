using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using UnityEngine;

namespace RedTheSettlers.Enemys.Normal
{
    /// <summary>
    /// 파이어볼 발사, enemy 객체는 한번에 하나의 fireball 객체를 보유할 수 있다.
    /// </summary>
    public class AttackPattern2 : Attack
    {
        const float lifeTime = 3f;
        const float speedCorrection = 50f;
        const float Yoffset = 0.55f;

        public AttackPattern2(EnemyFireBall fireBall, GameTimer fireballLifeTimer, Animator animator, 
            Transform transform, Rigidbody rigidbodyComponent, BattlePlayer targetObject, Vector3 position, 
            float fireballSpeed, TimerCallback pushFireball, ChangeStateCallback changeStateCallback, float Power) : base(animator)
        {
            this.fireBall = fireBall;
            this.fireballLifeTimer = fireballLifeTimer;
            this.animator = animator;
            this.transform = transform;
            this.rigidbodyComponent = rigidbodyComponent;
            this.targetObject = targetObject;
            this.fireballSpeed = fireballSpeed;
            this.pushFireball = pushFireball;
            this.changeStateCallback = changeStateCallback;
            this.Power = Power;
        }

        public override void DoAction()
        {
            if (fireballLifeTimer == null)
            {
                base.DoAction();

                Vector3 normalVector = (targetObject.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(normalVector);
                transform.rotation = new Quaternion(0f, transform.rotation.y, 0, transform.rotation.w);

                Vector3 fireRotationposition = transform.rotation * Vector3.forward * 0.2f + transform.position + Vector3.up;
                fireBall.transform.position = fireRotationposition;
                fireBall.rigidbodyComponent.velocity = normalVector * fireballSpeed * GameTimeManager.Instance.DeltaTime * speedCorrection;
                fireBall.AttackArea.Power = (int)Power;

                fireballLifeTimer = GameTimeManager.Instance.PopTimer();
                fireballLifeTimer.SetTimer(lifeTime, false);
                fireballLifeTimer.Callback = pushFireball;
                fireballLifeTimer.StartTimer();
            }
            else
            {
                changeStateCallback(EnemyStateType.Idle);
            }
                
        }
    }
}