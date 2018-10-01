using RedTheSettlers.GameSystem;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 김하정
    /// TradeUI에서 교환을 보여주고 그에따른 수치변화를 보여주는 스크립트
    /// </summary>
    public class UITradeCard : MonoBehaviour
    {
        [SerializeField]
        private GameObject ItemPopup, overItemPopup;

        [SerializeField]
        private GameObject giveGroup, handGroup, takeGroup;

        [SerializeField]
        private Slider ItemSlider;

        [SerializeField]
        private Text SliderValue;

        [SerializeField]
        private Text GiveDescriptionText, TakeDescriptionText;

        [SerializeField]
        private Text[] aIText;

        [SerializeField]
        private GameObject tradePopUp;
        [SerializeField]
        private GameObject tradeSuccessText, tradeNoText;

        private int tradeCardNumber;
        private int giveAndTake = 0;

        const string GIVEPANEL = "GivePanel";
        const string TAKEPANEL = "TakePanel";

        [Serializable]
        struct CardInfo
        {
            public string InspectorName;
            public GameObject ItemsCard;
            public Text TempitemsCount;
        }
        [SerializeField]
        private CardInfo[] cardInfo;
               
        private int[] tradeItemValue = new int[6]
        {0,0,0,0,0,0};  //순서대로 Cow,Iron,Soil,Water,Wheat,Wood

        private int playerNumber = 0;
        private int AiNumber = 0;
        private ItemData[] sendData;
        
        public void CheckCards(int cardNumber)
        {
            GameManager data = GameManager.Instance;
            tradeCardNumber = cardNumber;

            if (cardInfo[cardNumber].ItemsCard.transform.parent.name == GIVEPANEL)
            {
                giveAndTake = -1;
            }
            if (cardInfo[cardNumber].ItemsCard.transform.parent.name == TAKEPANEL)
            {
                giveAndTake = 1;
            }
            ShowItemPopup(giveAndTake);

            if (cardInfo[cardNumber].ItemsCard.transform.parent.name == giveGroup.name)
            {
                ItemPopup.SetActive(true);
                ItemSlider.maxValue = data.GetPlayerItemCount(UserType.Player,(ItemType)cardNumber);
                ItemSlider.value = 0;
            }
            else if (cardInfo[cardNumber].ItemsCard.transform.parent.name == takeGroup.name)    //MaxValue 때문에 나눔
            {
                ItemPopup.SetActive(true);
                ItemSlider.maxValue = GlobalVariables.MaxItemNum;
                ItemSlider.value = 0;
            }
            else      //핸드그룹에 돌아오는 애들을 초기화 시켜줘야함.
            {
                ItemPopup.SetActive(false);
                cardInfo[cardNumber].TempitemsCount.text = "";
                tradeItemValue[cardNumber] = 0;
            }
        }

        public void ShowItemPopup(int value)
        {
            if (value == 1)    
            {
                ItemPopup.SetActive(true);
                GiveDescriptionText.gameObject.SetActive(true);
                TakeDescriptionText.gameObject.SetActive(false);
            }
            else if (value == -1)
            {
                ItemPopup.SetActive(true);
                GiveDescriptionText.gameObject.SetActive(false);
                TakeDescriptionText.gameObject.SetActive(true);
            }
        }

        public void OnClickedPopupButton()
        {
            tradeItemValue[tradeCardNumber] = (int)ItemSlider.value * giveAndTake;
            cardInfo[tradeCardNumber].TempitemsCount.text = Mathf.Abs(tradeItemValue[tradeCardNumber]).ToString();
            CheckTakeCardLimit();
        }

        public void CheckTakeCardLimit()
        {
            float tempTotalvalue = 0;

            for (int i = 0; i < cardInfo.Length; i++)
            {
                tempTotalvalue += tradeItemValue[i];
            }

            if (tempTotalvalue > 50)
            {
                overItemPopup.SetActive(true);
                cardInfo[tradeCardNumber].ItemsCard.transform.SetParent(handGroup.transform);
                cardInfo[tradeCardNumber].TempitemsCount.text = "";
                tradeItemValue[tradeCardNumber] = 0;
            }
        }

        public void ResetCardBoard()
        {
            for (int i = 0; i < cardInfo.Length; i++)
            {
                cardInfo[i].ItemsCard.transform.SetParent(handGroup.transform);
                cardInfo[i].TempitemsCount.text = "";
                tradeItemValue[i] = 0;
            }
            ItemSlider.value = 0;
        }

        public void ChangeSliderValue()
        {
            SliderValue.text = ItemSlider.value.ToString();
        }

        //트레이드 컨트롤러에 줄 구조체
        private ItemData[] GetTradeData()
        {
            ItemData[] sendData = new ItemData[GlobalVariables.MaxItemNumber];

            for (int i = 0; i < tradeItemValue.Length; i++)
            {
                sendData[i].ItemType = (ItemType)i;
                sendData[i].Count = tradeItemValue[i];
            }
            return sendData;
        }

        public void OnClickedAIButton(int aiNumber)
        {
            sendData = GetTradeData();
            AiNumber = aiNumber;
        }

        private void SendTradeData()
        {
            UIManager.Instance.SendTradeData(sendData, playerNumber,AiNumber+1);
        }

        public void OnClickedRequestButton()
        {
            SendTradeData();
        }

        public void RecieveTradeData(OtherPlayerState state)
        {
            Debug.Log("컨트롤러에서 상태 받아옴");

            if (state == OtherPlayerState.Yes)
            {
                aIText[AiNumber].text = OtherPlayerState.Yes.ToString();
                tradePopUp.SetActive(true);
                tradeSuccessText.SetActive(true);
            }
            else
            {
                aIText[AiNumber].text = OtherPlayerState.No.ToString();
                tradePopUp.SetActive(true);
                tradeNoText.SetActive(true);
            }
        }
    }
}