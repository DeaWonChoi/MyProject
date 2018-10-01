using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Users;

namespace RedTheSettlers.GameSystem
{
    /// <summary>
    /// 작성자 : 박지용
    /// 유저와 AI의 턴 흐름을 제어한다.
    /// </summary>
    public class PlayerTurnController : MonoBehaviour
    {
        /// <summary>
        /// 현재 턴 진행중인 플레이어 번호(0 ~ 3)
        /// </summary>
        private int CurrentPlayerTurn;
        [SerializeField]
        private BoardAI[] boardAIs;

        private FlowFinishCallback _callback;
        public FlowFinishCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public void TurnFlow(GameState gameState)
        {
            if (gameState == GameState.PlayerTurn)
            {
                CurrentPlayerTurn = 0;
                // UI에서 플레이어가 타일을 이동하고 전투를 시작하면 gameManager에서 diffcultyController 호출
                // diffcultyController가 끝나면 battleFlow 호출
            }
            else // enum 3, 4, 5
            {
                CurrentPlayerTurn = (gameState - GameState.PlayerTurn);
                boardAIs[CurrentPlayerTurn - 1].FindOptimizedPath();
                Callback();
            }
        }

        /// <summary>
        /// AI의 턴에서 게임 화면에 게임 진행 상황을 표시하는 텍스트를 전달한다.
        /// </summary>
        public void SendGameLog()
        {
            if (CurrentPlayerTurn >= 1) // ai 턴일때, CurrentPlayerTurn 값이 1 ~ 3
            {
                Queue<string> messages = boardAIs[CurrentPlayerTurn - 1].MessageQueue;
                GameManager.Instance.SetGameLog(messages);
            }
        }

        public void SetAIs(User[] players)
        {
            for (int i = 0; i < boardAIs.Length; i++)
            {
                boardAIs[i] = players[i + 1].GetComponent<BoardAI>();
                boardAIs[i].AITurnEndCallBack = SendGameLog;
            }
        }

        /// <summary>
        /// UI에서 플레이어 턴을 종료할 때, Callback으로 게임 매니저에게 알리는 함수
        /// </summary>
        public void OnClickTurnFinish()
        {
            Callback();
        }
    }
}