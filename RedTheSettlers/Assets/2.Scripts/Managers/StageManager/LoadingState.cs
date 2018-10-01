using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 담당자 : 이재환
/// 수정시 간략 설명과 수정 날짜 
/// {
///   Ex : 함수명 변경 18/07/15
///    
///   
/// }
/// </summary>


namespace RedTheSettlers.GameSystem
{
    class LoadingState : State
    {

        public override void ContinueGame(bool canLoadData)
        {
            Debug.Log("로딩씬");
        }

        public override void Enter(StageType stageType)
        {
            SoundManager.Instance.ChangeBGM("BGM_PlayerTurn", true);
            StageManager.Instance.StartCoroutine(StageManager.Instance.ChangeStageLoad(StageType.LoadingScene));
        }

        public override void Exit(StageType stageType)
        {

        }

    }

}
