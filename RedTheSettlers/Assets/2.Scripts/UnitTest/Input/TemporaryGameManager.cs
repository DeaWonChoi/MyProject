using RedTheSettlers.GameSystem;
using RedTheSettlers.Players;
using RedTheSettlers.Tiles;
using UnityEngine;

public class TemporaryGameManager : Singleton<TemporaryGameManager>
{
    private TemporaryCameraController tempCameraController;
    private CameraController cameraCtrl;
    private new Rigidbody rigidbody;
    private Transform player;
    private BattlePlayer battlePlayer;
    private Coroutine coroutineMove;
    private Coroutine coroutineAttack;
    private Vector3 moveDirection;
    private float playerRotation;
    private float moveSpeed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        tempCameraController = GameObject.FindObjectOfType<TemporaryCameraController>();
        cameraCtrl = GameObject.FindObjectOfType<CameraController>();
        //player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        battlePlayer = GameObject.FindWithTag("Player").GetComponent<BattlePlayer>();
    }

    /*private void FixedUpdate()
    {
        if(playerRotation > 0)
        {
            PlayerMove(moveDirection);
        }
    }*/

    public void CameraMove(Vector3 direction)
    {
        //tempCameraController.CameraMove(direction);
        cameraCtrl.CameraDragMoving(direction);
    }

    public void CameraZoom(float value)
    {
        //tempCameraController.CameraZoom(value);
        cameraCtrl.ZoomInOut(value);
    }

    public void PlayerMove(Vector3 direction)
    {
#if UNITY_ANDROID
        moveDirection = direction;
        playerRotation = rotation;
        moveSpeed = speed;
        //Player.MovePosition(Player.position + (direction * 10) * Time.deltaTime);
        if(rotation > 0)
        {
            rigidbody.rotation = Quaternion.Euler(0f, rotation, 0f);
            rigidbody.velocity = (direction * speed) * Time.deltaTime;
        }
        rigidbody.velocity = direction * 20f * Time.deltaTime;
#endif
        Vector3 targetPosition = direction + new Vector3(0, battlePlayer.transform.position.y, 0);
        if (coroutineMove == null)
        {
            coroutineMove = StartCoroutine(battlePlayer.MoveToTargetPostion(direction));
        }
        else
        {
            StopCoroutine(coroutineMove);
            coroutineMove = StartCoroutine(battlePlayer.MoveToTargetPostion(direction));
        }
    }

    public void PlayerRotate(float rotation)
    {

    }

    public void PlayerAttack()
    {
        battlePlayer.AttackEnemy(10);
    }

    public void PlayerSkill(int SkillNumber)
    {
        LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, SkillNumber + "번 스킬");
        battlePlayer.UseSkill(SkillNumber);
    }

    public void TileInfo(BoardTile tileType)
    {
        LogManager.Instance.UserDebug(LogColor.Blue, GetType().Name, "타일 정보 : " + tileType);
    }
}