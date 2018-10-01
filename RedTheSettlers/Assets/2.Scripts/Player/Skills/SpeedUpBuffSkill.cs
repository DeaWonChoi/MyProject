using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Players;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.Skills
{
    public class SpeedUpBuffSkill : Skill
    {
        public override IEnumerator ActivateSkill(BattlePlayer battlePlayer)
        {
            float buffTime = 0;

            battlePlayer.ChangeSpeed(0.5f);
            GameObject speedUpBuffParticle = ObjectPoolManager.Instance.SkillObjectPool.PopSkillParticle(SkillType.SpeedUpBuff);
            speedUpBuffParticle.transform.position = battlePlayer.transform.position;
            speedUpBuffParticle.SetActive(true);

            while (!speedUpBuffParticle.GetComponent<ParticleSystem>().isStopped)
            {
                buffTime += Time.deltaTime;
                yield return null;
            }

            speedUpBuffParticle.SetActive(false);
            ObjectPoolManager.Instance.SkillObjectPool.PushSkillParticle(speedUpBuffParticle);

            yield return new WaitForSeconds(2f - buffTime);

            battlePlayer.ChangeSpeed(-0.5f);

            yield return null;
        }
    }
}
