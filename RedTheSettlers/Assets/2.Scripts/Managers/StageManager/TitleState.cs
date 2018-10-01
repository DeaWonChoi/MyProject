using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 담당자 : 이재환
/// 수정시 간략 설명과 수정 날짜 
/// {
///   Ex : 함수명 변경 18/07/15
///     
/// }
/// </summary>

namespace RedTheSettlers.GameSystem
{
    class TitleState : State
    {

        public override void ContinueGame(bool canLoadData)
        {
            if (!canLoadData)
            {
                DataManager.Instance.ResetData();
            }
            else
                Debug.Log("현재 상태가 없습니다.");
        }

        public override void Enter(StageType stageType)
        {
            Debug.Log("타이틀스테이트 Enter");
            SoundManager.Instance.ChangeBGM("BGM_Title", true);
        }

        public override void Exit(StageType stageType)
        {
            //StageManager.Instance.StartCoroutine(StageManager.Instance.ChangeStageLoad(StageType.LoadingScene));
        }
    }
}

