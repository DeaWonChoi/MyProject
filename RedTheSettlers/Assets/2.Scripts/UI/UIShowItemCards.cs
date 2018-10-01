using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.GameSystem;
using UnityEngine.UI;

namespace RedTheSettlers.UI
{
    public class UIShowItemCards : MonoBehaviour
    {
        [SerializeField]
        private Text[] textInfo = new Text[6];

        private int[] GetPlayerItems = new int[6] ;
         
       
        void Start()
        {
            SetItems();
            AlertArrayLength();
            ShowItemCount();
        }

        private void SetItems()
        {
            for (int i = 0; i < GlobalVariables.MaxItemNumber; i++)
            {
                GetPlayerItems[i] = GameManager.Instance.GetPlayerTileCount(UserType.Player, (ItemType)i);
            }
        }

        private void ShowItemCount()
        {
            for (int i = 0; i < GetPlayerItems.Length; i++)
            {
                textInfo[i].text = GetPlayerItems[i].ToString();
            }
            StartCoroutine(ShowFadeOutText());
        }

        IEnumerator ShowFadeOutText()
        {
            float alpha = 1f;
            while(alpha >= 0)
            {
                alpha -= 0.1f;
                Debug.Log(alpha);
                
                for (int i = 0; i < textInfo.Length; i++)
                {
                    textInfo[i].color = new Color(1f, 0f, 0f, alpha);
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

        private void AlertArrayLength()
        {
            if (textInfo.Length > 6)
            {
                LogManager.Instance.UserDebug(LogColor.Red, GetType().Name, "작업자님 인스펙터 창을 확인해주세요. 배열의 길이가 6개를 초과했습니다.");
            }
        }
    }
}
