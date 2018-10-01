using UnityEngine;
using RedTheSettlers.Players;
using RedTheSettlers.Enemys;

namespace RedTheSettlers.GameSystem
{
    public class CattleHit : MonoBehaviour
    {
        [SerializeField]
        private int hitDamage = 20;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == GlobalVariables.TAG_ENEMY)
            {
                Debug.Log("충돌 에너미");
                collision.gameObject.GetComponent<Enemy>().Damaged(hitDamage);
            }
            else if (collision.gameObject.tag == GlobalVariables.TAG_PLAYER)
            {
                collision.gameObject.GetComponent<BattlePlayer>().HittedByEnemy(hitDamage);
                Debug.Log("충돌 플레이어");
            }
        }
    }
}