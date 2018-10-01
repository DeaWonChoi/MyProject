using RedTheSettlers.Enemys;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindCollider : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GlobalVariables.TAG_PLAYER)
        {
            enemy.TargetObject = other.GetComponent<BattlePlayer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GlobalVariables.TAG_PLAYER)
        {
            enemy.TargetObject = null;
        }
    }
}
