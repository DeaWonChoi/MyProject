using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 담당자 : 이재환
/// 수정시 간략 설명과 수정 날짜 
/// {
///   Ex : 함수명 변경 18/07/15
///   Context 부분 
///   
/// }
/// </summary>


namespace RedTheSettlers.GameSystem
{
    public class StageStateMachine
    {
        private State currentState;
        public State CurrentState { get { return currentState; } }

       

        public void ContinueGame(bool canLoadData)
        {
            if (currentState != null) currentState.ContinueGame(canLoadData);
            else
                LogManager.Instance.UserDebug(LogColor.Navy, "StageStateMachine", "현재 상태가 없습니다.");
        }

        public void Enter(StageType stageType)
        {
            switch (stageType)
            {
                case StageType.BattleStageState:
                    currentState = new BattleState();
                    break;
                case StageType.BoardScene:
                    currentState = new MainState();
                    break;
                case StageType.LoadingScene:
                    currentState = new LoadingState();
                    break;
                case StageType.TitleScene:
                    currentState = new TitleState();
                    break;
                case StageType.TutorialStageState:
                    currentState = new TutorialState();
                    break;
                default:
                    break;
            }
            if (currentState != null)
            {
                Debug.Log("상태머신에서 엔터" + stageType);
                currentState.Enter(stageType);
            }
            else
                LogManager.Instance.UserDebug(LogColor.Navy, "StageStateMachine", "현재 상태가 없습니다.");
        }

        public void Exit(StageType stageType)
        {
            if (currentState != null) currentState.Exit(stageType);
            else
                LogManager.Instance.UserDebug(LogColor.Navy, "StageStateMachine", "현재 상태가 없습니다.");
        }
    }
}

