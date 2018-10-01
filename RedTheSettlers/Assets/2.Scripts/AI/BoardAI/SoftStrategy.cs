using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Users
{
    public class SoftStrategy : MonoBehaviour, IAIStrategy
    {
        public BoardTile CalculateTileWeight(BoardTile boardTile, ItemData[] itemData)
        {
            int[] coordX = { 1, 1, 0, -1, -1, 0 };
            int[] coordZ = { 0, -1, -1, 0, 1, 1 };

            BoardTile targetBoardTile = null;

            for (int i = 0; i < 6; i++)
            {
                BoardTile comparerTile = TileManager.Instance.BoardTileGrid[boardTile.TileCoordinate.x + coordX[i], boardTile.TileCoordinate.z + coordZ[i]].GetComponent<BoardTile>();

                if (comparerTile.tileOwner != TileOwner.None)
                {
                    continue;
                }

                comparerTile.CalculateTileWeight(itemData);

                if (targetBoardTile == null)
                {
                    targetBoardTile = comparerTile;
                }
                else
                {
                    targetBoardTile = (targetBoardTile.tileWeight <= comparerTile.tileWeight) ? targetBoardTile : comparerTile;
                }
            }

            return targetBoardTile;
        }
    }
}
