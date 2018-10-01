using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.UnitTest
{
    public delegate void EventCallback();

    /// <summary>
    /// 작성자 : 박지용
    /// 날씨 선택이나 보스 출현 등 보드게임에서 발생하는 이벤트를 제어한다.
    /// </summary>
    public class EventControllerTest : MonoBehaviour
    {
        private EventCallback _callback;
        public EventCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public IEnumerator EventFlow()
        {
            Callback();
            yield return new WaitForSeconds(3);
        }
    }
}