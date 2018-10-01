using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 강다희, 김하정
    /// UI Manager
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private GameObject playerTurnUI;

        [SerializeField]
        private GameObject aiTurnUI;

        [SerializeField]
        private GameObject CommonPlayerItemUI;

        [SerializeField]
        private GameObject tradeUI, equipmentAndSkillUI, stateUI;

        [SerializeField]
        private GameObject battleUI;

        [SerializeField]
        private GameObject bossWarningUI;

        [SerializeField]
        private GameObject weatherEventSelectUI;

        [SerializeField]
        private GameObject selectTileUI;

        public void ShowBoardUI()

        {
            playerTurnUI.SetActive(true);
            CommonPlayerItemUI.SetActive(true);
        }

        public void ShowAiTurnUI()
        {
            aiTurnUI.SetActive(true);
        }

        public void CoverAiTurnUI()
        {
            aiTurnUI.SetActive(false);
        }

        public void OnClickedEquipAndSkillButton()
        {
            equipmentAndSkillUI.SetActive(true);
            playerTurnUI.SetActive(false);
        }

        public void OnClickedStateButton()
        {
            stateUI.SetActive(true);
            playerTurnUI.SetActive(false);
        }

        public void OnClickedTradeButton()
        {
            playerTurnUI.SetActive(false);
            tradeUI.SetActive(true);
        }

        public void SendTradeData(ItemData[] itemDatas, int requestPlayer, int receivePlayer)
        {
            GameManager.Instance.SendTradeData(itemDatas, requestPlayer, receivePlayer);
        }

        public void RecieveTradeResult(OtherPlayerState state)
        {
            tradeUI.GetComponentInChildren<UITradeCard>().RecieveTradeData(state);
        }

        public void OnClickedTurnCloseButton()
        {
            playerTurnUI.SetActive(false);
            equipmentAndSkillUI.SetActive(false);
            stateUI.SetActive(false);
            tradeUI.SetActive(false);
        }

        public void ShowBattleUI()
        {
            battleUI.SetActive(true);
        }

        public void ShowBossWarningUI()
        {
            bossWarningUI.SetActive(true);
        }

        public void ShowWheatherEvent(int[] weathers)
        {
            weatherEventSelectUI.SetActive(true);
            weatherEventSelectUI.GetComponentInChildren<UIWeatherSelect>().ReceiveEventNumbers(weathers);
        }

        public void RequestStorageEventNumber(int eventNumber)
        {
            GameManager.Instance.SetWeatherEventNumber(eventNumber);
        }

        public void SendTileInfo(BoardTile selectionTile)
        {
            selectTileUI.SetActive(true);
            selectTileUI.GetComponentInChildren<UISelectTile>().SetSelectTileInfo(selectionTile);
        }

        public void SendBattleTileInfo(BoardTile tileInfo)
        {
            GameManager.Instance.BulidBattleTile(tileInfo);
        }

        public void RequestAddItemType(int playerNumber, ItemType itemType, int addItem)
        {
            GameManager.Instance.AddItemByType(playerNumber, itemType, addItem);
        }

        public void RequestSavePlayerStat(int hp, int mp, int stamina)
        {
            GameManager.Instance.SetPlayerStat(hp, mp, stamina);
        }

        public void SendAiTurnContentQueue(Queue<string> messageQueue)
        {
            UIAITurn uiAiTurn = aiTurnUI.GetComponentInChildren<UIAITurn>();
            while (messageQueue.Count == 0)
            {
                uiAiTurn.ContentStringQueue.Enqueue(messageQueue.Dequeue());
            }
        }

        public void SendNonClickedTile()
        {
            GameManager.Instance.NonClickedTile();
        }

        /// <summary>
        /// 플레이어 공격 레벨 전달
        /// </summary>
        public void SendPlayerAttackLevel(int attackLevel)
        {
            GameManager.Instance.SetPlayersAttackLevel(0, attackLevel);
        }

        /// <summary>
        /// 플레이어 방어 레벨 전달
        /// </summary>
        public void SendPlayerDefenseLevel(int defenseLevel)
        {
            GameManager.Instance.SetPlayersDefenseLevel(0, defenseLevel);
        }

        /// <summary>
        /// 교환 후 : 플레이어 자원 게산결과 전달
        /// </summary>
        public void SendPlayerItems(int woodCount, int ironCount, int soilCount)
        {
            GameManager.Instance.AddItemByType(0, ItemType.Wood, woodCount);
            GameManager.Instance.AddItemByType(0, ItemType.Iron, ironCount);
            GameManager.Instance.AddItemByType(0, ItemType.Soil, soilCount);
        }
    }
}