using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RedTheSettlers.GameSystem;
using System.Linq;

namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 김하정
    /// 우승자한테 왕관 아이콘을 보여준다.
    /// </summary>
    public class UIShowWinnerIcon : MonoBehaviour
    {
        [SerializeField]
        private Image[] winnerImage = new Image[4];
        private float iconDelayTime = 8f;

        void Start()
        {
            StartCoroutine(ShowWinnerIcon());
        }

        IEnumerator ShowWinnerIcon()
        {
            yield return new WaitForSeconds(iconDelayTime);
            
            if (winnerImage.Length == GlobalVariables.MaxPlayerNumber)
            {
                int tempScore = 0;
                List<int> TotalScores = new List<int>();
                for (int i = 0; i < winnerImage.Length; i++)
                {
                    tempScore += i;
                    //     GameManager.Instance.GetPlayerItemCountAll((UserType)i)
                    //* (GlobalVariables.CardWeightValue + GlobalVariables.EquipmentWeightValue + GlobalVariables.BonusWeightValue);
                    TotalScores.Add(i);
                }
                int winnerIndex = TotalScores.IndexOf(TotalScores.Max());
               winnerImage[winnerIndex].gameObject.SetActive(true);
            }
            else
                LogManager.Instance.UserDebug(LogColor.Red, GetType().Name, "작업자님 인스펙터 창에 winnerImage를 확인해주세요. 배열의 크기가 플레이어 인원보다 크거나 작습니다.");
           
        }
    }
}
