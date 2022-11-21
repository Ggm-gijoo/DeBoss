using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModule : MonoBehaviour
{
    [Header("공격 범위")]
    [SerializeField] private float attackRange = 3f;
    [Header("이동 범위")]
    [SerializeField] private float chaseRange = 10f;

    MoveModule moveModule;
    AttackModule attackModule;

    Transform player;

    private void Start()
    {
        moveModule = GetComponent<MoveModule>();
        attackModule = GetComponent<AttackModule>();
        player = MainModule.player.transform;
    }
    private void FixedUpdate()
    {
        if (player == null) return;

        switch(Vector3.Distance(player.position, transform.position))
        {
            case var a when a <= attackRange:
                Attack();
                break;

            case var a when a > attackRange && a <= chaseRange:
                StopAttack();
                Chasing();
                break;

            default:
                break;
        }
    }

    public void Chasing()
    { 
        moveModule.MoveTo(player.position);
    }

    public void Attack()
    {
        attackModule.Attack(player.position);
    }

    public void StopAttack()
    {
        attackModule.StopAttack();
    }
}
