using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 강다희
/// Tile 선택 시, 미개척/개척(플레이어)/개척(AI)인지 체크하고,
/// 해당 값을 출력해 주며 캠프짓기/캠프뺏기/업그레이드가 가능함.
///
/// [참고] 타일을 인풋으로 선택 했을 때 UI가 setactive되는 것은 UIManager에서 해야함.
/// </summary>

namespace RedTheSettlers.UI
{
    public class UISelectTile : MonoBehaviour
    {
        [Header("Current Tile Information")]
        [SerializeField]
        private Text tileName;

        [SerializeField]
        private Text tileItemName;

        [SerializeField]
        private Text tileItemLevel;

        [SerializeField]
        private Text tileItemExplain;

        [SerializeField]
        private Text itemAcquireInfomation;

        [SerializeField]
        private Text UpgradeItemCost;

        [Serializable]
        private struct TileOwnerImage
        {
            public GameObject tilePlayerImage;
            public GameObject tileAIFirstImage;
            public GameObject tileAISecondImage;
            public GameObject tileAIThirdImage;
            public GameObject tileUnexploredImage;
        }

        [SerializeField]
        private TileOwnerImage tileOwnerImage;

        [Serializable]
        private struct TileItemImage
        {
            public GameObject cowTileImage;
            public GameObject waterTileImage;
            public GameObject wheatTileImage;
            public GameObject woodTileImage;
            public GameObject ironTileImage;
            public GameObject soilTileImage;
        }

        [SerializeField]
        private TileItemImage tileItemImage;

        [Serializable]
        private struct UpgradeItemImage
        {
            public string ImageName;
            public GameObject image;
        }

        [SerializeField]
        private UpgradeItemImage[] upgradeItemImages;

        [Header("Current Tile Event Button")]
        [SerializeField]
        private GameObject[] tileActionButton;

        private BoardTile boardTile;

        private int selectTileItemLevel
        {
            get
            {
                if (selectTileItemLevel == 0)
                    return 1;
                else
                    return selectTileItemLevel;
            }
            set
            { selectTileItemLevel = value; }
        }

        private int playerUpgradeItemCost;
        private int playerHoldItemCount;

        private void OnEnable()
        {
            OnTileInformation();
        }

        private void CheckTilePossession()
        {
            int selectTileProperties = (int)boardTile.TileType;
            int selectTileOwner = (int)boardTile.tileOwner;

            if (selectTileOwner == 0) //플레이어 타일
            {
                tileOwnerImage.tilePlayerImage.SetActive(true);
                tileActionButton[0].SetActive(true);
                for (int i = 0; i < upgradeItemImages.Length; i++)
                {
                    if (selectTileProperties < i)
                        break;
                    else if (selectTileProperties == i)
                    {
                        upgradeItemImages[i].image.SetActive(true);
                        playerUpgradeItemCost = selectTileItemLevel * 3;
                        UpgradeItemCost.text = "- " + playerUpgradeItemCost.ToString();
                        break;
                    }
                }
            }
            else if (selectTileOwner == 1) //AI1 타일
            {
                tileOwnerImage.tileAIFirstImage.SetActive(true);
                tileActionButton[1].SetActive(true);
            }
            else if (selectTileOwner == 2) //AI2 타일
            {
                tileOwnerImage.tileAISecondImage.SetActive(true);
                tileActionButton[1].SetActive(true);
            }
            else if (selectTileOwner == 3) //AI3 타일
            {
                tileOwnerImage.tileAIThirdImage.SetActive(true);
                tileActionButton[1].SetActive(true);
            }
            else //미개척 타일
            {
                tileOwnerImage.tileUnexploredImage.SetActive(true);
                tileActionButton[2].SetActive(true);
            }
        }

        private void CheckTileProperties()
        {
            switch ((int)boardTile.TileType) //소,물,밀,나무,철,흙
            {
                case 0:
                    {
                        tileName.text = "목축지";
                        tileItemName.text = "소";
                        tileItemImage.cowTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '소' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'소'는 배고픔(HP)를 채워줍니다.";
                    }
                    break;

                case 1:
                    {
                        tileName.text = "오아시스";
                        tileItemName.text = "물";
                        tileItemImage.waterTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '물' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'물'은 갈증(MP)를 채워줍니다.";
                    }
                    break;

                case 2:
                    {
                        tileName.text = "밀밭";
                        tileItemName.text = "밀";
                        tileItemImage.wheatTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '밀' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'밀'은 스테미나(Move)를 채워줍니다.";
                    }
                    break;

                case 3:
                    {
                        tileName.text = "숲";
                        tileItemName.text = "나무";
                        tileItemImage.woodTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '물' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'나무'는 업그레이드에 필요합니다.";
                    }
                    break;

                case 4:
                    {
                        tileName.text = "돌산";
                        tileItemName.text = "철";
                        tileItemImage.ironTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '철' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'철'은 업그레이드에 필요합니다.";
                    }
                    break;

                case 5:
                    {
                        tileName.text = "흙산";
                        tileItemName.text = "흙";
                        tileItemImage.soilTileImage.SetActive(true);
                        tileItemExplain.text = "1턴마다 '흙' 자원을" + selectTileItemLevel + "개 획득합니다.";
                        itemAcquireInfomation.text = "'흙'은 업그레이드에 필요합니다.";
                    }
                    break;
            }
        }

        public void SetSelectTileInfo(BoardTile selectionTile)
        {
            Debug.Log("line 216");
            boardTile = selectionTile;
            selectTileItemLevel = selectionTile.TileLevel;
        }

        private void OnTileInformation()
        {
            CheckTilePossession();
            CheckTileProperties();

            tileItemLevel.text = "Lv." + selectTileItemLevel.ToString();
        }

        public void OnClickedBattleStartButton()
        {
            UIManager.Instance.SendBattleTileInfo(boardTile);
        }

        public void OnUpgradeButton()
        {
            SoundManager.Instance.PlaySFX("Button_Heavy", false);

            playerHoldItemCount = GameManager.Instance.gameData.PlayerData[0].ItemList[(int)boardTile.TileType].Count;
            if (playerHoldItemCount == 0)
            {
                UpgradeItemCost.color = new Color32(255, 0, 0, 255);
            }
            else
            {
                playerHoldItemCount = playerHoldItemCount - 1;
                selectTileItemLevel = selectTileItemLevel + 1;
                playerUpgradeItemCost = selectTileItemLevel * 3;
                UpgradeItemCost.text = "- " + playerUpgradeItemCost.ToString();
            }
        }

        public void OnCloseButton()
        {
            UIManager.Instance.SendNonClickedTile();
            //버튼 꺼졌다고 게임 매니저쪽에 전달해 줘야댐!! 왜냐면 카메라 뷰가 바뀌기 때무네!! 준명님 도와조
        }
    }
}