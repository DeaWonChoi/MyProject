using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Tiles;

namespace RedTheSettlers.GameSystem
{
    public class TileManager : Singleton<TileManager>
    {
        public GameObject[,] BoardTileGrid;
        public GameObject[,] BattleTileGrid;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            BoardTileGrid = new GameObject[GlobalVariables.BoardTileGridSize, GlobalVariables.BoardTileGridSize];
            BattleTileGrid = new GameObject[GlobalVariables.BattleTileGridSize, GlobalVariables.BattleTileGridSize];

            InitializeBattleTileSet(ItemType.Water, 0);
        }

        public void InitializeBoardTileSet()
        {
            CreateBoardTileGrid();
            ShowBoardTile();
        }

        public void InitializeBattleTileSet(ItemType itemType, int difficulty)
        {
            CreateBattleTileGrid(itemType, difficulty);
            ShowBattleTile();
        }
        
        public void LoadTileGrid()
        {
            int maxCount = DataManager.Instance.GameData.InGameData.BoardTileList.Length;

            PlayerData[] playerData = DataManager.Instance.GameData.PlayerData;

            for(int i = 0; i < maxCount; i++)
            {
                TileData tileData = DataManager.Instance.GameData.InGameData.BoardTileList[i];

                int x = tileData.LocationX;
                int z = tileData.LocationZ;
                BoardTileGrid[x, z] = ObjectPoolManager.Instance.TileObjectPool.PopBoardTile(tileData.TileType);
                BoardTileGrid[x, z].GetComponent<BoardTile>().TileLevel = tileData.TileLevel;
            }
            for (int j = 0; j < playerData.Length; j++)
            {
                for (int k = 0; k < playerData[j].TileList.Count; k++)
                {
                    foreach(TileData tileData in playerData[j].TileList)
                    {
                        BoardTileGrid[tileData.LocationX, tileData.LocationZ].GetComponent<BoardTile>().tileOwner = (TileOwner)j;
                    }
                }
            }

            for (int z = 0; z < GlobalVariables.BoardTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BoardTileGridSize; x++)
                {
                    if (z > -x + GlobalVariables.BoardTileMinZIntercept && z < -x + GlobalVariables.BoardTileMaxZIntercept)
                    {
                        int[] coordX = { 1, 0, -1, -1, 0, 1 };
                        int[] coordZ = { 0, 1, 1, 0, -1, -1 };

                        for (int i = 0; i < 6; i++)
                        {
                            if(BoardTileGrid[x + coordX[i], z + coordZ[i]] != null  
                                || x + coordX[i] < 0 || x + coordX[i] >= GlobalVariables.BoardTileGridSize  
                                || z + coordZ[i] < 0 || z + coordZ[i] >= GlobalVariables.BoardTileGridSize)
                            {
                                BoardTile targetBoardTile = BoardTileGrid[x + coordX[i], z + coordZ[i]].GetComponent<BoardTile>();
                                if (targetBoardTile.tileOwner != TileOwner.None)
                                {
                                    BoardTileGrid[x,z].GetComponent<BoardTile>().TileBorder[i].SetActive(false);
                                    targetBoardTile.TileBorder[(i + 3) % 6].SetActive(false);
                                }
                                else
                                {
                                    BoardTileGrid[x, z].GetComponent<BoardTile>().TileBorder[i].SetActive(true);
                                }
                            }

                        }
                    }
                }
            }
            ShowBoardTile();
        }

        public void CreateBoardTileGrid()
        {
            for (int z = 0; z < GlobalVariables.BoardTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BoardTileGridSize; x++)
                {
                    if (z > - x + GlobalVariables.BoardTileMinZIntercept && z < -x + GlobalVariables.BoardTileMaxZIntercept)
                    {
                        float xCoord = CalculateXcoord(x, z);
                        float zCoord = CalculateZcoord(z);
                        BoardTileGrid[x, z] = ObjectPoolManager.Instance.TileObjectPool.PopBoardTile((ItemType)Random.Range(0, 6));
                        BoardTileGrid[x, z].transform.position = new Vector3(xCoord, 0.05f, zCoord);
                        BoardTileGrid[x, z].GetComponent<BoardTile>().TileCoordinate = new Coordinate(x, z);
                    }
                }
            }
        }

        public void CreateBattleTileGrid(ItemType itemType, int difficulty)
        {
            for (int z = 0; z < GlobalVariables.BattleTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BattleTileGridSize; x++)
                {
                    if (z > - x + GlobalVariables.BattleTileMinZIntercept && z < - x + GlobalVariables.BattleTileMaxZIntercept)
                    {
                        float xCoord = CalculateXcoord(x, z) + GlobalVariables.BattleAreaOriginCoord;
                        float zCoord = CalculateZcoord(z) + GlobalVariables.BattleAreaOriginCoord;
                        if (Random.Range(difficulty, 5) < 4)
                        {
                            BattleTileGrid[x, z] = ObjectPoolManager.Instance.TileObjectPool.PopBattleTile(itemType);
                        }
                        else
                        {
                            BattleTileGrid[x, z] = ObjectPoolManager.Instance.TileObjectPool.PopBattleObstacleTile(itemType);
                        }
                        BattleTileGrid[x, z].transform.position = new Vector3(xCoord, 0.05f, zCoord);
                        BattleTileGrid[x, z].GetComponent<BattleTile>().TileCoordinate = new Coordinate(x, z);
                    }
                }
            }
        }

        public void ShowBoardTile()
        {
            for (int z = 0; z < GlobalVariables.BoardTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BoardTileGridSize; x++)
                {
                    if (z > -x + GlobalVariables.BoardTileMinZIntercept && z < -x + GlobalVariables.BoardTileMaxZIntercept)
                    {
                        BoardTileGrid[x, z].SetActive(true);
                    }
                }
            }
        }

        public void ShowBattleTile()
        {
            for (int z = 0; z < GlobalVariables.BattleTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BattleTileGridSize; x++)
                {
                    if (z > -x + GlobalVariables.BattleTileMinZIntercept && z < -x + GlobalVariables.BattleTileMaxZIntercept)
                    {
                        BattleTileGrid[x, z].SetActive(true);
                    }
                }
            }
        }

        public void ClearBattleTileGrid()
        {

        }

        public float CalculateXcoord(float x, float z)
        {
            return (x + z * 0.5f) * 1.74f;
        }

        public float CalculateZcoord(float z)
        {
            return z * 1.5f + 0.1f;
        }

        public void SetBossTileField()
        {
            int midPos = (int)(GlobalVariables.BoardTileGridSize * 0.5f);
            BoardTileGrid[midPos, midPos].GetComponent<BoardTile>().SetBossTile();
        }
    }
}

