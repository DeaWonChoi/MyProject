using RedTheSettlers.GameSystem;
using RedTheSettlers.Skills;
using System.Collections;
using UnityEngine;

namespace RedTheSettlers.Players
{
    public enum PlayerStateType
    {
        Attack,
        Move,
        CastSkill,
        Dead
    }

    public class BattlePlayer : MonoBehaviour
    {
        private GameTimer playerTimer;
        [SerializeField]
        private GameObject AttackBox;

        [HideInInspector]
        public Animator animator;

        public delegate void PlayerDeadDelegate();
        public PlayerDeadDelegate playerDeadCallBack; 

        public bool IsOverWhelm;
        private int hp;
        private int mp;
        private float standardSpeed = 2.0f;
        [SerializeField]
        private float moveSpeed = 2.0f;

        private Skill[] skillSlot = new Skill[3];

        private void Awake()
        {
            animator = GetComponent<Animator>();
            skillSlot[0] = new MeleeAttackSkill();
            skillSlot[1] = new SpeedUpBuffSkill();
            skillSlot[2] = new OverWhelmBuffSkill();
        }

        public IEnumerator MoveToTargetPostion(Vector3 targetPosition)
        {
            animator.SetBool("IsRunning", true);

            Quaternion targetAngle = Quaternion.LookRotation(targetPosition - transform.position);

            while (Quaternion.Angle(transform.rotation, targetAngle) > 0.1f ||
                    Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, 0.5f);
                transform.position += Vector3.Normalize(targetPosition - transform.position) * moveSpeed * Time.deltaTime;

                yield return null;
            }

            animator.SetBool("IsRunning", false);
        }

        public IEnumerator MoveToDirection(Vector3 Direction)
        {
            animator.SetBool("IsRunning", true);

            Quaternion targetAngle = Quaternion.LookRotation(Direction);

            int frameCount = 0;

            while (frameCount < 30)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, 0.5f);

                transform.position += Direction * moveSpeed * Time.deltaTime;

                frameCount++;

                yield return null;
            }

            animator.SetBool("IsRunning", false);
        }

        public void AttackEnemy(int damage)
        {
            StartCoroutine(AttackEnemyCoroutine(damage));
        }

        public IEnumerator AttackEnemyCoroutine(int damage)
        {
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(0.5f);

            AttackBox.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            
            AttackBox.SetActive(false);
        }

        public void HittedByEnemy(int damage)
        {
            if(!IsOverWhelm)
            StartCoroutine(HittedByEnemyCoroutine(damage));
        }

        public IEnumerator HittedByEnemyCoroutine(int damage)
        {
            hp -= damage;

            animator.SetBool("IsDamaged", true);
            
            yield return new WaitForSeconds(0.5f);

            animator.SetBool("IsDamaged", false);

            yield return null;

            //if(hp <= 0)
            //{
            //    playerDeadCallBack();

            //    animator.SetTrigger("Dead");

            //    yield return new WaitForSeconds(2f);

            //    gameObject.SetActive(false);

            //    ObjectPoolManager.Instance.PlayerObjectPool.SetPlayerObject(gameObject);
            //}
        }

        public void UseSkill(int skillSlotNum)
        {
            StartCoroutine(skillSlot[skillSlotNum].ActivateSkill(this));
        }

        public void ChangeSpeed(float amount)
        {
            moveSpeed += amount;
        }

        public void SlowDownSpeedByWater()
        {
            moveSpeed = 1.0f;
        }

        public void RecoverySpeed()
        {
            moveSpeed = 2.0f;
        }
    }
}

