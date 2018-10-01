using System.Collections;
using System.Collections.Generic;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using UnityEngine;

namespace RedTheSettlers.Skills
{
    public class OverWhelmBuffSkill : Skill
    {
        public override IEnumerator ActivateSkill(BattlePlayer battlePlayer)
        {
            float buffTime = 0;

            battlePlayer.IsOverWhelm = true;
            GameObject OverWhelmBuffParticle = ObjectPoolManager.Instance.SkillObjectPool.PopSkillParticle(SkillType.OverWhelmBuff);
            OverWhelmBuffParticle.transform.position = battlePlayer.transform.position;
            OverWhelmBuffParticle.SetActive(true);

            while (!OverWhelmBuffParticle.GetComponent<ParticleSystem>().isStopped)
            {
                buffTime += Time.deltaTime;
                yield return null;
            }

            OverWhelmBuffParticle.SetActive(false);
            ObjectPoolManager.Instance.SkillObjectPool.PushSkillParticle(OverWhelmBuffParticle);

            yield return new WaitForSeconds(2f - buffTime);

            battlePlayer.IsOverWhelm = false;

            yield return null;
        }
    }
}

