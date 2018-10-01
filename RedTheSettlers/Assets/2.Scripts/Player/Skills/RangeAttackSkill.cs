using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Skills
{
    public class RangeAttackSkill : Skill
    {
        public override IEnumerator ActivateSkill(BattlePlayer battlePlayer)
        {
            if (ObjectPoolManager.Instance.SkillQueue.Count > 0)
            {
                Vector3 positionToCreate = battlePlayer.transform.rotation * Vector3.forward * 2 + battlePlayer.transform.position;

                GameObject projectile = ObjectPoolManager.Instance.SkillQueue.Dequeue();
                projectile.SetActive(true);

                projectile.transform.position = positionToCreate;
                projectile.transform.rotation = battlePlayer.transform.rotation;
            }

            yield return null;
        }
    }
}
