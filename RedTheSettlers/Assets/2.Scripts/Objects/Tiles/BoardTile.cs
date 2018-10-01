using System;
using System.Collections.Generic;
using RedTheSettlers.GameSystem;
using UnityEngine;

namespace RedTheSettlers.Tiles
{
    public class BoardTile : Tile, IComparable<BoardTile>
    {
        public TileOwner tileOwner;
        [HideInInspector]
        public int tileWeight;
        public bool IsBossTile = false;

        public void CalculateTileWeight(ItemData[] itemData)
        {
            int userResourceAmount;

            userResourceAmount = itemData[(int)TileType].Count;

            tileWeight = userResourceAmount;
        }

        public int CompareTo(BoardTile boardTile)
        {
            return tileWeight.CompareTo(boardTile.tileWeight);
        }

        public void SetBossTile()
        {
            IsBossTile = true;
        }

        public void SetNormalTile()
        {
            IsBossTile = false;
        }
    }
}
