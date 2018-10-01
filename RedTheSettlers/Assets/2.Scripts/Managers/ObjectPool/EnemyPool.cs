using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyObject;
        [SerializeField]
        private GameObject bossObject;

        private Queue<GameObject> enemyQueue;
        private Queue<GameObject> bossQueue;

        private void Awake()
        {
            enemyQueue = new Queue<GameObject>(6);
            bossQueue = new Queue<GameObject>(2);

            for(int i = 0; i < enemyQueue.Count; i++)
            {
                PushEnemyObject(enemyObject);
            }
            for (int i = 0; i < enemyQueue.Count; i++)
            {
                PushBossObject(bossObject);
            }
        }

        public GameObject PopEnemyObject()
        {
            return enemyQueue.Dequeue();
        }

        public void PushEnemyObject(GameObject enemy)
        {
            enemyQueue.Enqueue(enemy);
        }

        public GameObject PopBossObject()
        {
            return bossQueue.Dequeue();
        }

        public void PushBossObject(GameObject boss)
        {
            bossQueue.Enqueue(boss);
        }
    }
}
