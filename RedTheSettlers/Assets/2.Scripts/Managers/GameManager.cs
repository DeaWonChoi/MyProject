using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using RedTheSettlers.Tiles;
using RedTheSettlers.UI;
using RedTheSettlers.Users;
using RedTheSettlers.Players;
using RedTheSettlers.Enemys;

namespace RedTheSettlers.GameSystem
{
    public delegate void FlowFinishCallback();

    /// <summary>
    /// 각 컨트롤러를 관리하고 중재하는 매니저
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public User[] Players;
        public GameData gameData = DataManager.Instance.GameData;

        public PlayerTurnController turnCtrl;
        public EventChecker eventCtrl;
        public ItemDistributor itemCtrl;
        public TradeController tradeCtrl;
        public BattleController battleCtrl;
        public CameraController cameraCtrl;
        public DifficultyController difficultyController;

        public GameState state = GameState.EventController;
        private Coroutine coroutineMove;

        private BoardTile tileType;

        public BoardTile TileType
        {
            get { return TileType; }
            set { tileType = value; }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            cameraCtrl.InitializeCamera();
        }

        private void Start()
        {
            turnCtrl.Callback = new FlowFinishCallback(GameFlowFinish);
            eventCtrl.Callback = new FlowFinishCallback(GameFlowFinish); ;
            itemCtrl.Callback = new FlowFinishCallback(GameFlowFinish);
            battleCtrl.Callback = new BattleFinishCallback(BattleFinish);
            difficultyController.Callback = new BuildBattleTileCallback(BulidBattleStageFinish);
            InitializeGame();
        }

