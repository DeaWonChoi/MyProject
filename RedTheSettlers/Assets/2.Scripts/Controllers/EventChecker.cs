using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class EventChecker : MonoBehaviour
    {
        private FlowFinishCallback _callback;
        public FlowFinishCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        public void EventFlow()
        {
            int turnCount = GetTurnCount();

            if (turnCount == GlobalVariables.MiddleBoss1AppearTurn) AppearMiddleBoss1();
            else if (turnCount == GlobalVariables.MiddleBoss2AppearTurn) AppearMiddleBoss2();
            else if (turnCount == GlobalVariables.BossAppearTurn) AppearBoss();

            if (turnCount >= GlobalVariables.WeatherEventStartTurn)
            {
                int playerNumber = GetLowestPlayer();
                int[] weathers = PickWeatherEvent();

                if (playerNumber == 1)
                {
                    GameManager.Instance.SendWeatherCard(weathers);
                }
                else // AI의 날씨 선택
                {
                    int aiPickedWeather = Random.Range(0, 3);
                    LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "AI가 선택한 날씨 : " + aiPickedWeather);
                    GameManager.Instance.SetWeatherEventNumber(weathers[aiPickedWeather]);
                }
            }
        }

        private int GetTurnCount()
        {
            int turnCount = GameManager.Instance.gameData.InGameData.TurnCount;
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "현재 턴 : " + turnCount);
            return turnCount;
        }

        /// <summary>
        /// 12개의 날씨 이벤트 중 3가지를 뽑아 반환한다.
        /// </summary>
        /// <returns>3의 길이를 갖는 int형 배열</returns>
        public int[] PickWeatherEvent()
        {
            int[] weathers = new int[3];
            int pickedNumber;
            int[] weatherList = new int[(int)Weather.Count];

            for (int i = 0; i < weathers.Length; i++)
            {
                pickedNumber = Random.Range(0, (int)Weather.Count);

                if (weatherList[pickedNumber] != 1)
                {
                    weathers[i] = pickedNumber;
                    weatherList[pickedNumber] = 1;
                }
                else i--;
            }

            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "뽑힌 날씨 : " + weathers[0] + ", " + weathers[1] + ", " + weathers[2]);
            return weathers;
        }

        private int GetLowestPlayer()
        {
            int lowestPlayerNumber = 0;
            int tempCampCount = GlobalVariables.MaxTileCount;

            for (int i = 0; i < GlobalVariables.MaxPlayerNumber; i++)
            {
                // 캠프 수가 같으면, 자원 수가 적은 사람이 우선권, 그것도 같으면 번호순
                if (tempCampCount > GameManager.Instance.GetPlayerTileCountAll((UserType)i)) // 수정
                {
                    lowestPlayerNumber = i;
                }
                else if (tempCampCount == GameManager.Instance.GetPlayerTileCountAll((UserType)i)) // 수정
                {
                    if (GameManager.Instance.GetPlayerItemCountAll((UserType)lowestPlayerNumber) > GameManager.Instance.GetPlayerItemCountAll((UserType)i))
                        lowestPlayerNumber = i;
                }
            }

            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "선정된 플레이어 : " + lowestPlayerNumber);
            return lowestPlayerNumber;
        }

        private void AppearMiddleBoss1()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "중간보스 1 등장");
        }

        private void AppearMiddleBoss2()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "중간보스 2 등장");
        }

        private void AppearBoss()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "보스 등장");
        }
    }
}