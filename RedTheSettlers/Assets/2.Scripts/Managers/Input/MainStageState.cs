using RedTheSettlers.GameSystem;
using RedTheSettlers.Tiles;
using UnityEngine;

/// <summary>
/// 담당자 박상원
/// State 패턴 구현부
/// Board Game 부분 버튼 기능 및 카메라 이동
/// </summary>
public class MainStageState : IInputState
{
    [SerializeField]
    private Camera boardCamera;
    private Vector3 firstClick;
    private Vector3 dragPosition;
    private Vector3 dragDirection;
    private BoardTile tileInformation;
    private float cameraZoom;
    private float touchDistance;
    private float firstDistance;
    private float currentDistance;
    private const int reversValue = -1;
    private const int touchMaxCount = 2;

    public void TileInfo()
    {
        if (boardCamera == null)
        {
            boardCamera = GameObject.FindWithTag("BoardCamera").GetComponentInChildren<Camera>();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray rayPoint = boardCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(rayPoint, out hitPoint, Mathf.Infinity))
            {
                if(hitPoint.collider.CompareTag("Tile"))
                {
                    tileInformation = hitPoint.collider.gameObject.GetComponent<BoardTile>();
                    GameManager.Instance.GetClickedTile(tileInformation);
                }
                else if(!hitPoint.collider.CompareTag("Tile"))
                {
                    LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "타일 정보를 찾을 수 없습니다.");
                }
            }
        }
    }

    public void OnStartDrag()
    {
        firstClick = Input.mousePosition;
    }

    public void OnDragging(float speed)
    {
        dragPosition = Input.mousePosition;
        dragDirection = (((dragPosition - firstClick).normalized * speed) * reversValue) * Time.deltaTime;
        //TemporaryGameManager.Instance.CameraMove(dragDirection);
        GameManager.Instance.CameraMoving(dragDirection);
    }

    public void EndStopDrag()
    {
        dragPosition = Vector3.zero;
        firstClick = Vector3.zero;
        dragDirection = Vector3.zero;
    }

    public void ZoomInOut(float speed)
    {
        #if UNITY_EDITOR
        if (Input.GetAxis("Mouse ScrollWheel") * reversValue < 0)
        {
            cameraZoom = (Input.GetAxis("Mouse ScrollWheel") * speed) * reversValue;
            //TemporaryGameManager.Instance.CameraZoom(cameraZoom);
            GameManager.Instance.CameraZoomInOut(cameraZoom);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") * reversValue > 0)
        {
            cameraZoom = (Input.GetAxis("Mouse ScrollWheel") * speed) * reversValue;
            //TemporaryGameManager.Instance.CameraZoom(cameraZoom);
            GameManager.Instance.CameraZoomInOut(cameraZoom);
        }
#endif

#if UNITY_STANDALONE_WIN
        if (Input.GetAxis("Mouse ScrollWheel") * reversValue < 0)
        {
            cameraZoom = (Input.GetAxis("Mouse ScrollWheel") * speed) * reversValue;
            //TemporaryGameManager.Instance.CameraZoom(cameraZoom);
            GameManager.Instance.CameraZoomInOut(cameraZoom);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") * reversValue > 0)
        {
            cameraZoom = (Input.GetAxis("Mouse ScrollWheel") * speed) * reversValue;
            //TemporaryGameManager.Instance.CameraZoom(cameraZoom);
            GameManager.Instance.CameraZoomInOut(cameraZoom);
        }
#endif

        // 모바일 줌인,아웃 구현부 테스트 필요 미완성
#if UNITY_ANDROID
        if (Input.touchCount == touchMaxCount)
        {
            Touch firstTouchPoint = Input.GetTouch(0);
            Touch secondTouchPoint = Input.GetTouch(1);

            Vector2 firstTouchPrevPoint = firstTouchPoint.position - firstTouchPoint.deltaPosition;
            Vector2 secondTouchPrevPoint = secondTouchPoint.position - secondTouchPoint.deltaPosition;

            float prevTouchDeltaPoint = (firstTouchPrevPoint - secondTouchPrevPoint).magnitude;
            float currentTouchDeltaPoint = (firstTouchPoint.position - secondTouchPoint.position).magnitude;

            float deltaMagnitude = prevTouchDeltaPoint - currentTouchDeltaPoint;
            TemporaryGameManager.Instance.CameraZoom(deltaMagnitude);

            /*float prevLength = 0;
            float Length = 0;

            Vector2 a = Input.GetTouch(0).position;
            Vector2 b = Input.GetTouch(1).position;

            Length = Vector2.Distance(a, b);
            TemporaryGameManager.Instance.CameraZoom(Length - prevLength);
            prevLength = Length;*/
        }
#endif
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

    public void BattleAttack()
    {
        throw new System.NotImplementedException();
    }

    public void UseSkill(int skillSlotNumber)
    {
        throw new System.NotImplementedException();
    }

    public void MovingPlayer()
    {
        throw new System.NotImplementedException();
    }
}
