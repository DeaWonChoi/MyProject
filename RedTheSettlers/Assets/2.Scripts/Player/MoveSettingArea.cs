using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Players;

public class MoveSettingArea : MonoBehaviour {

    public BattlePlayer battlePlayer;
    private Coroutine coroutineMove;
    private Coroutine coroutineAttack;

    private void Start()
    {
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        while(true)
        {
            if (Input.GetMouseButton(0))
            {
                //MoveByMousePointer();
                yield return new WaitForSeconds(0.25f);
            }

            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveByKeyboard();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                MakePlayerAttack();
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                battlePlayer.UseSkill(0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                battlePlayer.UseSkill(1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                battlePlayer.UseSkill(2);
            }

            yield return null;
        }
    }

    private void MoveByMousePointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.name.Contains("Plane"))
            {
                Vector3 targetPosition = hit.point + new Vector3(0, battlePlayer.transform.position.y, 0);
                if(coroutineMove == null)
                {
                    coroutineMove = StartCoroutine(battlePlayer.MoveToTargetPostion(targetPosition));
                }
                else
                {
                    StopCoroutine(coroutineMove);
                    coroutineMove = StartCoroutine(battlePlayer.MoveToTargetPostion(targetPosition));
                }
            }
        }
    }

    private void MoveByKeyboard()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (coroutineMove == null)
        {
            coroutineMove = StartCoroutine(battlePlayer.MoveToDirection(direction));
        }

        else
        {
            StopCoroutine(coroutineMove);
            coroutineMove = StartCoroutine(battlePlayer.MoveToDirection(direction));
        }
    }

    private void MakePlayerAttack()
    {
        battlePlayer.AttackEnemy(10);
    }
}
