using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.UnitTest
{
    public delegate void BattleCallback();

    /// <summary>
    /// 작성자 : 박지용
    /// 전투 화면에서의 시스템 처리를 담당한다.
    /// </summary>
    public class BattleControllerTest : MonoBehaviour
    {
        private BattleCallback _callback;
        public BattleCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public IEnumerator BattleFlow()
        {
            Callback();
            yield return new WaitForSeconds(3);
        }
    }
}