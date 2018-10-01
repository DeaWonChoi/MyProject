using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Tiles;

namespace RedTheSettlers.GameSystem
{

    public class TilePool : MonoBehaviour
    {
        public const int TileListSize = 6;
        public const int BoardTileQueueSize = 60;
        public const int BattleTileQueueSize = 200;

        public const int TileCloneSetSize = 6;

        [Header("Tiles")]
        public GameObject[] BoardTileObjects = new GameObject[6];
        public GameObject[] BattleTileObjects = new GameObject[6];
        public GameObject[] BattleObstacleTileObjects = new GameObject[6];

        public GameObject[] BoardTileCloneSet = new GameObject[6];
        public GameObject[] BattleTileCloneSet = new GameObject[6];
        public GameObject[] BattleObstacleTileCloneSet = new GameObject[6];

        public List<Queue<GameObject>> BoardTileQueueList;
        public List<Queue<GameObject>> BattleTileQueueList;
        public List<Queue<GameObject>> BattleObstacleTileQueueList;

        private void Awake()
        {
            BoardTileQueueList = new List<Queue<GameObject>>(TileListSize);

            for (int i = 0; i < TileListSize; i++)
            {
                Queue<GameObject> tileQueue = new Queue<GameObject>(BoardTileQueueSize);

                for (int index = 0; index < BoardTileQueueSize; index++)
                {
                    tileQueue.Enqueue(Instantiate(BoardTileObjects[i], BoardTileCloneSet[i].transform));
                }
                BoardTileQueueList.Insert(i, tileQueue);
            }

            BattleTileQueueList = new List<Queue<GameObject>>(TileListSize);

            for (int i = 0; i < TileListSize; i++)
            {
                Queue<GameObject> tileQueue = new Queue<GameObject>(BattleTileQueueSize);

                for (int index = 0; index < BattleTileQueueSize; index++)
                {
                    tileQueue.Enqueue(Instantiate(BattleTileObjects[i], BattleTileCloneSet[i].transform));
                }
                BattleTileQueueList.Insert(i, tileQueue);
            }

            BattleObstacleTileQueueList = new List<Queue<GameObject>>(TileListSize);

            for (int i = 0; i < TileListSize; i++)
            {
                Queue<GameObject> tileQueue = new Queue<GameObject>(BattleTileQueueSize);

                for (int index = 0; index < BattleTileQueueSize; index++)
                {
                    if(BattleObstacleTileObjects[i] != null)
                    tileQueue.Enqueue(Instantiate(BattleObstacleTileObjects[i], BattleObstacleTileCloneSet[i].transform));
                }
                BattleObstacleTileQueueList.Insert(i, tileQueue);
            }

        }

        public void PushBattleTile(GameObject battleTile)
        {
            battleTile.SetActive(false);

            int index = (int)(battleTile.GetComponent<Tile>().TileType);
            BattleTileQueueList[index].Enqueue(battleTile);
        }

        public GameObject PopBattleTile(ItemType itemType)
        {
            return BattleTileQueueList[(int)itemType].Dequeue();
        }

        public void PushBoardTile(GameObject boardTile)
        {
            boardTile.SetActive(false);

            int index = (int)(boardTile.GetComponent<Tile>().TileType);
            BoardTileQueueList[index].Enqueue(boardTile);
        }

        public GameObject PopBoardTile(ItemType itemType)
        {
            return BoardTileQueueList[(int)itemType].Dequeue();
        }

        public void PushBattleObstacleTile(GameObject battleObstacleTile)
        {
            battleObstacleTile.SetActive(false);

            int index = (int)(battleObstacleTile.GetComponent<Tile>().TileType);
            BattleObstacleTileQueueList[index].Enqueue(battleObstacleTile);
        }

        public GameObject PopBattleObstacleTile(ItemType itemType)
        {
            return BattleObstacleTileQueueList[(int)itemType].Dequeue();
        }
    }
}
