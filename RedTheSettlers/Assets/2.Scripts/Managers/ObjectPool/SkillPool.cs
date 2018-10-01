using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Skills;

namespace RedTheSettlers.GameSystem
{
    public class SkillPool : MonoBehaviour
    {
        private const int ParticlePoolsize = 4;

        [SerializeField]
        private GameObject MeleeAttackParticleObject;
        [SerializeField]
        private GameObject SpeedUpBuffParticleObject;
        [SerializeField]
        private GameObject OverWhelmBuffParticleObject;

        private Queue<GameObject> MeleeAttackQueue;
        private Queue<GameObject> SpeedUpBuffQueue;
        private Queue<GameObject> OverWhelmBuffQueue;

        private void Awake()
        {
            MeleeAttackQueue = new Queue<GameObject>();
            SpeedUpBuffQueue = new Queue<GameObject>();
            OverWhelmBuffQueue = new Queue<GameObject>();

            for (int i = 0; i < ParticlePoolsize; i++)
            {
                MeleeAttackQueue.Enqueue(Instantiate(MeleeAttackParticleObject));
            }
            for (int i = 0; i < ParticlePoolsize; i++)
            {
                SpeedUpBuffQueue.Enqueue(Instantiate(SpeedUpBuffParticleObject));
            }
            for (int i = 0; i < ParticlePoolsize; i++)
            {
                OverWhelmBuffQueue.Enqueue(Instantiate(OverWhelmBuffParticleObject));
            }
        }

        public GameObject PopSkillParticle(SkillType skillType)
        {
            switch(skillType)
            {
                case SkillType.Melee:
                    return MeleeAttackQueue.Dequeue();
                case SkillType.SpeedUpBuff:
                    return SpeedUpBuffQueue.Dequeue();
                case SkillType.OverWhelmBuff:
                    return OverWhelmBuffQueue.Dequeue();
                default:
                    throw new MissingReferenceException();
            }
        }

        public void PushSkillParticle(GameObject skillParticle)
        {
            switch(skillParticle.GetComponent<SkillParticle>().skillType)
            {
                case SkillType.Melee:
                    MeleeAttackQueue.Enqueue(skillParticle);
                    break;
                case SkillType.SpeedUpBuff:
                    SpeedUpBuffQueue.Enqueue(skillParticle);
                    break;
                case SkillType.OverWhelmBuff:
                    OverWhelmBuffQueue.Enqueue(skillParticle);
                    break;
                default:
                    throw new MissingReferenceException();
            }
        }
    }
}
