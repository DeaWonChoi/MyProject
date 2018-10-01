using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;

namespace RedTheSettlers.UnitTest
{
    /// <summary>
    /// 작성자 : 박지용
    /// 날씨 상태에 따른 자원 획득을 담당하는 컨트롤러
    /// </summary>
    public class ItemControllerTest2 : MonoBehaviour
    {
        WeatherChange weather;
        GameData datas = DataManager.Instance.GameData;

        public IEnumerator ItemDistributeFlow()
        {
            ChangeWeather();
            weather.GetItems();

            yield break;
        }

        public void ChangeWeather()
        {
            switch((Weather)datas.InGameData.Weather)
            {
                case Weather.Rain:
                    weather = new Rain(); break;

                case Weather.Drought:
                    weather = new Drought(); break;

                case Weather.RichYear:
                    weather = new RichYear(); break;

                case Weather.SwarmOfLocusts:
                    weather = new SwarmOfLocusts(); break;

                case Weather.BreedingSeason:
                    weather = new BreedingSeason(); break;

                case Weather.Plague:
                    weather = new Plague(); break;

                case Weather.FestivalOfSprits:
                    weather = new FestivalOfSprits(); break;

                case Weather.ForestFire:
                    weather = new ForestFire(); break;

                case Weather.GoldMine:
                    weather = new GoldMine(); break;

                case Weather.LandSlide:
                    weather = new LandSlide(); break;

                case Weather.GoodSoil:
                    weather = new GoodSoil(); break;

                case Weather.Deluge:
                    weather = new Deluge(); break;

                default: LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "존재하지 않는 날씨 상태입니다."); break;
            }
        }
    }
}