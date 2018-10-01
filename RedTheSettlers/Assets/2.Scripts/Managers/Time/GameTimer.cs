using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public delegate void TimerCallback();
    /// <summary>
    /// 타이머 클래스, 콜백을 지정 해줘야 합니다.
    /// 담당자 : 최대원
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        [SerializeField]
        private float snoozeTime;
        [SerializeField]
        private float elapseTime;
        private bool isCounting = false;
        private bool isRepeat = false;
        private TimerCallback _callback;
        public TimerCallback Callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

        /// <summary>
        /// 타이머에 시간을 지정합니다. time : 시간, repeat : 반복 호출 여부
        /// </summary>
        public void SetTimer(float time, bool repeat)
        {
            snoozeTime = time;
            isRepeat = repeat;
        }

        /// <summary>
        /// 타이머를 처음부터 작동시킵니다.
        /// </summary>    
        public void StartTimer()
        {
            if (_callback != null)
            {
                isCounting = true;
                elapseTime = 0f;
            }
            else
            {
                LogManager.Instance.UserDebug(LogColor.Purple, GetType().ToString(), "(test)Callback is null");
            }

        }

        /// <summary>
        /// 타이머를 일시정시 시킵니다. 다시 호출하면 재개됩니다.
        /// </summary>
        public void SleepTimer()
        {
            isCounting = !isCounting;
        }

        /// <summary>
        /// 타이머를 정지시킵니다. 시간이 초기화됩니다.
        /// </summary>
        public void StopTimer()
        {
            isCounting = false;
            elapseTime = 0f;
        }

        private void Update()
        {
            if (isCounting)
            {
                if (elapseTime < snoozeTime)
                {
                    elapseTime += GameTimeManager.Instance.DeltaTime;
                }
                else
                {
                    //알람이 울렸다.
                    _callback();
                    if (isRepeat)
                    {
                        StartTimer();
                    }
                    else
                    {
                        StopTimer();
                        GameTimeManager.Instance.PushTimer(this);
                    }
                }
            }
        }

    }
}