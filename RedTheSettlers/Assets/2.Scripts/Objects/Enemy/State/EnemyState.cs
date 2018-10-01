using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using RedTheSettlers.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public abstract class EnemyState
    {
        protected EnemyState currentState;
        protected Material[] materials;
        protected EnemyFireBall fireBall;
        protected BattleAI battleAI;

        protected Transform transform;
        protected Animator animator;
        protected SkinnedMeshRenderer typeRenderer;
        protected EnemyAttackArea attackArea;
        protected EnemyHitArea hitArea;
        protected Collider AttackColliderComponent;
        protected Collider HitColliderComponent;
        protected Rigidbody rigidbodyComponent;
        protected BattlePlayer targetObject;
        protected int bossPhase;

        protected Vector3 destinationPoint;
        protected Vector3 currentPoint;
        protected Tile currentTile;

        protected float moveSpeed;
        protected int currentHp;
        protected int maxHp;
        protected float Power;
        protected float timeToReturn;
        protected float fireballSpeed;

        protected Explode explodePrefab;
        public Explode explode;
        protected GameTimer explodeLifeTimer;
        protected float explodeLifeTime;

        protected GameTimer deadTimer;
        protected GameTimer pattern1Timer;
        protected GameTimer pattern2Timer;
        protected GameTimer fireballLifeTimer;

        protected TimerCallback pushFireball;
        protected ChangeStateCallback changeStateCallback;
        protected DeadTimerCallback deadTimerCallback;

        public abstract void DoAction();
    }
}