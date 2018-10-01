
namespace RedTheSettlers.GameSystem
{

    public enum ItemType
    {
        Cow = 0,
        Iron = 1,
        Soil = 2,
        Water = 3,
        Wheat = 4,
        Wood = 5
    }

    public enum TileOwner
    {
        Player = 0,
        AI1 = 1,
        AI2 = 2,
        AI3 = 3,
        None = 4
    }

    public enum UserType
    {
        Player = 0,
        AI1 = 1,
        AI2 = 2,
        AI3 = 3,
    }

    public enum QuickSlot
    {
        First = 0,
        Second = 1,
        Third = 2,
    }

    public enum OtherPlayerState
    {
        Trade,
        No,
        Yes,
    }

    public enum GameState
    {
        EventController,
        ItemController,
        PlayerTurn,
        AI1Turn,
        AI2Turn,
        AI3Turn,
    }

    public enum AssetBundleNumbers
    {
        Player,
        Skill,
        Enemy, // unload(false)
        MiddleBoss1,
        MiddleBoss2,
        Boss,
        Tile, // unload(false)
        UI,
        Count,
    }

    static class GlobalVariables
    {
        public const int BoardTileGridSize = 9;
        public const int BoardTileMinZIntercept = 3;
        public const int BoardTileMaxZIntercept = 13;

        public const int BattleTileGridSize = 15;
        public const int BattleTileMinZIntercept = 9;
        public const int BattleTileMaxZIntercept = 19;

        public const int MaxItemNum = 50;
        public const int MaxEquipmentUpgradeLevel = 3;
        public const int MaxTileUpgradeLevel = 3;
        public const int MaxTileCount = 39;

        public const int CardWeightValue = 1000;
        public const int EquipmentWeightValue = 3000;
        public const int BonusWeightValue = 3000;

        public const int MaxPlayerNumber = 4;
        public const int MaxItemNumber = 6;
        public const int MaxSkillNumber = 4;
        public const int MiddleBoss1AppearTurn = 12;
        public const int MiddleBoss2AppearTurn = 24;
        public const int BossAppearTurn = 36;
        public const int WeatherEventStartTurn = 5;

        public const int BattleAreaOriginCoord = 50;

        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
        public const string TAG_GAMECONTROLLER = "GameController";
        public const string TAG_TILE = "Tile";
        public const string TAG_FIRE = "Fire";
        public const string TAG_SKILLICON = "SkillIcon";
        public const string TAG_WALL = "Wall";
        public const string TAG_MAINCAMERA = "MainCamera";
        public const string TAG_BOARDCAMERA = "BoardCamera";
        public const string TAG_BATTLECAMERA = "BattleCamera";
        public const string TAG_UICAMERA = "UICamera";
        public const string TAG_UIICON = "UIIcon";
        public const string TAG_ENEMYATTACK = "EnemyAttack";
        public const string TAG_PLAYERATTACK = "PlayerAttack";
        
    }
}