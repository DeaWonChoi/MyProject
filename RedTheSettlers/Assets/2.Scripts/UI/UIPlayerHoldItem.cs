using RedTheSettlers.GameSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 강다희
/// 플레이어의 자원 정보 노출 및
/// 플레이어 소지 가능 자원 정보 bar 형식으로 노출해주는 UI스크립트.
/// [중요] UI Manager 스크립트에서 각 상황별로 처리할 예정.
/// </summary>

namespace RedTheSettlers.UI
{
    public class UIPlayerHoldItem : MonoBehaviour
    {
        [Header("Player's Total Item")]
        [SerializeField]
        private Text playerTotalItem;

        [Header("Player's Item")]  //itemtype
        [SerializeField]
        private Text playerCowItem;

        [SerializeField]
        private Text playerWaterItem;

        [SerializeField]
        private Text playerWheatItem;

        [SerializeField]
        private Text playerWoodItem;

        [SerializeField]
        private Text playerIronItem;

        [SerializeField]
        private Text playerSoilItem;

        [Header("Total Bar")]
        [SerializeField]
        private Slider totalItemBar;

        private int cowCardCount;
        private int waterCardCount;
        private int wheatCardCount;
        private int woodCardCount;
        private int ironCardCount;
        private int soilCardCount;

        [SerializeField]
        private float cardmaxNumber;

        private int computeItemCount;

        private void OnEnable()
        {
            PutItemCount(); //UIManager에서 각 state, equip, selectTile에서 요 두개를 실행시켜줘야 함.
            ComputeTotalItem();
        }

        private void PutItemCount()
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];

            playerCowItem.text = playerData.ItemList[(int)ItemType.Cow].Count.ToString();
            playerWaterItem.text = playerData.ItemList[(int)ItemType.Water].Count.ToString();
            playerWheatItem.text = playerData.ItemList[(int)ItemType.Wheat].Count.ToString();
            playerWoodItem.text = playerData.ItemList[(int)ItemType.Wood].Count.ToString();
            playerIronItem.text = playerData.ItemList[(int)ItemType.Iron].Count.ToString();
            playerSoilItem.text = playerData.ItemList[(int)ItemType.Soil].Count.ToString();
        }

        private void ComputeTotalItem()
        {
            cardmaxNumber = GlobalVariables.MaxItemNum;

            computeItemCount = cowCardCount + waterCardCount + wheatCardCount + woodCardCount + ironCardCount + soilCardCount;
            playerTotalItem.text = computeItemCount.ToString();

            totalItemBar.value = computeItemCount / cardmaxNumber;
            LogManager.Instance.UserDebug(LogColor.Olive, GetType().Name, "현재 소지한 자원 합 : " + playerTotalItem.text);
        }
    }
}