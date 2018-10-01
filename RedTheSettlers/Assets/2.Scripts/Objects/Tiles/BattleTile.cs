using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Tiles
{
    public class BattleTile : Tile {

        [HideInInspector]
        public int ParentTileXCoord;
        [HideInInspector]
        public int ParentTileZCoord;
        [HideInInspector]
        public int f;
        [HideInInspector]
        public int g;
        [HideInInspector]
        public int h;
        [HideInInspector]
        public bool isWall;
    }
}