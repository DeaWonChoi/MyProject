using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Tiles;
using RedTheSettlers.Players;

/*
 * 카메라 컨트롤러가 해야할일
 * 카메라 스위칭
 * 카메라 안에있는 기능을 실행시키기
*/

namespace RedTheSettlers.GameSystem
{
    public enum CameraStateType
    {
        Idle = 0,
        Damage = 1,
        Dead = 3,
        Skill_1 = 4,
        Skill_2 = 5,
        Skill_3 = 6,
        Skill_4 = 7
    }

    /// <summary>
    /// 카메라컨트롤러 클래스
    /// 담당자 : 정진영
    /// 테스트용 기본조작
    /// 카메라 스위칭 : x
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameCamera BoardGameCamera, BattleGameCamera, ActiveCamera;

        private CameraStateType newCameraState;
        private CameraStateType cameraState;

        private Animator cloudAnimator;

        [SerializeField]
        private Vector3 playerVector3;

        private Transform playerTransform;
        private bool ConsentToSwiching = false;
        //float ZoomValue;
        //Vector3 vector3; //배틀 카메라를 사용할때 비어있는 v3를 전달하기위해 선언함

        public void InitializeCamera()
        {
            cloudAnimator = GameObject.FindWithTag("UICamera").GetComponentInChildren<Animator>();
            BoardGameCamera = GameObject.FindWithTag("BoardCamera").GetComponent<GameCamera>();
            BattleGameCamera = GameObject.FindWithTag("BattleCamera").GetComponent<GameCamera>();
            //playerTransform = GameObject.FindWithTag(GlobalVariables.TAG_PLAYER).transform;
            ActiveCamera = BoardGameCamera;
            cameraState = CameraStateType.Idle;
            //vector3 = new Vector3();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("x");

                if (ActiveCamera == BoardGameCamera)
                {
                    StartCoroutine(SwichingCamera(BattleGameCamera));
                }
                else
                {
                    StartCoroutine(SwichingCamera(BoardGameCamera));
                }
            }
            //if (Input.GetKeyDown(KeyCode.C))
            //{
            //    Debug.Log("c");
            //    ZoomInOut(ZoomValue);//ActiveCamera,
            //}

            //새로운 상태가 있으면 바꿔준다
            if (cameraState != newCameraState)
            {
                cameraState = newCameraState;
            }
        }

        /// <summary>
        /// 배틀카메라를 움직일때 사용함
        /// </summary>
        private void FixedUpdate()
        {
            if (ActiveCamera == BattleGameCamera && ActiveCamera != null && BattleGameCamera != null)
            {
                Debug.Log(ActiveCamera + " " + playerVector3);
                if (playerVector3 == Vector3.zero)
                {
                    GetPlayerVector3();
                    return;
                }
                else
                {
                    ActiveCamera.MovingCamera(playerVector3, cameraState);
                    ActiveCamera.Looking(playerVector3);
                }
            }
        }

        /// <summary>
        /// Player에서 카메라의 상태를 변경함
        /// </summary>
        /// <param name="cameraState"></param>
        public void ChangeState(CameraStateType cameraStateType)
        {
            cameraState = cameraStateType;
        }

        /// <summary>
        /// 카메라 피치 줌인아웃
        /// </summary>
        public void ZoomInOut(float ZoomValue)
        {
            if (ActiveCamera == BoardGameCamera)
                ActiveCamera.ZoomInOutCamera(ZoomValue);
        }

        /// <summary>
        /// 보드에서 드레그로 화면을 움직인다
        /// InputManager에서 호출하게됨
        /// </summary>
        public void CameraDragMoving(Vector3 direction)
        {
            ActiveCamera.MovingCamera(direction, cameraState);
        }

        /// <summary>
        /// 보드타일이 선택되었을때 해당 타일을 바라보며 줌
        /// </summary>
        public void LookingTile(BoardTile boardTile)
        {
            if (ActiveCamera == BoardGameCamera)
                ActiveCamera.Looking(boardTile.gameObject.transform.position);
        }

        //타일선택 해제했을때 뭐로할지 이야기해야함!!!!!!!!!!!
        public void NoLookingTile()
        {
            if (ActiveCamera == BoardGameCamera)
                ActiveCamera.Looking(new Vector3());
        }

        /// <summary>
        /// 플레이어위치를 찾아옴
        /// </summary>
        private void GetPlayerVector3()
        {
            playerVector3 = GameObject.FindWithTag(GlobalVariables.TAG_PLAYER).transform.position;
        }

        /// <summary>
        /// 배틀/보드 카메라 전환
        /// </summary>
        /// <param name="stateType"></param>
        public void ChangeCamera(StateType stateType)
        {
            switch (stateType)
            {
                case StateType.BattleStageState:
                    SwichingCamera(BattleGameCamera);
                    break;

                case StateType.MainStageState:
                    SwichingCamera(BoardGameCamera);
                    break;

                default:
                    break;
            }
        }

        public void GiveConsentToSwiching(bool pass)
        {
            if (pass) ConsentToSwiching = true;
            else ConsentToSwiching = false;
        }

        /// <summary>
        /// 카메라 스위칭할때 애니메이션, 싱크
        /// StageManager와 연관
        /// </summary>
        private IEnumerator SwichingCamera(GameCamera Camera)
        {
            Debug.Log("SwichingCameraasdsa");
            cloudAnimator.SetTrigger("Closing");

            yield return new WaitForSeconds(cloudAnimator.GetCurrentAnimatorStateInfo(0).length);

            ActiveCamera.TrunOffCamera();
            if (Camera == BoardGameCamera)
            {
                playerVector3 = new Vector3();
                ActiveCamera = BoardGameCamera;
            }
            else
            {
                GetPlayerVector3();
                //targetTransform = playerTransform;
                ActiveCamera = BattleGameCamera;
            }
            ActiveCamera.TrunOnCamera();

            //여기에서 배틀타일이 완성되었는지 판단하고 다음을 진행한다
            while (ActiveCamera == BattleGameCamera)
            {
                if (ConsentToSwiching == true)
                {
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
            ConsentToSwiching = false;
            cloudAnimator.SetTrigger("Opening");
            cloudAnimator.SetTrigger("Idle");

            yield return null;
        }
    }
}