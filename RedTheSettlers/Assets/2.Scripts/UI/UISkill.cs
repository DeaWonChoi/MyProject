using UnityEngine;
using UnityEngine.UI;
using RedTheSettlers.GameSystem;
using RedTheSettlers.Users;
using RedTheSettlers.Skills;
using System;


namespace RedTheSettlers.UI
{
    /// <summary>
    /// 작성자 : 김하정
    /// 스킬 UI 스크립트 : 스킬 인벤토리 창을 제작. 스킬을 장착, 제거할 수 있다.
    /// </summary>
    public class UISkill : MonoBehaviour
    {
        [Serializable]
        struct Icons
        {
            public Sprite  SkillImage;
            public Button SkillButton;
           
           
        }
        [SerializeField]
        private Icons[] icons;

        [SerializeField]
        private Button[] SlotButton;

        [SerializeField]
        private GameObject alertPopup;
        [SerializeField]
        private GameObject skillGroup;


        const int LIMITSLOT = 3;
        private int skillSlotNumber = 0;

        private void Start()
        {
            if (icons.Length > GlobalVariables.MaxSkillNumber)
            {
                LogManager.Instance.UserDebug(LogColor.Red, GetType().Name, "작업자님 인스펙터 창을 확인해주세요. 스킬 아이콘의 개수가 4개를 초과했습니다.");
            }
        }

        
        public void OnClickedSkillButton(int index)
        {
            SlotButton[skillSlotNumber].image.sprite = icons[index].SkillImage;
            icons[index].SkillButton.interactable = false;
            skillSlotNumber++;
        }

        public void OnClickedSlotButton(int index)
        {
            SlotButton[index].image.sprite = null;
            //어떻게 해야 2번 이후의 아이콘의 인터렉테이블을 트루 시킬 수 있을까?
            skillSlotNumber--;
        }

        public void CloseSkillSlot()
        {
            int slotCount = 0;
            for (int i = 0; i < icons.Length; i++)
            {
                if (SlotButton[i].gameObject.activeSelf == true)
                {
                    slotCount++;
                    if (slotCount < 3)
                    {
                        alertPopup.SetActive(true);
                        
                    }
                    else
                    {
                        skillGroup.SetActive(false);
                    }
                }
            }

        }
      
    }
}
