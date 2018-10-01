using RedTheSettlers.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 강다희
/// 보유한 아이템으로 state 수치 조정
/// </summary>

namespace RedTheSettlers.UI
{
    public class UIPlayerBattleState : MonoBehaviour
    {
        [Header("Player's Total Item")]
        [SerializeField]
        private Text playerHP;

        [SerializeField]
        private Text playerMP;

        [SerializeField]
        private Text playerMaxHP;

        [SerializeField]
        private Text playerMaxMP;

        [SerializeField]
        private Text playerMovePoint;

        [SerializeField]
        private Slider PlayerHPBar;

        [SerializeField]
        private Slider PlayerMPBar;

        private int playerCowItem;
        private int playerWaterItem;
        private int playerWheatItem;

        private int currentPlayerHP;
        private int currentPlayerMP;
        private int currentPlayerMovePoint;

        private int fillHP;
        private int fillMp;
        private int fillMovePoint;

        private int maxHp;
        private int maxMp;

        private void CurrentState()
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];

            playerHP.text = playerData.StatData.HealthPoint.ToString();
            playerMP.text = playerData.StatData.MagicPoint.ToString();
            playerMovePoint.text = playerData.StatData.StaminaPoint.ToString();

            playerMaxHP.text = playerData.StatData.MaxHealthPoint.ToString();
            playerMaxMP.text = playerData.StatData.MaxMagicPoint.ToString();
        }

        private void FilledBar()
        {
            PlayerHPBar.value = currentPlayerHP / maxHp;
            PlayerMPBar.value = currentPlayerMP / maxMp;
        }

        public void OnClickState(int Count)
        {
            if (Count == 0)
            {
            }
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}