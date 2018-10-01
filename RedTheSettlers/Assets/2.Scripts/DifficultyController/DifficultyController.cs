using RedTheSettlers.Enemys;
using RedTheSettlers.Tiles;
using RedTheSettlers.Users;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RedTheSettlers.GameSystem
{
    public enum BattleLevel
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
    }

    public delegate void BuildBattleTileCallback();
    /// <summary>
    /// 작성자 : 박준명
    /// </summary>
    public class DifficultyController : MonoBehaviour
    {
        public BuildBattleTileCallback Callback;
        private BattleLevel battlelevel;
        private ItemType tileType;
        private int BossCount;

        public List<GameObject> EnemyList
        {
            get { return EnemyList; }
            private set { EnemyList = value; }
        }
        public GameObject Player
        {
            get { return Player; }
            private set { Player = value; }
        }


        private IEnumerator SendBattleLevel()
        {
            TileManager.Instance.InitializeBattleTileSet(tileType, (int)battlelevel);
            yield return null;
        }      
        
        /// <summary>
        /// DifficultyController의 기능이 시작되는 메인 코루틴.
        /// </summary>
        /// <param name="selectionTile"></param>
        /// <param name="PossessingTileList"></param>
        /// <returns></returns>
        public IEnumerator EstablishBattleStage(BoardTile selectionTile, List<Tile> PossessingTileList)
        {
            judgeLevel(selectionTile, PossessingTileList);
            yield return StartCoroutine(SendBattleLevel());
            DisposePlayerAndEnemy(selectionTile.IsBossTile); //후에 타일클래스에 구분 bool타입 변수 생기면 변경 예정 true - > selectionTile.isBossTile
            Callback();
        }
        
        private void judgeLevel(Tile selectionTile, List<Tile> PossessingTileList)
        {
            tileType = selectionTile.TileType;
            int count = 0;
            if (selectionTile.TileLevel == 0)
            {
                for (int i = 0; i < PossessingTileList.Count; i++)
                {
                    if (PossessingTileList[i].TileType == tileType)
                        count++;
                }
                if (count < 2)
                {
                    battlelevel = BattleLevel.Level1;
                    LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "Level1으로 설정");
                }
                else if (count == 2 || count == 3)
                {
                    battlelevel = BattleLevel.Level2;
                    LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "Level2으로 설정");
                }
                else
                {
                    battlelevel = BattleLevel.Level3;
                    LogManager.Instance.UserDebug(LogColor.Magenta, GetType().Name, "Level3으로 설정");
                }
            }
            else
            {
                battlelevel = (BattleLevel)selectionTile.TileLevel;
            }
            
        }

        private void DisposePlayerAndEnemy(bool isBossTile)
        {
            Vector3 playerSpotPosition = new Vector3
                (
                TileManager.Instance.BattleTileGrid[13, 1].transform.position.x + GlobalVariables.BattleAreaOriginCoord, 0,
                TileManager.Instance.BattleTileGrid[13, 1].transform.position.z + GlobalVariables.BattleAreaOriginCoord
                );
            Vector3 enemySpotPosition = new Vector3
                (
                TileManager.Instance.BattleTileGrid[1, 13].transform.position.x + GlobalVariables.BattleAreaOriginCoord, 0,
                TileManager.Instance.BattleTileGrid[1, 13].transform.position.z + GlobalVariables.BattleAreaOriginCoord
                );
            DisposePlayer(playerSpotPosition);
            if (isBossTile)
                DisposeBoss(enemySpotPosition);
            else
                DisposeEnemy(enemySpotPosition);

        }

        private void DisposePlayer(Vector3 position)
        {
            Player = ObjectPoolManager.Instance.PlayerObjectPool.GetPlayerObject();
            Player.transform.position = position;
            Player.SetActive(true);
        }

        private void DisposeBoss(Vector3 position)
        {
            EnemyList = new List<GameObject>();
            EnemyList.Add(ObjectPoolManager.Instance.EnemyObjectPool.PopBossObject());
            BossEnemy bossenemyScript = EnemyList[0].GetComponent<BossEnemy>();
            switch (BossCount)
            {
                case 0:
                    bossenemyScript.SetStatus(100, 10, false);
                    bossenemyScript.SetType(false);
                    break;
                case 1:
                    bossenemyScript.SetStatus(150, 15, false);
                    bossenemyScript.SetType(false);
                    break;
                case 2:
                    bossenemyScript.SetStatus(200, 25, true);
                    bossenemyScript.SetType(true);
                    break;
            }
            BossCount++;
        }

        private void DisposeEnemy(Vector3 position)
        {
            EnemyList = new List<GameObject>();
            for (int i = 0; i <= (int)battlelevel; i++)
            {
                //ObjectPoolManager.Instance.EnemyPool[(int)tileType].Dequeue();   ObjectManager에서 리스트 큐 형식으로만 쓴다면 이렇게.
                EnemyList.Add(ObjectPoolManager.Instance.EnemyObjectPool.PopEnemyObject());
                EnemyList[i].GetComponent<NormalEnemy>().SetStatus((int)battlelevel);
            }
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (i == 0)
                    EnemyList[i].transform.position = position;
                else if (i % 2 == 1)
                {
                    position.x += 2;
                    position.z += 2;
                    EnemyList[i].transform.position = position;
                }
                else if (i % 2 == 0)
                {
                    position.x = -position.x;
                    position.z = -position.z;
                    EnemyList[i].transform.position = position;
                }
                EnemyList[i].SetActive(true);
                EnemyList[i].GetComponent<NormalEnemy>().SetType(tileType);
            }
        }

        /*
        DifficultyController가 해야할 일.
        1. 소유 타일에 따라 정해진 난이도를 타일 매니저에 보내줌.

        2. 플레이어가 소유하고 있는 동일한 타일 갯수마다 몬스터 스탯 및 수 증가.

        3. 보스타일일 경우엔 선택 타일이 아닌 랜덤으로 타일이 배치됨
         - 소,물,밀,철,흙,나무 중 아무거나
         - 보스의 위치는 고정.

         */
    }
}
