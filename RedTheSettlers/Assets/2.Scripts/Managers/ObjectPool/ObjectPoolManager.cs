using RedTheSettlers.Enemys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        public const int FireballSize = 20;
        public const int ExplodeSize = 5;

        [Header("Pools")]
        public TilePool TileObjectPool;
        public EnemyPool EnemyObjectPool;
        public PlayerPool PlayerObjectPool;
        public SkillPool SkillObjectPool;

        [Header("Player Skill")]
        public GameObject SkillObject;

        [Header("Enemy Skills")]
        public EnemyFireBall FireballPrefab;
        public Explode ExplodePrefab;

        [Header("Cow Battle Event")]
        public GameObject CowPrefab;

        public List<Queue<GameObject>> EnemyQueueList;
        public Queue<GameObject> SkillQueue;
        public Queue<EnemyFireBall> FireballQueue;
        public Queue<Explode> ExplodeQueue;
        [HideInInspector]
        public GameObject CowObject;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            //enemy only -----------
            FireballQueue = new Queue<EnemyFireBall>(FireballSize);
            for (int i = 0; i < FireballSize; i++)
            {
                FireballQueue.Enqueue(Instantiate(FireballPrefab, gameObject.transform));
            }

            ExplodeQueue = new Queue<Explode>(ExplodeSize);
            for (int i = 0; i < FireballSize; i++)
            {
                ExplodeQueue.Enqueue(Instantiate(ExplodePrefab, gameObject.transform));
            }
            //-----------------------
    

            SkillQueue = new Queue<GameObject>(6);
            for (int i = 0; i < 6; i++)
            {
                SkillQueue.Enqueue(Instantiate(SkillObject));
            }

            CowObject = Instantiate(CowPrefab);
        }
    }
}