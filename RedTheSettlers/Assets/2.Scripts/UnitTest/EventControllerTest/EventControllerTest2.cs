using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;
using RedTheSettlers.UI;

namespace RedTheSettlers.UnitTest
{
    /// <summary>
    /// 작성자 : 박지용
    /// 날씨 선택이나 보스 출현 등 보드게임에서 발생하는 이벤트를 제어한다.
    /// </summary>
    public class EventControllerTest2 : MonoBehaviour
    {
        GameData datas;

        private void Start()
        {
            datas = DataManager.Instance.GameData;

        }

        public void EventFlow()
        {
            int turnCount = GetTurnCount();

            if (turnCount == GlobalVariables.MiddleBoss1AppearTurn) AppearMiddleBoss1();
            else if (turnCount == GlobalVariables.MiddleBoss2AppearTurn) AppearMiddleBoss2();
            else if (turnCount == GlobalVariables.BossAppearTurn) AppearBoss();

            if(turnCount >= GlobalVariables.WeatherEventStartTurn)
            {
                int playerNumber = GetLowestPlayer();
                int[] weathers = PickWeatherEvent();

                if (playerNumber == 1)
                {
                    GameManager.Instance.SendWeatherCard(weathers);
                }
                else
                {
                    // AI의 날씨 선택
                }
                //QualifyWeatherSelect(playerNumber);
            }

            //yield return new WaitForSeconds(3);
        }

        public int GetTurnCount()
        {
            return datas.InGameData.TurnCount;
        }

        //private void QualifyWeatherSelect(int playerNumber)
        //{
        //    // 해당 플레이어에게 선택 패널을 띄워서 보여준다. >> UI에서 처리

        //    int selectedWeather = 0;
        //    datas.InGameData.Weather = selectedWeather;
        //}

        //public int[] PickWeatherEvent()
        //{
        //    int[] weathers = { -1, -1, -1 };
        //    int pickedNumber;

        //    for (int i = 0; i < weathers.Length; i++)
        //    {
        //        pickedNumber = Random.Range(0, (int)Weather.Count);

        //        if (CheckDuplication(weathers, pickedNumber))
        //            weathers[i] = pickedNumber;
        //        else i--;
        //    }
        //    return weathers;
        //}

        //private bool CheckDuplication(int[] weathers, int pickedNumber)
        //{
        //    for (int i = 0; i < weathers.Length; i++)
        //    {
        //        if (weathers[i] == pickedNumber)
        //            return false;
        //    }
        //    return true;
        //}

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
            return weathers;
        }

        private int GetLowestPlayer()
        {
            int lowestPlayerNumber = 0;
            int tempCampCount = GlobalVariables.MaxTileCount;

            for (int i = 0; i < GlobalVariables.MaxPlayerNumber; i++)
            {
                // 캠프 수가 같으면, 자원 수가 적은 사람이 우선권, 그것도 같으면 번호순
                if (tempCampCount > datas.PlayerData[i].TileList.Count) // 수정
                {
                    lowestPlayerNumber = i;
                }
                else if(tempCampCount == datas.PlayerData[i].TileList.Count) // 수정
                {
                    if (GameManager.Instance.GetPlayerItemCountAll((UserType)lowestPlayerNumber) > GameManager.Instance.GetPlayerItemCountAll((UserType)i))
                        lowestPlayerNumber = i;
                }
            }
            return lowestPlayerNumber;
        }

        private void AppearMiddleBoss1()
        {
            Debug.Log("중간보스 1 등장");
        }

        private void AppearMiddleBoss2()
        {
            Debug.Log("중간보스 2 등장");
        }

        private void AppearBoss()
        {
            Debug.Log("보스 등장");
        }

    }
}
