using System.Collections;
using System.Collections.Generic;
using RedTheSettlers.Players;
using RedTheSettlers.GameSystem;
using UnityEngine;

namespace RedTheSettlers.Skills
{
    public class MeleeAttackSkill : Skill
    {
        public override IEnumerator ActivateSkill(BattlePlayer battlePlayer)
        {
            battlePlayer.AttackEnemy(20);

            yield return new WaitForSeconds(0.5f);

            GameObject meleeAttackParticle = ObjectPoolManager.Instance.SkillObjectPool.PopSkillParticle(SkillType.Melee);
            meleeAttackParticle.transform.position = battlePlayer.transform.position + battlePlayer.transform.rotation * Vector3.forward;
            meleeAttackParticle.transform.rotation = battlePlayer.transform.rotation;
            meleeAttackParticle.SetActive(true);

            while (!meleeAttackParticle.GetComponent<ParticleSystem>().isStopped)
            {
                yield return null;
            }

            meleeAttackParticle.SetActive(false);
            ObjectPoolManager.Instance.SkillObjectPool.PushSkillParticle(meleeAttackParticle);
        }
    }
}
