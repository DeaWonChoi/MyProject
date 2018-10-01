using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.Tiles
{
    public class WheatBushTile : BattleTile
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(GlobalVariables.TAG_ENEMY))
            {
                collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(GlobalVariables.TAG_ENEMY))
            {
                collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            }
        }
    }
}
