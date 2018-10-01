using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class PlayerPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        public GameObject GetPlayerObject()
        {
            return player;
        }

        public void SetPlayerObject(GameObject player)
        {
            this.player = player;
        }
    }
}
