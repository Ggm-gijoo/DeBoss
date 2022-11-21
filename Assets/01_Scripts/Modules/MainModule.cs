using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AnimState
{
    Attack = 4,
    GetHit = 12,
    Jump = 18,
    Idle = 25,
    Knockback = 26,
    Knockdown = 27,
    Dodge = 28,
    Skill = 30,
}

public class MainModule : MonoBehaviour
{
    public static MainModule player;

    public bool isPlayer = false;
    public bool isAct = false;

    public Animator anim;
    private AnimState animstate = AnimState.Idle;
    public AnimState TriggerValue { set { animstate = value; anim.SetInteger(_triggerNum, (int)animstate); } get { return animstate; } }
    private readonly int _triggerNum = Animator.StringToHash("TriggerNumber");

    void Awake()
    {
        if (isPlayer)
            player = this;
        anim = GetComponent<Animator>();
    }

    void Update()
    { 

    }

    public void Damage()
    {

    }
}
