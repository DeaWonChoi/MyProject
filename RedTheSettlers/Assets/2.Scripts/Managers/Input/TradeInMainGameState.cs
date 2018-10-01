using UnityEngine;

/// <summary>
/// 담당자 : 박상원
/// 거래 화면 입력 부분
/// </summary>
public class TradeInMainStageState : IInputState
{
    private static GameObject[] tradeCards;
    private static GameObject[] cardAreas;
    private GameObject targetCard;
    private GameObject targetArea;
    private Transform parentToReturnTo = null;
    private Vector3 startPosition;
    private Vector3 clickPoint;
    private Vector3 dropPoint;
    private float cardDistance;
    private float currentCardDistance;
    private float areaDistance;
    private float currentAreaDistance;

    public void OnStartDrag()
    {
        clickPoint = Input.mousePosition;
        // FindGameObjectWithTag는 임시방편으로 UI및 패널을 찾기 위해 작성한 코드로 추후 수정 예정
        tradeCards = GameObject.FindGameObjectsWithTag("UIIcon");
        cardDistance = Vector3.Distance(clickPoint, tradeCards[0].transform.position);
        foreach (GameObject tradeCard in tradeCards)
        {
            currentCardDistance = Vector3.Distance(clickPoint, tradeCard.transform.position);
            if (currentCardDistance <= cardDistance)
            {
                targetCard = tradeCard;
                cardDistance = currentCardDistance;
            }
        }
        startPosition = targetCard.transform.position;
        targetCard.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        parentToReturnTo = targetCard.transform.parent;
        targetCard.transform.SetParent(targetCard.transform.parent.parent);
    }

    public void OnDragging(float speed)
    {
        targetCard.transform.position = Input.mousePosition;
    }

    public void EndStopDrag()
    {
        targetCard.transform.SetParent(parentToReturnTo);
        targetCard.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDropSlot()
    {
        dropPoint = Input.mousePosition;
        // FindGameObjectWithTag는 임시방편으로 UI및 패널을 찾기 위해 작성한 코드로 추후 수정 예정
        cardAreas = GameObject.FindGameObjectsWithTag("CardArea");
        areaDistance = Vector3.Distance(dropPoint, cardAreas[0].transform.position);
        foreach(GameObject cardArea in cardAreas)
        {
            currentAreaDistance = Vector3.Distance(dropPoint, cardArea.transform.position);
            if(currentAreaDistance <= areaDistance)
            {
                targetArea = cardArea;
                areaDistance = currentAreaDistance;
            }
        }
        if (parentToReturnTo != null)
        {
            parentToReturnTo = targetArea.transform;
        }
    }

    // 이 밑으로 해당 클래스에서는 사용하지 않음.
    // 추후 구조 변경시 필요치 않은 메서드들은 해당 클래스에서 사라질 예정

    public void DragMove(float speed)
    {
        throw new System.NotImplementedException();
    }

    public void SkillDirection()
    {
        throw new System.NotImplementedException();
    }

    public void ZoomInOut(float speed)
    {
        throw new System.NotImplementedException();
    }

    public void TileInfo()
    {
        throw new System.NotImplementedException();
    }

    public void OnInPointer()
    {
        throw new System.NotImplementedException();
    }

    public void OnOutPointer()
    {
        throw new System.NotImplementedException();
    }

    public void MovingPlayer()
    {
        throw new System.NotImplementedException();
    }

    public void BattleAttack()
    {
        throw new System.NotImplementedException();
    }

    public void UseSkill(int skillSlotNumber)
    {
        throw new System.NotImplementedException();
    }
}