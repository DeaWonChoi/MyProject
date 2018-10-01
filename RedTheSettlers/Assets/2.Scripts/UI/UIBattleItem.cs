using RedTheSettlers.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 강다희
/// 전투 중 아이템 사용 관련 UI
/// [수정필요] 몬스터에 맞았을 때, HP가 줄어드는 계산 필요 (*승환님)
/// </summary>

namespace RedTheSettlers.UI
{
    public class UIBattleItem : MonoBehaviour
    {
        [Header("Player's Current Item")]
        [SerializeField]
        private Text HPItem;

        [SerializeField]
        private Text MPItem;

        [SerializeField]
        private Slider HPItemBar;

        [SerializeField]
        private Slider MPItemBar;

        private int holdHPItem;
        private int holdMPItem;

        private int playerHP;
        private int playerMP;
        private float playerMaxHP;
        private float playerMaxMP;

        private void Start()
        {
            PutPlayerState();
            SliderChanged();
        }

        public void PutPlayerState()
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];

            holdHPItem = playerData.ItemList[(int)ItemType.Cow].Count;
            holdMPItem = playerData.ItemList[(int)ItemType.Water].Count;

            HPItem.text = holdHPItem.ToString();
            MPItem.text = holdMPItem.ToString();

            playerHP = playerData.StatData.HealthPoint;
            playerMaxHP = playerData.StatData.MagicPoint;

            playerMP = playerData.StatData.MaxHealthPoint;
            playerMaxMP = playerData.StatData.MaxMagicPoint;
        }

        public void UpdatePlayerItem(int itemType)
        {
            PlayerData playerData = GameManager.Instance.gameData.PlayerData[0];
            switch (itemType)
            {
                case 0:
                    holdHPItem = playerData.ItemList[(int)ItemType.Cow].Count;
                    break;

                case 1:
                    holdMPItem = playerData.ItemList[(int)ItemType.Water].Count;
                    break;
            }
        }

        public void SliderChanged()
        {
            HPItemBar.value = playerHP / playerMaxHP;
            MPItemBar.value = playerMP / playerMaxMP;
        }

        public void OnUseItem(int itemType)
        {
            UpdatePlayerItem(itemType);

            Color textRedColor = new Color(255, 0, 0, 255);

            if (itemType == 0)
            {
                if (holdHPItem == 0)
                {
                    HPItem.color = textRedColor;
                }
                else if (playerHP < playerMaxHP)
                {
                    UIManager.Instance.RequestAddItemType(0, (ItemType)itemType, -1);
                    HPItem.text = holdHPItem.ToString();
                    playerHP = playerHP + 10;
                    SliderChanged();
                }
            }
            else if (itemType == 1)
            {
                if (holdMPItem == 0)
                {
                    MPItem.color = textRedColor;
                }
                else if (playerMP < playerMaxMP)
                {
                    UIManager.Instance.RequestAddItemType(0, (ItemType)itemType, -1);
                    MPItem.text = holdMPItem.ToString();
                    playerMP = playerMP + 10;
                    SliderChanged();
                }
            }
        }
    }
}