using System.Collections.Generic;
using System;
using RedTheSettlers.Tiles;

namespace RedTheSettlers.GameSystem
{
    [Serializable]
    public struct StatData
    {
        public int HealthPoint;
        public int MaxHealthPoint;
        public int MagicPoint;
        public int MaxMagicPoint;
        public int StaminaPoint;
        public int WeaponLevel;
        public int ShieldLevel;
    }

    [Serializable]
    public struct SkillData
    {
        public int SkillNumber;
        public QuickSlot SlotNumber;
    }

    [Serializable]
    public struct TileData
    {
        public ItemType TileType;
        public int LocationX;
        public int LocationZ;
        public int TileLevel;
    }

    [Serializable]
    public struct ItemData
    {
        public ItemType ItemType;
        public int Count;
    }

    [Serializable]
    public struct InGameData
    {
        public TileData[] BoardTileList;
        public int TurnCount;
        public int Weather;
    }

    /// <summary>
    /// 작성자 : 박준명
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public StatData StatData;
        public List<SkillData> SkillList = new List<SkillData>();
        public List<TileData> TileList = new List<TileData>();
        public ItemData[] ItemList = new ItemData[6];
        public int BossKillCount;
        public PlayerData()
        {
            for (int i = 0; i < GlobalVariables.MaxItemNumber; i++)
            {
                ItemList[i].ItemType = (ItemType)i;
            }
        }
    }

    /// <summary>
    /// 작성자 : 박준명
    /// </summary>
    [Serializable]
    public class GameData
    {
        public string UserId;
        public string UserPassword;
        public InGameData InGameData;
        public PlayerData[] PlayerData;

        public GameData(int numberOfPlayer)
        {
            PlayerData = new PlayerData[numberOfPlayer];
            InGameData.BoardTileList = new TileData[61];
            for (int i = 0; i < PlayerData.Length; i++)
            {
                PlayerData[i] = new PlayerData();
            }
        }

    }
}

