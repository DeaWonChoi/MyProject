using UnityEngine;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using RedTheSettlers.Players;

namespace RedTheSettlers.Enemys
{
    public delegate EnemyFireBall FireballCallback(EnemyFireBall enemyFireBall);
    public delegate void ChangeStateCallback(EnemyStateType stateType);
    public delegate void DeadTimerCallback();

    public enum EnemyStateType
    {
        Idle = 0,
        Die = 1,
        Damage = 2,
        Attack1 = 3,
        Attack2 = 4,
        Move = 5,
    }

    /// <summary>
    /// 몬스터 클래스
    /// 담당자 : 최대원
    /// </summary>
    public abstract class Enemy : MonoBehaviour
    {
        public EnemyState currentState;
        [SerializeField]
        protected Material[] Materials;
        [SerializeField]
        protected Material[] bossMaterials;
        public GameObject FireBall;
        protected BattleAI battleAI;
        EnemyFireBall enemyFireBall;

        [Header("Compoenets")]
        public Animator animator;
        protected SkinnedMeshRenderer typeRenderer;
        protected EnemyAttackArea attackArea;
        protected EnemyHitArea hitArea;
        protected Collider AttackColliderComponent;
        protected Collider HitColliderComponent;
        public Rigidbody rigidbodyComponent;
        public BattlePlayer TargetObject;
        protected EnemyFindCollider TargetFindCollider;

        [Header("Moving Points")]
        public Vector3 destinationPoint;
        public Vector3 currentPoint;
        public Tile currentTile;

        [Header("Status")]
        public float MoveSpeed;
        public int CurrentHp;
        public int MaxHp;
        public float TimeToReturn = 3.0f;
        public float Power;
        public bool IsLastBoss;
        [ReadOnly]
        public float FireBallSpeed = 4.0f;

        [Header("Timers")]
        protected GameTimer DeadTimer;
        protected GameTimer Pattern1Timer;
        protected GameTimer Pattern2Timer;
        protected GameTimer FireBallLifeTimer;
        public bool[] isAttackable;

        public GameTimer AITimer;
        public EnemyDeadCallback enemyDeadCallback;

        private void Update()
        {
            UpdatePosition();
            battleAI.AIUpdate();
        }

        protected virtual void Setting()
        {
            typeRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            animator = GetComponent<Animator>();
            attackArea = GetComponentInChildren<EnemyAttackArea>();
            attackArea.Power = (int)Power;
            hitArea = GetComponentInChildren<EnemyHitArea>();
            hitArea.enemy = this;
            rigidbodyComponent = GetComponent<Rigidbody>();
            TargetFindCollider = transform.GetComponentInChildren<EnemyFindCollider>();
            TargetFindCollider.enemy = this;
            isAttackable = new bool[2] { true, true };
            battleAI = new BattleAI(this);
            AITimer = battleAI.pathFindTimer;
            ChangeState(EnemyStateType.Idle);
        }

        protected void UpdatePosition()
        {
            currentPoint = transform.position;
            StopMovement();
        }

        public abstract void SetStatus(int BattleLevel);
        public abstract void SetStatus(int HP, int Power, bool IsLastBoss);

        public virtual void ChangeState(EnemyStateType stateType)
        {
            ReQuest();
        }

        public void ChangeState(int stateType)
        {
            ChangeState((EnemyStateType)stateType);
        }

        protected void ReQuest()
        {
            currentState.DoAction();
        }

        protected void CheckHp()
        {
            if (CurrentHp <= 0)
            {
                ChangeState(EnemyStateType.Die);
            }

            if (CurrentHp > MaxHp)
            {
                CurrentHp = MaxHp;
            }
        }

        public void SetType(ItemType ItemType)
        {
            typeRenderer.material = Materials[(int)ItemType];
        }

        public void SetType(bool IsLastBoss)
        {
            if (IsLastBoss)
            {
                typeRenderer.material = bossMaterials[0];
            }
            else
            {
                typeRenderer.material = bossMaterials[1];
            }
        }

        protected void SetAttackable1()
        {
            isAttackable[0] = true;
        }

        protected void SetAttackable2()
        {
            isAttackable[1] = true;
        }

        public EnemyFireBall PopFireBall()
        {
            enemyFireBall = ObjectPoolManager.Instance.FireballQueue.Dequeue();
            enemyFireBall.gameObject.SetActive(true);
            return enemyFireBall;
        }

        public void PushFireBall()
        {
            FireBallLifeTimer = null;
            enemyFireBall.gameObject.SetActive(false);
            ObjectPoolManager.Instance.FireballQueue.Enqueue(enemyFireBall);
        }

        protected void StopMovement()
        {
            if (destinationPoint != null && currentState != null)
            {
                if (Vector3.Distance(destinationPoint, currentPoint) <= 0.5f && currentState is Move)
                {
                    rigidbodyComponent.velocity = Vector3.zero;
                    ChangeState(EnemyStateType.Idle);
                }
            }
        }

        //피격 처리를 담당하는 메서드
        public void Damaged(int damage)
        {
            rigidbodyComponent.velocity = Vector3.zero;
            CurrentHp -= damage;
            CheckHp();
        }

        public void EndDead()
        {
            DeadTimer = null;
            enemyDeadCallback();
            ObjectPoolManager.Instance.EnemyObjectPool.PushEnemyObject(gameObject);
            gameObject.SetActive(false);
        }

        public BattleTile GetCurrentTile(Vector3 position)
        {
            RaycastHit[] hitInfo = Physics.RaycastAll(position, Vector3.down);
            foreach (RaycastHit item in hitInfo)
            {
                if(item.collider.GetComponent<BattleTile>() != null)
                {
                    return item.collider.GetComponent<BattleTile>();
                }
            }

            return null;
        }
    }
}