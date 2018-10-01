using UnityEngine;
using UnityEngine.UI;
using RedTheSettlers.GameSystem;
using System;

namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 김하정
    /// 장비UI 스크립트 : 장비 업그레이드에 대한 것을 보여준다.
    /// </summary>
    public class UIEquipment : MonoBehaviour
    {
        public const int AttackUpgradeOneWood = 3;
        public const int AttackUpgradeOneIron = 5;
        public const int AttackUpgradeOneSoil = 5;

        public const int AttackUpgradeTwoWood = 10;
        public const int AttackUpgradeTwoIron = 15;
        public const int AttackUpgradeTwoSoil = 15;

        public const int DefenseUpgradeOneWood = 5;
        public const int DefenseUpgradeOneIron = 3;
        public const int DefenseUpgradeOneSoil = 3;

        public const int DefenseUpgradeTwoWood = 15;
        public const int DefenseUpgradeTwoIron = 10;
        public const int DefenseUpgradeTwoSoil = 10;

        private int PlayerWood;
        private int PlayerIron;
        private int PlayerSoil;

        [Flags]
        enum UpgradeItems
        {
            Wood = 1,
            Iron = 2,
            Soil = 4,
        }

        struct UpgradeRequestItems
        {
            public int Wood, Iron, Soil;

            public UpgradeItems CheckForItems(int wood, int iron, int soil)
            {
                UpgradeItems result = 0;
                if (Wood >= wood)
                {
                    result |= UpgradeItems.Wood;
                }
                if (Iron >= iron)
                {
                    result |= UpgradeItems.Iron;
                }
                if (Soil >= soil)
                {
                    result |= UpgradeItems.Soil;
                }
                return result;
            }
        }

        int playerAtttackLevel;
        int playerDefenseLevel;

        UpgradeRequestItems WeaponLevelOne;
        UpgradeRequestItems WeaponLevelTwo;
        UpgradeRequestItems ShieldLevelOne;
        UpgradeRequestItems ShieldLevelTwo;

        [SerializeField]
        private Text attackOneWood, attackOneIron, attackOneSoil;
        [SerializeField]
        private Text attackTwoWood, attackTwoIron, attackTwoSoil;
        [SerializeField]
        private Text shieldOneWood, shieldOneIron, shieldOneSoil;
        [SerializeField]
        private Text shieldTwoWood, shieldTwoIron, shieldTwoSoil;
        [SerializeField]
        private Button firstAttackButton, secondAttackButton, firstShieldButton, secondShieldButton;
        [SerializeField]
        private GameObject firstAttackGroup, secondAttackGroup, firstShieldGroup, secondShieldGroup;

        Color textColor = new Color(255, 0, 0, 255); // 빨간색
        Color resetTextColor = new Color(0, 0, 0);//검은색

        private void Start()
        {
            PlayerWood = GameManager.Instance.GetPlayerItemCount(UserType.Player, ItemType.Wood);
            PlayerIron = GameManager.Instance.GetPlayerItemCount(UserType.Player, ItemType.Iron);
            PlayerSoil = GameManager.Instance.GetPlayerItemCount(UserType.Player, ItemType.Soil);

            playerAtttackLevel = GameManager.Instance.GetPlayersAttackLevel((int)UserType.Player);
            playerDefenseLevel = GameManager.Instance.GetPlayersDefenseLevel((int)UserType.Player);

            secondAttackButton.interactable = false;
            secondShieldButton.interactable = false;

            attackOneWood.text = AttackUpgradeOneWood.ToString();
            attackOneIron.text = AttackUpgradeOneIron.ToString();
            attackOneSoil.text = AttackUpgradeOneSoil.ToString();

            attackTwoWood.text = AttackUpgradeTwoWood.ToString();
            attackTwoIron.text = AttackUpgradeTwoIron.ToString();
            attackTwoSoil.text = AttackUpgradeTwoSoil.ToString();

            shieldOneWood.text = DefenseUpgradeOneWood.ToString();
            shieldOneIron.text = DefenseUpgradeOneIron.ToString();
            shieldOneSoil.text = DefenseUpgradeOneSoil.ToString();

            shieldTwoWood.text = DefenseUpgradeTwoWood.ToString();
            shieldTwoIron.text = DefenseUpgradeTwoIron.ToString();
            shieldTwoSoil.text = DefenseUpgradeTwoSoil.ToString();

            WeaponLevelOne = new UpgradeRequestItems
            {
                Wood = PlayerWood,
                Iron = PlayerIron,
                Soil = PlayerSoil,
            };

            WeaponLevelTwo = new UpgradeRequestItems
            {
                Wood = PlayerWood,
                Iron = PlayerIron,
                Soil = PlayerSoil,
            };

            ShieldLevelOne = new UpgradeRequestItems
            {
                Wood = PlayerWood,
                Iron = PlayerIron,
                Soil = PlayerSoil,
            };
            ShieldLevelTwo = new UpgradeRequestItems
            {
                Wood = PlayerWood,
                Iron = PlayerIron,
                Soil = PlayerSoil,
            };
        }

        public void OnclickedButton(int buttonValue)
        {
            UpgradeItems upgradeItems;

            if (buttonValue == 0) //0일때는 무기 1일때는 방어구
            {
                if (playerAtttackLevel == 0)
                {
                    upgradeItems = WeaponLevelOne.CheckForItems(AttackUpgradeOneWood, AttackUpgradeOneIron, AttackUpgradeOneSoil);
                }
                else    //playerWeaponLevel == 1
                {
                    upgradeItems = WeaponLevelTwo.CheckForItems(AttackUpgradeTwoWood, AttackUpgradeTwoIron, AttackUpgradeTwoSoil);
                }
                EquipmentUpgrade(playerAtttackLevel, buttonValue, upgradeItems);
            }
            else //0일때는 무기 1일때는 방어구
            {
                if (playerDefenseLevel == 0)
                {
                    upgradeItems = ShieldLevelOne.CheckForItems(DefenseUpgradeOneWood, DefenseUpgradeOneIron, DefenseUpgradeOneSoil);
                }
                else     //playerShieldLevel == 1
                {
                    upgradeItems = ShieldLevelTwo.CheckForItems(DefenseUpgradeTwoWood, DefenseUpgradeTwoIron, DefenseUpgradeTwoSoil);
                }
                EquipmentUpgrade(playerDefenseLevel, buttonValue, upgradeItems);
            }
            LogManager.Instance.UserDebug(LogColor.Green, GetType().Name, "플레이어가 소지한 나무=" + PlayerWood + "플레이어가 소지한 철=" +
                 PlayerIron + "플레이어가 소지한 흙=" + PlayerSoil);
            LogManager.Instance.UserDebug(LogColor.Green, GetType().Name, "플레이어의 무기 레벨은" + playerAtttackLevel + "플레이어의 방어 레벨은" + playerDefenseLevel);
        }

        void EquipmentUpgrade(int level, int buttonValue, UpgradeItems upgradeItems)
        {
            if (upgradeItems != (UpgradeItems.Wood | upgradeItems))
            {
                //나무 부족
                if (buttonValue == 0)
                {
                    if (level == 0)
                    {
                        attackOneWood.color = textColor; //빨간색!
                        firstAttackButton.interactable = false;
                    }
                    else /*if(level == 1)*/
                    {
                        attackTwoWood.color = textColor;
                        secondAttackButton.interactable = false;
                    }
                }
                if (buttonValue == 1)
                {
                    if (level == 0)
                    {
                        shieldOneWood.color = textColor;
                        firstShieldButton.interactable = false;
                    }
                    else/* if (level == 1)*/
                    {
                        shieldTwoWood.color = textColor;
                        secondShieldButton.interactable = false;
                    }
                }
            }
            if (upgradeItems != (UpgradeItems.Iron | upgradeItems))
            {
                //철 부족
                if (buttonValue == 0)
                {
                    if (level == 0)
                    {
                        attackOneIron.color = textColor; //빨간색!
                        firstAttackButton.interactable = false;
                    }
                    else  //level == 1
                    {
                        attackTwoIron.color = textColor;
                        secondAttackButton.interactable = false;
                    }
                }
                if (buttonValue == 1)
                {
                    if (level == 0)
                    {
                        shieldOneIron.color = textColor;
                        firstShieldButton.interactable = false;
                    }
                    else  //level == 1
                    {
                        shieldTwoIron.color = textColor;
                        secondShieldButton.interactable = false;
                    }
                }
            }
            if (upgradeItems != (UpgradeItems.Soil | upgradeItems))
            {
                //흙 부족
                if (buttonValue == 0)
                {
                    if (level == 0)
                    {
                        attackOneSoil.color = textColor;//빨간색!
                        firstAttackButton.interactable = false;
                    }
                    else  //level == 1
                    {
                        attackTwoSoil.color = textColor;
                        secondAttackButton.interactable = false;
                    }
                }
                if (buttonValue == 1)
                {
                    if (level == 0)
                    {
                        shieldOneSoil.color = textColor;
                        firstShieldButton.interactable = false;
                    }
                    else  //level == 1
                    {
                        shieldTwoSoil.color = textColor;
                        secondShieldButton.interactable = false;
                    }
                }
            }

            if (upgradeItems == (UpgradeItems.Iron | UpgradeItems.Soil | UpgradeItems.Wood))
            {
                //첫번째 업그레이드
                if (buttonValue == 0)
                {
                    if (level == 0)
                    {
                        PlayerWood -= AttackUpgradeOneWood;
                        PlayerIron -= AttackUpgradeOneIron;
                        PlayerSoil -= AttackUpgradeOneSoil;
                        firstAttackGroup.SetActive(false);
                        firstAttackButton.interactable = false;
                        secondAttackButton.interactable = true;
                        playerAtttackLevel++;
                    }
                    else if (level == 1)
                    {
                        PlayerWood -= AttackUpgradeTwoWood;
                        PlayerIron -= AttackUpgradeTwoIron;
                        PlayerSoil -= AttackUpgradeTwoSoil;
                        secondAttackGroup.SetActive(false);
                        secondAttackButton.interactable = false;
                        playerAtttackLevel++;
                    }
                    SendPlayerAttackLevel();
                    SendPlayerItems();
                }
                if (buttonValue == 1)
                {
                    if (level == 0)
                    {
                        PlayerWood -= DefenseUpgradeOneWood;
                        PlayerIron -= DefenseUpgradeOneIron;
                        PlayerSoil -= DefenseUpgradeOneSoil;
                        secondShieldButton.interactable = true;
                        firstShieldGroup.SetActive(false);
                        firstShieldButton.interactable = false;
                        playerDefenseLevel++;
                    }
                    else if (level == 1)
                    {
                        PlayerWood -= DefenseUpgradeTwoWood;
                        PlayerIron -= DefenseUpgradeTwoIron;
                        PlayerSoil -= DefenseUpgradeTwoSoil;
                        secondShieldGroup.SetActive(false);
                        secondShieldButton.interactable = false;
                        playerDefenseLevel++;
                    }
                    SendPlayerDefenseLevel();
                    SendPlayerItems();
                }
            }
        }
        
        //유아이 매니저로 데이터 보내기
        private void SendPlayerAttackLevel()
        {
            UIManager.Instance.SendPlayerAttackLevel(playerAtttackLevel);
        }

        private void SendPlayerDefenseLevel()
        {
            UIManager.Instance.SendPlayerAttackLevel(playerDefenseLevel);
        }

        private void SendPlayerItems()
        {
            UIManager.Instance.SendPlayerItems(PlayerWood, PlayerIron, PlayerSoil);
        }

        public void ResetTextsAndButtons()
        {
            if (playerAtttackLevel == 0)
            {
                firstAttackButton.interactable = true;
                attackOneIron.color = resetTextColor;
                attackOneSoil.color = resetTextColor;
                attackOneWood.color = resetTextColor;
            }
            else
            {
                secondAttackButton.interactable = true;
                attackTwoIron.color = resetTextColor;
                attackTwoSoil.color = resetTextColor;
                attackTwoWood.color = resetTextColor;
            }

            if (playerDefenseLevel == 0)
            {
                firstShieldButton.interactable = true;
                shieldOneIron.color = resetTextColor;
                shieldOneSoil.color = resetTextColor;
                shieldOneWood.color = resetTextColor;
            }
            else
            {
                secondShieldButton.interactable = true;
                shieldTwoIron.color = resetTextColor;
                shieldTwoSoil.color = resetTextColor;
                shieldTwoWood.color = resetTextColor;
            }
        }
    }
}