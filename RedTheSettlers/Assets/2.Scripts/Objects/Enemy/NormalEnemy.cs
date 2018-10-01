using RedTheSettlers.GameSystem;
using RedTheSettlers.UnitTest;

namespace RedTheSettlers.Enemys
{
    /// <summary>
    /// 일반 몬스터 클래스 생성과 동시에 SetType와 SetStatus를 같이 실행해야한다.
    /// 작업자 : 최대원
    /// </summary>
    public class NormalEnemy : Enemy
    {
        private const float attack1Tick = 3.1f;
        private const float attack2Tick = 8.9f;

        private void Start()
        {
            Setting();
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
                        currentState = new Normal.AttackPattern1(animator, transform, TargetObject);

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
                        currentState = new Normal.AttackPattern2(
                        PopFireBall(),
                        FireBallLifeTimer,
                        animator,
                        transform,
                        rigidbodyComponent,
                        TargetObject,
                        currentPoint,
                        FireBallSpeed,
                        new TimerCallback(PushFireBall),
                        new ChangeStateCallback(ChangeState),
                        Power);

                        isAttackable[1] = false;
                        Pattern1Timer = GameTimeManager.Instance.PopTimer();
                        Pattern1Timer.SetTimer(attack1Tick, false);
                        Pattern1Timer.Callback = new TimerCallback(SetAttackable2);
                        Pattern1Timer.StartTimer();
                    }
                    break;
                case EnemyStateType.Move:
                    currentState = new Move(
                        animator,
                        transform,
                        rigidbodyComponent,
                        destinationPoint,
                        currentPoint,
                        MoveSpeed,
                        currentTile);
                    break;
                default:
                    break;
            }
            base.ChangeState(stateType);
        }

        private static void ExecuteDelegate(ChangeStateCallback changeStateCallback)
        {
            changeStateCallback(EnemyStateType.Idle);
        }

        private void StartAttack1()
        {
            attackArea.AttackCollider.enabled = true;
        }

        private void EndAttack()
        {
            ChangeState(EnemyStateType.Idle);
            attackArea.AttackCollider.enabled = false;
        }

        private void EndDamage()
        {
            ChangeState(EnemyStateType.Idle);
        }

        /// <summary>
        /// 일반 몹 전용 스텟 설정 메서드
        /// </summary>
        /// <param name="ItemNumber"></param>
        public override void SetStatus(int BattleLevel)
        {
            MaxHp = 10 + BattleLevel * 3;
            Power = 2 + BattleLevel * 0.5f;
            CurrentHp = MaxHp;
        }
        public override void SetStatus(int HP, int Power, bool IsLastBoss) { }
    }
}