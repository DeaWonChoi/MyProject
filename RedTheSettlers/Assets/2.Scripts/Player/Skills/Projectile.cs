using UnityEngine;
using RedTheSettlers.Enemys;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.Skills
{
    public class Projectile : MonoBehaviour
    {
        private const int projectileDamage = 5;

        private void OnCollisionEnter(Collision collision)
        {
            GameObject target = collision.gameObject;

            if(target.CompareTag("Enemy"))
            {
                target.GetComponent<Enemy>().Damaged(projectileDamage);
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            ObjectPoolManager.Instance.SkillQueue.Enqueue(gameObject);
        }
    }
}
