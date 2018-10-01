using RedTheSettlers.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Tiles
{
    [System.Serializable]
    public struct Coordinate
    {
        public Coordinate(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public int x;
        public int z;
    }

    [System.Serializable]
    public class Tile : MonoBehaviour
    {
        private const int tileTypeAmount = 6;

        public ItemType TileType;
        [HideInInspector]
        public Coordinate TileCoordinate;
        [HideInInspector]
        public GameObject[] TileBorder = new GameObject[6];
        [HideInInspector]
        public int TileLevel;

        private void Awake()
        {
            for (int i = 0; i < tileTypeAmount; i++)
            {
                TileBorder[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
