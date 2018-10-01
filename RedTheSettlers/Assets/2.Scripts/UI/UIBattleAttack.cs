using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using RedTheSettlers.GameSystem;

/// <summary>
/// 작성자 : 강다희
/// 공격, 스킬 공격 버튼 이벤트
/// </summary>

namespace RedTheSettlers.UI
{
    public class UIBattleAttack : MonoBehaviour
    {
        public enum AttackType
        {
            Attack,
            Skill01,
            Skill02,
            Skill03,
        }

        [Header("Normal Attack Button")]
        [SerializeField]
        private Button AttackButton;

        [Header("Skill Attack Button")]
        [SerializeField]
        private Button SkillSlot1Button;

        [SerializeField]
        private Button SkillSlot2Button;

        [SerializeField]
        private Button SkillSlot3Button;

        public void PutPlayerAttack()
        {
        }

        public void OnAttackButtonClick(AttackType attackType)
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];

            switch (attackType)
            {
                case AttackType.Attack:
                    {
                        Debug.Log("Attack Click!"); //데이터 매니저에 저장되어 있는 어택 가져옴
                    }
                    break;

                case AttackType.Skill01:
                    {
                    }
                    break;

                case AttackType.Skill02:
                    {
                    }
                    break;

                case AttackType.Skill03:
                    {
                    }
                    break;
            }
        }

        private void Start()
        {
        }

        private void Update()
        {
        }
    }
}