using UnityEngine;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.Enemys
{
    public class EnemyHitArea : MonoBehaviour
    {
        public Enemy enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == GlobalVariables.TAG_PLAYERATTACK)
            {
                enemy.ChangeState(EnemyStateType.Damage);
            }
            else if (other.tag == GlobalVariables.TAG_WALL)
            {
                enemy.rigidbodyComponent.velocity = Vector3.zero;
                enemy.ChangeState(EnemyStateType.Idle);
            }
        }
    }
}