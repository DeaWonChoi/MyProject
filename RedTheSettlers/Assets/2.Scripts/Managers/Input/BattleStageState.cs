using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using UnityEngine;

/// <summary>
/// 담당자 : 박상원
/// State 패턴 구현부
/// Battle 씬 유저 캐릭터 이동 및 공격 등
/// </summary>
public class BattleStageState : MonoBehaviour, IInputState
{
    private Camera battleCamera;
    private BattlePlayer battlePlayer;
    private Vector3 skillDirection;
    private Vector3 playerDirection;
    private Vector2 startDragPosition;
    private Vector2 currentDragPosition;
    private Vector2 startSkillDirection;
    private Vector2 currentSkillDirection;
    private Touch firstTouch;
    private Touch secondTouch;
    private bool TouchMoveActive = false;
    private int reversValue = -1;
    private float playerRotate;
    private float skillRotate;
    private float moveSpeed;

    private void Update()
    {
#if UNITY_ANDROID
        MultiTouchCheck();
#endif
    }

#if UNITY_ANDROID
    public void MultiTouchCheck()
    {
        if(Input.touchCount >= 2)
        {
            TouchMoveActive = true;
        }
    }
#endif

    public void MovingPlayer()
    {
        if(battleCamera == null)
        {
            battleCamera = GameObject.FindWithTag("BattleCamera").GetComponentInChildren<Camera>();
        }
        Ray rayPoint = battleCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(rayPoint);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.name.Contains("Plane"))
            {
                Vector3 rayHitPosition = hit.point;
                //TemporaryGameManager.Instance.PlayerMove(rayHitPosition);
                GameManager.Instance.PlayerMove(rayHitPosition);
            }
        }
    }

    public void BattleAttack()
    {
        //TemporaryGameManager.Instance.PlayerAttack();
        GameManager.Instance.PlayerAttack();
        //LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "공격");
    }

    public void UseSkill(int skillSlotNumber)
    {
#if UNITY_EDITOR
        //TemporaryGameManager.Instance.PlayerSkill(skillSlotNumber);
        GameManager.Instance.PlayerSkill(skillSlotNumber);
        //LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "스킬 " + skillSlotNumber + "번");
#endif
#if UNITY_ANDROID
        skillDirection = Input.mousePosition;
        LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "스킬 방향 : " + skillDirection);
#endif
#if UNITY_STANDALONE_WIN
        //TemporaryGameManager.Instance.PlayerSkill(skillSlotNumber);
        GameManager.Instance.PlayerSkill(skillSlotNumber);
#endif
    }

    public void OnStartDrag()
    {
#if UNITY_ANDROID
        startDragPosition = Input.mousePosition;
        if(TouchMoveActive==true)
        {
            startSkillDirection = Input.mousePosition;
        }
#endif
    }

    public void OnDragging(float speed)
    {
#if UNITY_ANDROID
        moveSpeed = speed;
        currentDragPosition = Input.mousePosition;
        Vector3 dragDistance = startDragPosition - currentDragPosition;
        playerRotate = (Mathf.Atan2(dragDistance.y, dragDistance.x) * Mathf.Rad2Deg) * reversValue - 90f;
        playerDirection = Quaternion.Euler(0f, playerRotate, 0f) * Vector3.forward;
        //TemporaryGameManager.Instance.PlayerMove(playerDirection, playerRotate, speed);
        //dragDirection = (currentDragPosition - startDragPosition).normalized;

        if(TouchMoveActive==false)
        {
            //TemporaryGameManager.Instance.PlayerMove(playerDirection, playerRotate, speed);
        }

        if(TouchMoveActive==true)
        {
            TemporaryGameManager.Instance.PlayerRotate(playerRotate);
            /*currentSkillDirection = Input.mousePosition;
            Vector3 skillDragDistance = startSkillDirection - currentSkillDirection;
            skillRotate = (Mathf.Atan2(skillDragDistance.y, skillDragDistance.x) * Mathf.Rad2Deg) * reversValue - 90f;*/
        }
#endif
    }

    public void EndStopDrag()
    {
#if UNITY_ANDROID
        startDragPosition = Vector3.zero;
        currentDragPosition = Vector3.zero;
        playerDirection = Vector3.zero;
        //TemporaryGameManager.Instance.PlayerMove(playerDirection, playerRotate, moveSpeed);
#endif
    }

    // 이 밑으로 해당 클래스에서는 사용하지 않음.
    // 추후 구조 변경시 필요치 않은 메서드들은 해당 클래스에서 사라질 예정

    public void DragMove(float speed)
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

    public void OnDropSlot()
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
}