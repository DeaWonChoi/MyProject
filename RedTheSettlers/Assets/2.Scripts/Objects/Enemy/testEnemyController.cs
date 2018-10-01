using UnityEngine;
using RedTheSettlers.Enemys;

namespace RedTheSettlers.UnitTest
{
    /// <summary>
    /// **********************************************
    /// enemy 이동 테스트를 위한 테스트옹 소스입니다.
    /// **********************************************
    /// </summary>
    public class testEnemyController : MonoBehaviour
    {
        [SerializeField]
        Enemy enemy;

        void Update()
        {
            if (Input.GetMouseButtonUp(0) && GUIUtility.hotControl == 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 100f))
                {
                    Debug.Log("hit point : " + hitInfo.point);
                    enemy.destinationPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                    enemy.ChangeState(EnemyStateType.Move);
                }
            }
        }
    }
}

