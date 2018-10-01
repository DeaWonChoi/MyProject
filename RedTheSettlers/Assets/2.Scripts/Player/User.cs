using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Tiles;
using RedTheSettlers.GameSystem;
using RedTheSettlers.UnitTest;

namespace RedTheSettlers.Users
{
    public abstract class User : MonoBehaviour
    {
        public Tile tileSteppingOn;
        public List<Tile> PossessingTile;
        public ItemData[] inventory;

        public abstract void ChangeItemCount(ItemData[] itemList);

        private void Start()
        {
            for(int i = 0; i < GlobalVariables.MaxItemNumber; i++)
            {
                inventory[i] = new ItemData();
                inventory[i].ItemType = (ItemType)i;
                inventory[i].Count = i;
            }
        }
    }
}
