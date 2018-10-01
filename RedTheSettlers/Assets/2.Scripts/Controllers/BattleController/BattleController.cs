using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Enemys;
using RedTheSettlers.Players;
using RedTheSettlers.Tiles;

namespace RedTheSettlers.GameSystem
{
    public delegate void BattleFinishCallback(bool isWin);
    public delegate void EnemyDeadCallback();
    public delegate void PlayerDeadCallback();

    public class BattleController : MonoBehaviour
    {
        public BattlePlayer Player;
        private List<Enemy> enemyList;
        private GameTimer cattlesTimer;

        private bool isWin;
        private float cattleResawnTime = 20; // second

        private BattleFinishCallback _callback;
        public BattleFinishCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public void BattleFlow(BoardTile tileInfo)
        {
            if (tileInfo.TileType == ItemType.Cow)
            {
                LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "Cow 타이머 시작");

                cattlesTimer = GameTimeManager.Instance.PopTimer();
                cattlesTimer.SetTimer(cattleResawnTime, true);
                cattlesTimer.Callback = new TimerCallback(SpawnHerdOfCattles);
                //cattlesTimer.Callback = new TimerCallback(SpawnCattleTest); // 테스트용

                cattlesTimer.StartTimer();
            }
        }

        // 일정 시간마다 소 떼가 등장한다.
        private void SpawnHerdOfCattles()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "소 떼 출현");

            Quaternion angle = Quaternion.Euler(0f, Random.Range(0, 360f), 0f);
            // 스폰 포인트 정상적으로 위치되는지 잘 확인할 것 // 유닛 테스트 안됨 
            Vector3 spawnPoint = new Vector3
            (
                TileManager.Instance.BattleTileGrid[Random.Range(1, 14), Random.Range(1, 14)].transform.position.x + GlobalVariables.BattleAreaOriginCoord,
                0,
                TileManager.Instance.BattleTileGrid[Random.Range(1, 14), Random.Range(1, 14)].transform.position.z + GlobalVariables.BattleAreaOriginCoord
            );

            GameObject cows = ObjectPoolManager.Instance.CowObject;
            cows.transform.position = spawnPoint;
            cows.transform.rotation = angle;
        }

        private void ClearBattle()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "전투 종료");

            if (cattlesTimer != null)
            {
                cattlesTimer.StopTimer();
                GameTimeManager.Instance.PushTimer(cattlesTimer);
            }

            Callback(isWin);
        }

        private void EnemyDead()
        {
            if (enemyList.Count > 0)
            {
                LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "Enemy Dead!");
            }
            else // enemyList.Count == 0
            {
                LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "전투 승리!");
                isWin = true;
                ClearBattle();
            }
        }

        private void PlayerDead()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "플레이어 사망!");
            isWin = false;
            ClearBattle();
        }

        /// <summary>
        /// Player와 EnemyList에서 오브젝트를 받아온다.
        /// </summary>
        public void ReceiveEnemysAndPlayer(List<Enemy> enemys, BattlePlayer player)
        {
            Player = player;
            enemyList = enemys;

            Player.playerDeadCallBack = PlayerDead;

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].enemyDeadCallback = EnemyDead;
            }
        }

        /// <summary>
        /// Test용 코드. SpawnHerdOfCattles()가 원본
        /// </summary>
        public void SpawnCattleTest()
        {
            Quaternion angle = Quaternion.Euler(0f, Random.Range(0, 360f), 0f);
            Vector3 spawnPoint = new Vector3(0, 0, 0);

            GameObject cowsTest = ObjectPoolManager.Instance.CowObject;
            cowsTest.transform.position = spawnPoint;
            cowsTest.transform.rotation = angle;
        }
    }
}