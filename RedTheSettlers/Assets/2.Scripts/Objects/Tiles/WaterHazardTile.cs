using UnityEngine;
using RedTheSettlers.Players;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.Tiles
{
    public class WaterHazardTile : BattleTile
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(GlobalVariables.TAG_PLAYER))
            {
                collision.gameObject.GetComponent<BattlePlayer>().SlowDownSpeedByWater();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(GlobalVariables.TAG_PLAYER))
            {
                collision.gameObject.GetComponent<BattlePlayer>().RecoverySpeed();
            }
        }
    }
}
