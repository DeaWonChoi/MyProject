using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class ItemDistributor : MonoBehaviour
    {
        private EventWeathers weather;
        private Weather weatherEvnet;

        private FlowFinishCallback _callback;
        public FlowFinishCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        private void Start()
        {
            weatherEvnet = (Weather)GameManager.Instance.gameData.InGameData.Weather;
        }

        public void ItemDistributeFlow()
        {
            ChangeWeather();

            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "자원 분배 시작");
            weather.GetItems();
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "자원 분배 종료");
        }

        private void ChangeWeather()
        {
            LogManager.Instance.UserDebug(LogColor.Orange, GetType().ToString(), "날씨 이벤트 변경");
            switch (weatherEvnet)
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