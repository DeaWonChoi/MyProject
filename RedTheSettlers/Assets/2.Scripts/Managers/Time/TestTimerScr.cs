using RedTheSettlers.GameSystem;
using UnityEngine;

namespace RedTheSettlers.UnitTest
{ 
    public class TestTimerScr : MonoBehaviour
    {
        [SerializeField]
        GameTimer timer;

        GameObject skill_1_Obj;
        GameTimer skill_1_Timer;
        GameObject skill_2_Obj;
        GameTimer skill_2_Timer;

        int count = 0;

        private void Start()
        {
            skill_1_Obj = Instantiate(timer.gameObject);
            skill_1_Obj.transform.parent = transform;
            skill_1_Timer = skill_1_Obj.GetComponent<GameTimer>();
            skill_1_Timer.SetTimer(1.0f, true);
            skill_1_Timer.Callback = new TimerCallback(execute);
            skill_1_Timer.StartTimer();
        }

        void execute()
        {
            //Debug.Log("callback execute");
            skill_2_Timer = GameTimeManager.Instance.PopTimer();
            skill_2_Timer.SetTimer(1f, false);
            skill_2_Timer.Callback = new TimerCallback(callback);
            skill_2_Timer.StartTimer();
        }

        void callback()
        {
            Debug.Log("callback execute : " + count);
            count++;
        }
    }
}
