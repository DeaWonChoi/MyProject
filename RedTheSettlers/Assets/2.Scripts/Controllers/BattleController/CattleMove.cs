using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class CattleMove : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        
        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}