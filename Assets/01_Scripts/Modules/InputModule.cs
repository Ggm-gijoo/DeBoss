using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputModule : MonoSingleton<InputModule>
{
    [SerializeField]private MoveModule moveModule;
    [SerializeField]private AttackModule attackModule;

    public UnityEvent EndAttackEvent { get; private set; } = new UnityEvent();

    private void Awake()
    {
        if (moveModule == null)
            moveModule = GetComponent<MoveModule>();
        if (attackModule == null)
            attackModule = GetComponent<AttackModule>();
    }

    private void FixedUpdate() //이동
    {
        if (Time.timeScale < 0.1f) return;
        InputMove();
    }
    private void Update() //즉각 반응해야하는 행동
    {
        if (Time.timeScale < 0.1f) return;
        if (!MainModule.player.IsAct)
        {
            InputDash();
            InputJump();
        }
        InputAttack();
    }

    public void InputDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveModule.Dash();
    }
    public void InputMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (MainModule.player.IsAct)
            h = v = 0;
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
        if (Input.GetMouseButtonDown(0) && MainModule.player.TriggerValue != AnimState.Dodge && MainModule.player.TriggerValue != AnimState.Jump)
        {
            MainModule.player.TriggerValue = AnimState.Attack;
            attackModule.Attack();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndAttackEvent.Invoke();
            attackModule.StopAttack();
        }
    }
}
