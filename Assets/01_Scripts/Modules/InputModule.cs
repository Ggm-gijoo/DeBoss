using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    [SerializeField]private MoveModule moveModule;
    [SerializeField]private AttackModule attackModule;


    private void Awake()
    {
        if (moveModule == null)
            moveModule = GetComponent<MoveModule>();
        if (attackModule == null)
            attackModule = GetComponent<AttackModule>();
    }

    private void FixedUpdate() //이동
    {
        if(!MainModule.player.isAct)
            InputMove();
    }
    private void Update() //즉각 반응해야하는 행동
    {
        if (!MainModule.player.isAct)
        {
            InputDash();
            InputJump();
        }
        InputAttack();
    }

    public void InputDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && MainModule.player.TriggerValue != AnimState.Jump)
            moveModule.Dash();
    }
    public void InputMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveModule.MoveTo(h, v);
    }

    public void InputJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MainModule.player.TriggerValue = AnimState.Jump;
            moveModule.Jump();
        }
    }

    public void InputAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MainModule.player.TriggerValue = AnimState.Attack;
            attackModule.Attack();
        }
        else if (Input.GetMouseButtonUp(0))
            attackModule.StopAttack();
    }
}
