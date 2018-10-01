using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using UnityEngine;

namespace RedTheSettlers.Enemys
{
    public class Move : EnemyState
    {
        const float speedCorrection = 50f;

        public Move(Animator animator, Transform transform, Rigidbody rigidbodyComponent, 
            Vector3 destinationPoint, Vector3 currentPoint,  float moveSpeed, Tile currentTile)
        {
            this.animator = animator;
            this.transform = transform;
            this.rigidbodyComponent = rigidbodyComponent;
            this.destinationPoint = destinationPoint;
            this.currentPoint = currentPoint;
            this.moveSpeed = moveSpeed;
            this.currentTile = currentTile;
        }

        public override void DoAction()
        {
            animator.SetBool("IsMoving", true);
            Vector3 normalVector = (destinationPoint - currentPoint).normalized;
            transform.rotation = Quaternion.LookRotation(normalVector);
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0, transform.rotation.w);
            rigidbodyComponent.velocity = normalVector * GameTimeManager.Instance.DeltaTime * moveSpeed * speedCorrection;
        }
    }
}