using RedTheSettlers.GameSystem;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Enemys
{
    /// <summary>
    /// 보스 클래스, 생성과 동시에 SetType와 SetStatus를 같이 실행해야한다.
    /// 작업자 : 최대원
    /// </summary>
    public class BossEnemy : Enemy
    {
        private const float explodeLifeTime = 10f;
        private Vector3 explosionLocation;
        [SerializeField] private int bossPhase;
        private Vector3 fireballPosition;
        private GameTimer explodeLifeTimer;
        private GameTimer fireballLifeTimer;
        [SerializeField] private Explode explodePrefab;
        private Explode explode;
        public Queue<Explode> explodeList;
        public Queue<EnemyFireBall> LaunchedFireballList;
        private const float attack1Tick = 4.5f;
        private const float attack2Tick = 12.2f;

        private void Start()
        {
            base.Setting();
            Setting();
        }

        protected override void Setting()
        {
            explodeList = new Queue<Explode>();
            LaunchedFireballList = new Queue<EnemyFireBall>();
        }

        public override void ChangeState(EnemyStateType stateType)
        {
            switch (stateType)
            {
                case EnemyStateType.Idle:
                    currentState = new Idle(animator, rigidbodyComponent);
                    break;
                case EnemyStateType.Die:
                    currentState = new Die(animator, DeadTimer, TimeToReturn, new DeadTimerCallback(EndDead));
                    break;
                case EnemyStateType.Damage:
                    currentState = new Damage(animator);
                    break;
                case EnemyStateType.Attack1:
                    if (isAttackable[0])
                    {
                        currentState = new Boss.Attack(animator, bossPhase, fireballLifeTimer, 
                            new TimerCallback(PushFireballTimer), TargetObject, transform, TimeToReturn, 
                            ObjectPoolManager.Instance.FireballQueue, FireBallSpeed,  LaunchedFireballList, 
                            (int)Power);

                        isAttackable[0] = false;
                        Pattern1Timer = GameTimeManager.Instance.PopTimer();
                        Pattern1Timer.SetTimer(attack1Tick, false);
                        Pattern1Timer.Callback = new TimerCallback(SetAttackable1);
                        Pattern1Timer.StartTimer();
                    }
                    break;
                case EnemyStateType.Attack2:
                    if (isAttackable[1])
                    {
                        currentState = new Boss.UseSkill(animator, explode, explodeLifeTimer, 
                            Power, explodeLifeTime, new TimerCallback(UseSkillEnd), bossPhase);

                        isAttackable[1] = false;
                        Pattern1Timer = GameTimeManager.Instance.PopTimer();
                        Pattern1Timer.SetTimer(attack2Tick, false);
                        Pattern1Timer.Callback = new TimerCallback(SetAttackable2);
                        Pattern1Timer.StartTimer();
                    }
                    break;
                case EnemyStateType.Move:
                    currentState = new Move(animator, transform, rigidbodyComponent, destinationPoint,
                        currentPoint, MoveSpeed, currentTile);
                    break;
                default:
                    break;
            }
            base.ChangeState(stateType);
        }

        /// <summary>
        /// 보스 전용 스탯 설정 메서드
        /// </summary>
        /// <param name="HP"></param>
        /// <param name="Power"></param>
        public override void SetStatus(int HP, int Power, bool isLastBoss)
        {
            MaxHp = HP;
            this.Power = Power;
            this.IsLastBoss = isLastBoss;
        }
        public override void SetStatus(int BattleLevel) { }

        public void Damaged(int damaged)
        {
            float tempHP = CurrentHp / MaxHp;
            if(tempHP > 0.8f)
            { 
                bossPhase = 0;
            }
            else if(tempHP > 0.5f)
            {
                bossPhase = 1;
            }
            else if(tempHP > 0.2)
            {
                bossPhase = 2;
            }
            else
            {
                bossPhase = 3;
            }
            base.Damaged(damaged);
        }

        void ShotFireball()
        {

        }

        void EndAttack()
        {
            ChangeState(EnemyStateType.Idle);
        }

        void EndDamage()
        {
            ChangeState(EnemyStateType.Idle);
        }

        void UseSkillStart()
        {
            if(currentState is Boss.UseSkill)
            {
                explodeList.Enqueue(currentState.explode);
                currentState.explode.SkillRangeCircle.SetActive(true);
                currentState.explode.gameObject.transform.position = explosionLocation;
                currentState.explode.isViewingCircle = true;
            }
        }

        void UsingSkill()
        {
            if (currentState is Boss.UseSkill)
            {
                currentState.explode.particle.gameObject.SetActive(true);
                currentState.explode.isViewingCircle = false;
            }
        }

        void UseSkillEnd()
        {
            Explode tempExplode = explodeList.Dequeue();
            ObjectPoolManager.Instance.ExplodeQueue.Enqueue(tempExplode);
            tempExplode.gameObject.SetActive(false);
        }

        void PushFireballTimer()
        {
            EnemyFireBall fireBall = LaunchedFireballList.Dequeue();
            ObjectPoolManager.Instance.FireballQueue.Enqueue(fireBall);
            fireBall.gameObject.SetActive(false);
        }

        void EndSkill()
        {   
            ChangeState(EnemyStateType.Idle);
        }
    }
}

