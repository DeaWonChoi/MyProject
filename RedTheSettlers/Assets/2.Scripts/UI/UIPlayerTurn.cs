using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.UI
{
    public class UIPlayerTurn : MonoBehaviour
    {
        /// <summary>
        /// 작성자 : 강다희
        /// 플레이어의 현재 turn수를 노출해주는 UI스크립트
        ///
        /// [중요] Turn, move, event를 Script 분리 할 것임
        /// [중요] UI Manager 스크립트에서 각 상황에 맞게 처리 할 예정
        /// </summary>

        [Header("Player's Current Turn")]
        [SerializeField]
        private Text currentTurnText;

        private int currentTurn;

        private void OnEnable()
        {
            changeTurn();
        }

        private void changeTurn()
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];

            currentTurn = playerData.StatData.StaminaPoint;
            currentTurnText.text = currentTurn.ToString();

            LogManager.Instance.UserDebug(LogColor.Olive, GetType().Name, "현재 Turn : " + currentTurn);
        }
    }
}