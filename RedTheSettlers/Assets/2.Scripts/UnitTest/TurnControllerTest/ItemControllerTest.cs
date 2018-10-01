using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.UnitTest
{
    public delegate void ItemCallback();

    /// <summary>
    /// 작성자 : 박지용
    /// 날씨 상태에 따른 각 플레이어의 자원 획득을 제어한다.
    /// </summary>
    public class ItemControllerTest : MonoBehaviour
    {
        private ItemCallback _callback;
        public ItemCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public IEnumerator ItemDistributeFlow()
        {
            Callback();
            yield return new WaitForSeconds(3);
        }
    }
}