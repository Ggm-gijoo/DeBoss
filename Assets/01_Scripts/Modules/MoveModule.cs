using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveModule : MonoBehaviour
{
    [Header("이동 속도")]
    [Range(3f, 25f)]
    [SerializeField] private float moveSpeed = 3f;
    [Header("회전 가속도")]
    [Range(0.01f, 10f)]
    [SerializeField] private float moveChangeSpd = 0.5f;
    [Header("회전 속도")]
    [Range(10f, 3600f)]
    [SerializeField] private float angularSpeed = 60f;
    [Header("점프력")]
    [Range(5f, 10f)]
    [SerializeField] private float jumpForce = 7.5f;
    [Header("낙하 가속력")]
    [SerializeField] private float fallMultiplier = 2.5f;

    private MainModule mainModule;
    private GroundCheck groundCheck;
    private CharacterTrail characterTrail;
    private NavMeshAgent agent;
    private Rigidbody rigid;

    private readonly int _moving = Animator.StringToHash("Moving");
    private readonly int _jumping = Animator.StringToHash("Jumping");
    private readonly int _trigger = Animator.StringToHash("Trigger");

    Vector3 moveDir = Vector3.zero;
    int jumpCount = 0;

    private void Awake()
    {
        mainModule = GetComponent<MainModule>();
        groundCheck = GetComponent<GroundCheck>();
        characterTrail = GetComponent<CharacterTrail>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rigid != null)
            vecDirectionChangeBody();

        if (groundCheck != null)
        {
            if(groundCheck.IsGrounded() && jumpCount > 0)
            {
                jumpCount = 0;
                mainModule.TriggerValue = AnimState.Jump;

                mainModule.anim.SetInteger(_jumping, jumpCount);
                mainModule.anim.SetTrigger(_trigger);

                mainModule.TriggerValue = AnimState.Idle;

            }
            else if(!groundCheck.IsGrounded() && jumpCount == 0)
            {
                jumpCount = 2;
                mainModule.TriggerValue = AnimState.Jump;

                mainModule.anim.SetInteger(_jumping, jumpCount);
                mainModule.anim.SetTrigger(_trigger);
            }
        }
    }

    public void MoveTo(float h, float v)
    {   

        Vector3 forward = Camera.main.transform.localRotation * Vector3.forward;
        forward.y = 0.0f;

        forward = forward.normalized;

        moveDir = forward * v * moveSpeed + Quaternion.Euler(0, 90, 0) * forward * h * moveSpeed;

        rigid.velocity = new Vector3(moveDir.x,rigid.velocity.y,moveDir.z);

        mainModule.anim.SetBool(_moving, Mathf.Abs(h) + Mathf.Abs(v) > Mathf.Epsilon);
        mainModule.anim.SetFloat("Velocity Z", _moving);


        if (rigid.velocity.y < 0)
            rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }

    public void MoveTo(Vector3 targetPos)
    {
        if (targetPos == null) return;
        if (agent == null) return;

        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.angularSpeed = angularSpeed;
        agent.SetDestination(targetPos);
    }

    public void Dash()
    {
        if (!mainModule.isAct)
        {
            mainModule.isAct = true;
            characterTrail.OnTrail(0.3f);

            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            mainModule.TriggerValue = AnimState.Dodge;

            mainModule.anim.SetTrigger(_trigger, () =>
            {
                mainModule.isAct = false;
            }, 0.6f);
        }

    }

    void vecDirectionChangeBody()
    {
        if (moveDir == Vector3.zero) return;
        Quaternion quaternion = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, moveChangeSpd * Time.deltaTime);
    }

    public void Jump()
    {
        if (jumpCount < 3)
        {
            if (jumpCount == 0)
            {
                jumpCount++;
            }
            jumpCount++;

            mainModule.TriggerValue = AnimState.Jump;
            mainModule.anim.SetInteger(_jumping, jumpCount);
            mainModule.anim.SetTrigger(_trigger);

            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
