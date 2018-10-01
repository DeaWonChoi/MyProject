using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class EnemyFireBall : MonoBehaviour
    {
        public Rigidbody rigidbodyComponent;
        public EnemyAttackArea AttackArea;

        private void Start()
        {
            AttackArea = GetComponent<EnemyAttackArea>();
            rigidbodyComponent = GetComponent<Rigidbody>();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            rigidbodyComponent.velocity = Vector3.zero;
        }
    }
}