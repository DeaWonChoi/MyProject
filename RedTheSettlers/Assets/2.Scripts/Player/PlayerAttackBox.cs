using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Enemys;

namespace RedTheSettlers.Players
{
    public class PlayerAttackBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GlobalVariables.TAG_ENEMY))
            {
                other.gameObject.GetComponent<Enemy>().Damaged(10);
            }
        }
    }
}
