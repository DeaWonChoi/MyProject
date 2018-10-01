using RedTheSettlers.GameSystem;
using RedTheSettlers.Users;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 박준명, 강다희
    /// AI들의 Turn 내용 표시.
    /// </summary>
    public class UIAITurnTest : MonoBehaviour
    {
        [SerializeField]
        private Image aiPlayerImage;
        [SerializeField]
        private Image turnCircleImage;
        [SerializeField]
        private Text aiPlayerNameText;
        [SerializeField]
        private Text aiTurnContentText;
        [SerializeField]
        private UIPlayerSprite[] spriteArray;

        public Queue<string> ContentStringQueue;
        private WaitForSeconds waitForSeconds;
        private int aiTurnCount = 1;

        IEnumerator ChangeSliderFillAmount()
        {
            ChangeAiPlayerNameText(aiTurnCount);
            turnCircleImage.fillAmount = 0;
            while (turnCircleImage.fillAmount < 1)
            {
                //turnCircleImage.fillAmount += 0.002f; 기존
                if (turnCircleImage.fillAmount == 0)
                    ChangeAiPlayerImage(aiTurnCount, 0);
                else if (turnCircleImage.fillAmount > 0.3f && turnCircleImage.fillAmount < 0.6f)
                    ChangeAiPlayerImage(aiTurnCount, 1);
                else if (turnCircleImage.fillAmount > 0.6f)
                    ChangeAiPlayerImage(aiTurnCount, 2);

                turnCircleImage.fillAmount += 0.002f;
                yield return null;
            }
            aiTurnCount++;
            Debug.Log("Exit");
            if (aiTurnCount < GlobalVariables.MaxPlayerNumber)
            {
                StartCoroutine(ChangeSliderFillAmount());
                StartCoroutine(ChangeAiTurnContentText());
            }
            else
            {
                aiTurnCount = 1;
                UIManager.Instance.CoverAiTurnUI();
            }
        }

        IEnumerator ChangeAiTurnContentText()
        {
            int count = 0;
            while (count < 3)
            {
                aiTurnContentText.text = ContentStringQueue.Dequeue();
                count++;
                yield return waitForSeconds;
            }
        }

        private void OnEnable()
        {
            StartCoroutine(ChangeSliderFillAmount());
            waitForSeconds = new WaitForSeconds(2.7f);
            ContentStringQueue = new Queue<string>();
            ContentStringQueue.Enqueue("타일을 탐색중 입니다...");
            ContentStringQueue.Enqueue("Cow 타일을 점령중 입니다...");
            ContentStringQueue.Enqueue("Cow 타일을 점령하였습니다!.");
            ContentStringQueue.Enqueue("타일을 탐색중 입니다...");
            ContentStringQueue.Enqueue("Iron 타일을 점령중 입니다...");
            ContentStringQueue.Enqueue("Iron 타일을 점령하였습니다!.");
            ContentStringQueue.Enqueue("타일을 탐색중 입니다...");
            ContentStringQueue.Enqueue("Water 타일을 점령중 입니다...");
            ContentStringQueue.Enqueue("Water 타일을 점령하였습니다!.");
            StartCoroutine(ChangeAiTurnContentText());
        }

        private void ChangeAiPlayerNameText(int turnCount)
        {
            switch (turnCount)
            {
                case 1:
                    aiPlayerNameText.text = "First AI";
                    aiPlayerNameText.color = new Color32(219, 31, 31, 255);
                    break;
                case 2:
                    aiPlayerNameText.text = "Second AI";
                    aiPlayerNameText.color = new Color32(31, 79, 219, 255);
                    break;
                case 3:
                    aiPlayerNameText.text = "Third AI";
                    aiPlayerNameText.color = new Color32(79, 146, 25, 255);
                    break;
            }
        }

        private void ChangeAiPlayerImage(int turnCount, int imageCount)
        {
            int index;
            switch (turnCount)
            {
                case 1:
                    index = imageCount;
                    aiPlayerImage.sprite = spriteArray[index].UISprite;
                    break;
                case 2:
                    index = imageCount + 3;
                    aiPlayerImage.sprite = spriteArray[index].UISprite;
                    break;
                case 3:
                    index = imageCount + 6;
                    aiPlayerImage.sprite = spriteArray[index].UISprite;
                    break;
            }
        }

    }
}
