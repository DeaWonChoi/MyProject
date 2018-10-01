using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 담당자 : 이재환
/// 수정시 간략 설명과 수정 날짜 
/// {
///   Ex : 함수명 변경 18/07/15
///   Client 부분 
///   
/// }
/// </summary>

namespace RedTheSettlers.GameSystem
{

    public enum StageType
    {
        TitleScene,
        LoadingScene,
        BoardScene,
        TutorialStageState,
        BattleStageState

    }

    public class StageManager : Singleton<StageManager>
    {

        [SerializeField]
        private StageStateMachine stageStateMachine;
        public StageStateMachine StageStateMachine { get { return stageStateMachine; } }



        private void Awake()
        {
            stageStateMachine = new StageStateMachine();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ChangeState(StageType.TitleScene);
        }


        public void JudgeLoadingData(bool canLoadData, StageType stageType)
        {
            StageStateMachine.ContinueGame(canLoadData);
            ChangeState(stageType);
            ChangeStage(stageType);
        }

        public void ChangeState(StageType stageType)
        {
            stageStateMachine.Enter(stageType);
        }

        public void ChangeStage(StageType stageType)
        {
            StageStateMachine.Exit(stageType);
        }


        public IEnumerator ChangeStageLoad(StageType stageType)
        {
            AsyncOperation asyncOperationLoad = SceneManager.LoadSceneAsync(stageType.ToString());

            asyncOperationLoad.allowSceneActivation = false;

            while (!asyncOperationLoad.isDone)
            {
                yield return new WaitForSeconds(0.5f);

                if (asyncOperationLoad.progress >= 0.9f)
                    asyncOperationLoad.allowSceneActivation = true;

            }

            AsyncOperation asyncOperationMain = SceneManager.LoadSceneAsync(StageType.BoardScene.ToString());

            asyncOperationMain.allowSceneActivation = false;

            while (!asyncOperationMain.isDone)
            {
                yield return new WaitForSeconds(0.5f);

                if (asyncOperationMain.progress >= 0.9f)
                    asyncOperationMain.allowSceneActivation = true;
            }

            Debug.Log("boardScene진입하는 ChangeState(StageType.BoardScene);");
            ChangeState(StageType.BoardScene);
        }
    }
}