        public void InitializeGame()
        {
            //처음시작일때
            if (gameData.InGameData.TurnCount == 0)
            {
                TileManager.Instance.InitializeBoardTileSet();
            }
            //이어하기일때
            else
            {
                TileManager.Instance.LoadTileGrid();
            }
            for(int i = 0; i < Players.Length; i++)
            {
                if (Players[i] is BoardPlayer)
                {
                    (Players[i] as BoardPlayer).MoveToTargetTile(TileManager.Instance.BoardTileGrid[3, 5].GetComponent<BoardTile>());
                    Instantiate(Players[i], TileManager.Instance.BoardTileGrid[3, 5].transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                }
                else
                {
                    (Players[i] as BoardAI).PossessTile(TileManager.Instance.BoardTileGrid[i + 4, 4 - i].GetComponent<BoardTile>());
                    Instantiate(Players[i], TileManager.Instance.BoardTileGrid[i + 4, 4 - i].transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                }
            }
        }

        public void StartGameFlow(bool isfirst)
        {
            if (!isfirst)
            {
                ChangeGameFlow();
            }
            switch (state)
            {
                case GameState.EventController:
                    eventCtrl.EventFlow();
                    break;

                case GameState.ItemController:
                    itemCtrl.ItemDistributeFlow();
                    break;

                default:
                    turnCtrl.TurnFlow(state);

                    break;
            }
        }

        public void GameFlowFinish()
        {
            switch (state)
            {
                case GameState.EventController:

                    break;

                case GameState.ItemController:
                    UIManager.Instance.ShowBoardUI();
                    break;

                default:
                    break;
            }
            StartGameFlow(false);
        }

        private void BulidBattleStageFinish()
        {
            BattlePlayer player = difficultyController.Player.GetComponent<BattlePlayer>();
            List<Enemy> enemyList = new List<Enemy>();
            List<GameObject> enemys = difficultyController.EnemyList;
            for (int i = 0; i < enemys.Count; i++)
            {
                enemyList.Add(enemys[i].GetComponent<Enemy>());
            }

            StageManager.Instance.ChangeState(StageType.BattleStageState);
            battleCtrl.ReceiveEnemysAndPlayer(enemyList, player);
            battleCtrl.BattleFlow(tileType);
        }

        private void BattleFinish(bool isWin)
        {
            if (isWin)
            {
                tileType.tileOwner = TileOwner.Player;
            }
            StageManager.Instance.ChangeState(StageType.BoardScene);
        }

        private void ChangeGameFlow()
        {
            if (state == GameState.AI3Turn)
            {
                if (DataManager.Instance.GameData.InGameData.TurnCount == GlobalVariables.BossAppearTurn)
                    state = GameState.EventController;
                DataManager.Instance.GameData.InGameData.TurnCount++;
                UIManager.Instance.ShowAiTurnUI();
            }
            else
                state++;
        }

        /// <summary>
        /// 플레이어가 보유한 자원량의 합을 반환합니다.
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public int GetPlayerItemCountAll(UserType userType)
        {
            int countSum = 0;
            for (int i = 0; i < GlobalVariables.MaxItemNumber; i++)
            {
                countSum += Players[(int)userType].inventory[i].Count;
            }
            return countSum;
        }

        /// <summary>
        /// 플레이어가 보유한 특정 자원의 개수를 반환합니다.
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public int GetPlayerItemCount(UserType userType, ItemType itemType)
        {
            return Players[(int)userType].inventory[(int)itemType].Count;
        }

        /// <summary>
        /// 플레이어가 보유한 모든 타일의 개수를 반환합니다.
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public int GetPlayerTileCountAll(UserType userType)
        {
            return Players[(int)userType].PossessingTile.Count;
        }

        /// <summary>
        /// 플레이어가 보유한 특정 자원 타일의 개수를 반환합니다.
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="tiletype"></param>
        /// <returns></returns>
        public int GetPlayerTileCount(UserType userType, ItemType itemType)
        {
            int tileCount = 0;
            for (int i = 0; i < GetPlayerTileCountAll(userType); i++)
            {
                if (Players[(int)userType].PossessingTile[i].TileType == itemType)
                {
                    tileCount++;
                }
            }
            return tileCount;
        }

        /// <summary>
        /// 플레이어가 보유한 특정 타일의 레벨 합을 반환합니다.
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="tiletype"></param>
        /// <returns></returns>
        public int GetPlayerTileLevelCount(UserType userType, ItemType itemType)
        {
            int tileLevelCount = 0;
            for (int i = 0; i < GetPlayerTileCountAll(userType); i++)
            {
                if (Players[(int)userType].PossessingTile[i].TileType == itemType)
                {
                    tileLevelCount += Players[(int)userType].PossessingTile[i].TileLevel;
                }
            }
            return tileLevelCount;
        }

        /// <summary>
        /// 특정 플레이어의 아이템의 수량을 조절합니다.
        /// </summary>
        /// <param name="playerNumber"></param>
        /// <param name="itemType"></param>
        /// <param name="addItem"></param>
        public void AddItemByType(int playerNumber, ItemType itemType, int addItem)
        {
            DataManager.Instance.GameData.PlayerData[playerNumber].ItemList[(int)itemType].Count += addItem;
        }

        /// <summary>
        /// 클릭한 타일을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public void GetClickedTile(BoardTile boardTile)
        {
            UIManager.Instance.SendTileInfo(boardTile);
            cameraCtrl.LookingTile(boardTile);
        }

        public void NonClickedTile()
        {
            cameraCtrl.NoLookingTile();
        }

        /// <summary>
        /// 배틀스테이지의 타일을 배치합니다.
        /// </summary>
        /// <param name="tileinfo"></param>
        public void BulidBattleTile(BoardTile tileinfo)
        {
            TileType = tileinfo;
            StartCoroutine(difficultyController.EstablishBattleStage(tileinfo, Players[0].PossessingTile));
        }

        /// <summary>
        /// 선택된 날씨 정보를 UI로 전달합니다.
        /// </summary>
        public void SendWeatherCard(int[] weathers)
        {
            UIManager.Instance.ShowWheatherEvent(weathers);
        }

        /// <summary>
        /// 거래 정보를 가져옵니다.
        /// </summary>
        public void SendTradeData(ItemData[] itemDatas, int requestPlayer, int receivePlayer)
        {
            TradeData tradeData = new TradeData
            {
                ItemsToTrade = itemDatas,
                RequestReceiver = Players[receivePlayer],
                RequestSender = Players[requestPlayer]
            };
            tradeCtrl.DoTrade(tradeData);
        }

        /// <summary>
        /// 거래 결과를 전달합니다.
        /// </summary>
        public void SendTradeResult(OtherPlayerState otherPlayerState)
        {
            UIManager.Instance.RecieveTradeResult(otherPlayerState);
        }

        public void SetWeatherEventNumber(int eventNumber)
        {
            gameData.InGameData.Weather = eventNumber;
            DataManager.Instance.SaveGameData(gameData, true);
        }

        /// <summary>
        /// 카메라를 전환시킵니다.
        /// </summary>
        public void ChangedCamera(StateType stateType)
        {
            cameraCtrl.ChangeCamera(stateType);
        }

        /// <summary>
        /// 카메라 줌 인/아웃
        /// </summary>
        /// <param name="ZoomValue"></param>
        public void CameraZoomInOut(float ZoomValue)
        {
            cameraCtrl.ZoomInOut(ZoomValue);
        }

        /// <summary>
        /// 카메라 이동
        /// </summary>
        /// <param name="direction"></param>
        public void CameraMoving(Vector3 direction)
        {
            cameraCtrl.CameraDragMoving(direction);
        }

        /// <summary>
        /// 플레이어를 이동시킵니다.
        /// </summary>
        /// <param name="direction"></param>
        public void PlayerMove(Vector3 direction)
        {
            Vector3 targetPosition = direction + new Vector3(0, battleCtrl.Player.transform.position.y, 0);
            if (coroutineMove == null)
            {
                coroutineMove = StartCoroutine(battleCtrl.Player.MoveToTargetPostion(direction));
            }
            else
            {
                StopCoroutine(coroutineMove);
                coroutineMove = StartCoroutine(battleCtrl.Player.MoveToTargetPostion(direction));
            }
        }

        /// <summary>
        /// 플레이어의 공격을 실행합니다.
        /// </summary>
        public void PlayerAttack()
        {
            battleCtrl.Player.AttackEnemy(10);
        }

        /// <summary>
        /// 플레이어의 스킬을 실행합니다.
        /// </summary>
        /// <param name="skillNumber"></param>
        public void PlayerSkill(int skillNumber)
        {
            battleCtrl.Player.UseSkill(skillNumber);
        }

        /// <summary>
        /// 플레이어의 스텟을 설정합니다.
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="mp"></param>
        /// <param name="stamina"></param>
        public void SetPlayerStat(int hp, int mp, int stamina)
        {
            DataManager.Instance.GameData.PlayerData[0].StatData.HealthPoint = hp;
            DataManager.Instance.GameData.PlayerData[0].StatData.MagicPoint = mp;
            DataManager.Instance.GameData.PlayerData[0].StatData.StaminaPoint = stamina;
        }

        /// <summary>
        /// AI의 진행 상황을 담은 큐를 전달합니다.
        /// </summary>
        public void SetGameLog(Queue<string> messageQueue)
        {
            //UIManager.Instance.
        }

        private void SendPlayers()
        {
            turnCtrl.SetAIs(Players);
        }

        //플레이어의 무기, 방어구 설정하는 메서드들-----
        public int GetPlayersAttackLevel(int playerNum)
        {
            return DataManager.Instance.GameData.PlayerData[playerNum].StatData.WeaponLevel;
        }

        public int GetPlayersDefenseLevel(int playerNum)
        {
            return DataManager.Instance.GameData.PlayerData[playerNum].StatData.ShieldLevel;
        }

        public void SetPlayersAttackLevel(int playerNum, int level)
        {
            DataManager.Instance.GameData.PlayerData[playerNum].StatData.WeaponLevel = level;
        }

        public void SetPlayersDefenseLevel(int playerNum, int level)
        {
            DataManager.Instance.GameData.PlayerData[playerNum].StatData.ShieldLevel = level;
        }

        public int GetPlayersBossKillCount(int playerNum)
        {
            return DataManager.Instance.GameData.PlayerData[playerNum].BossKillCount;
        }

        //-----
    }
}