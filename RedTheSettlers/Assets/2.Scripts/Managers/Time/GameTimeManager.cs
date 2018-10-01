using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임의 시간을 관리하는 기반 클래스
/// 담당자 : 최대원
/// </summary>
namespace RedTheSettlers.GameSystem
{
    public class GameTimeManager : Singleton<GameTimeManager>
    {
        protected GameTimeManager() { }

        public float TimeScale;
        public float DeltaTime;
        public float TimeSceneLoad
        {
            get
            {
                return Time.timeSinceLevelLoad;
            }
        }
        public float GameTime
        {
            get
            {
                return Time.time;
            }
        }
        private float startTime;
        private float fixedDeltaTime;

        private Stack<GameTimer> TimerStack;
        [SerializeField]
        private GameTimer gameTimerPrefab;
        const int timerAmount = 10;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            startTime = Time.realtimeSinceStartup;
            fixedDeltaTime = Time.fixedDeltaTime;
            TimeScale = 1f;

            TimerStack = new Stack<GameTimer>();
            for (int i = 0; i < timerAmount; i++)
            {
                GameTimer gametimer = Instantiate(gameTimerPrefab, transform);
                TimerStack.Push(gametimer);
                gametimer.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            DeltaTime = Time.realtimeSinceStartup - startTime;
            startTime = Time.realtimeSinceStartup;

            Time.fixedDeltaTime = fixedDeltaTime * TimeScale;
        }

        /// <summary>
        /// 타이머를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public GameTimer PopTimer()
        {
            GameTimer timer = TimerStack.Pop();
            timer.gameObject.SetActive(true);
            return timer;
        }

        /// <summary>
        /// 다 사용한 타이머를 집어 넣습니다.
        /// </summary>
        /// <param name="timer"></param>
        public void PushTimer(GameTimer timer)
        {
            timer.SetTimer(0f, false);
            timer.StopTimer();
            TimerStack.Push(timer);
            timer.gameObject.SetActive(false);
        }
    }
}
