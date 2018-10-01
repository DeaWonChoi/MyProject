using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.UI
{
    public class UIPlayerStamina : MonoBehaviour
    {
        /// <summary>
        /// 작성자 : 강다희
        /// 플레이어의 현재 Move를 노출해주는 UI스크립트
        /// [중요] UI Manager 스크립트에서 각 상황에 맞게 처리 할 예정
        /// </summary>

        [Header("Player's Current Move")]
        [SerializeField]
        private Text moveText;

        private int currentMove;

        private void ChangeMove()
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];
            currentMove = playerData.StatData.StaminaPoint;

            moveText.text = currentMove.ToString();

            LogManager.Instance.UserDebug(LogColor.Olive, GetType().Name, "현재 Move : " + currentMove);
        }

        private void Start()
        {
            ChangeMove();
        }

        private void Update()
        {
        }
    }
}