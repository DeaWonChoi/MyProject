using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class EnemyAttackArea : MonoBehaviour
    {
        public Collider AttackCollider;
        public int Power;

        private void Start()
        {
            AttackCollider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GlobalVariables.TAG_PLAYER)
            {
                other.GetComponent<BattlePlayer>().HittedByEnemy(Power);
            }
        }
    }
}