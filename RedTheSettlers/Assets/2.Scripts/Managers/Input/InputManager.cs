using UnityEngine;

/// <summary>
/// 담당자 : 박상원
/// 화면 또는 아이콘 드래그, 이동 방향 전달, UI 버튼 입력시
/// 해당 UI 버튼 해야 하는 일을 받아서 전달
/// 최대한 커밋시 다른 코드와 오류가 나지 않도록 구조 수정 중
/// </summary>
namespace RedTheSettlers.GameSystem
{
    public enum StateType
    {
        MainStageState,
        BattleStageState,
        TradeInMainStageState,
    }
    public enum SkillSlot
    {
        SkillSlotA,
        SkillSlotB,
        SkillSlotC,
    }

    public class InputManager : Singleton<InputManager>
    {
        private IInputState inputState;

        [SerializeField, Range(1, 200)]
        private float moveSpeed;
        [SerializeField, Range(1, 100)]
        private float zoomSpeed;

        private Vector3 downClickPoint;
        private Vector3 upClickPoint;
        private bool enableInputBattleStage = false;
        private bool enableInputMainStage = false;
        private bool enableInputCamera = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            TypeState(StateType.MainStageState);
        }

        private void Update()
        {
            if (enableInputMainStage && enableInputCamera)
            {
                CameraZoomInOut();
            }
        }

        private void FixedUpdate()
        {
            PcInput();
        }

        public void CameraZoomInOut()
        {
            inputState.ZoomInOut(zoomSpeed);
        }

        public void OnBeginDrag()
        {
            inputState.OnStartDrag();
        }

        public void OnDrag()
        {
            inputState.OnDragging(moveSpeed);
        }

        public void EndDrag()
        {
            inputState.EndStopDrag();
        }

        public void OnDrop()
        {
            inputState.OnDropSlot();
        }

        // PC에서의 입력
        private void PcInput()
        {
            if (enableInputBattleStage)
            {       
                // 좌클릭 캐릭터 이동
                if (Input.GetMouseButton(0))
                {
                    inputState.MovingPlayer();
                }
                // 스페이스바 기본 공격
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    inputState.BattleAttack();
                }
                // Q,W,E 스킬
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    inputState.UseSkill((int)SkillSlot.SkillSlotA);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    inputState.UseSkill((int)SkillSlot.SkillSlotB);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inputState.UseSkill((int)SkillSlot.SkillSlotC);
                }
            }

            if (enableInputMainStage)
            {
                // 좌클릭시 처음 눌렀을 때와 땠을 때의 거리가 같지 않으면 작동하지 않게...
                // 테스트 필요.
                if(Input.GetMouseButtonDown(0))
                {
                    downClickPoint = Input.mousePosition;
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    upClickPoint = Input.mousePosition;
                    if(downClickPoint == upClickPoint)
                    {
                        inputState.TileInfo();
                    }
                }
            }
        }

        private void ChangeState(IInputState state)
        {
            inputState = state;
        }

        public void TypeState(StateType stateType)
        {
            switch (stateType)
            {
                case StateType.MainStageState:
                    ChangeState(new MainStageState());
                    enableInputMainStage = true;
                    enableInputBattleStage = false;
                    enableInputCamera = true;
                    break;
                case StateType.BattleStageState:
                    ChangeState(new BattleStageState());
                    enableInputMainStage = false;
                    enableInputBattleStage = true;
                    enableInputCamera = false;
                    break;
                case StateType.TradeInMainStageState:
                    ChangeState(new TradeInMainStageState());
                    enableInputMainStage = false;
                    enableInputBattleStage = false;
                    enableInputCamera = false;
                    break;
                default:
                    LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "해당되는 상태가 존재하지 않습니다.");
                    break;
            }
        }
    }
}