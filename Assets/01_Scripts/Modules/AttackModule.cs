using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackModule : MonoBehaviour
{
    private float[] attackDelay = {0.75f, 1f, 1.5f};
    private NavMeshAgent agent;
    private WeaponModule weaponModule;
    private Coroutine attackCoroutine;
    private MainModule mainModule;

    private readonly int _attack = Animator.StringToHash("Action");
    private readonly int _trigger = Animator.StringToHash("Trigger");
    private readonly int _skill = Animator.StringToHash("Skill");

    private int attackMove = 0;
    private bool flag = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        weaponModule = GetComponent<WeaponModule>();
        mainModule = GetComponent<MainModule>();
    }

    public void Attack(Vector3 targetPos)
    {
        if (agent == null)
        {
            Attack();
            return;
        }
        agent.SetDestination(targetPos);
        if (attackCoroutine == null)
        {
            agent.isStopped = true;
            agent.speed = 0;
            attackCoroutine = StartCoroutine(AttackRepeat());
        }
    }

    public void Attack()
    {
        if (attackCoroutine == null)
        {
            mainModule.isAct = true;
            attackCoroutine = StartCoroutine(AttackRepeat());
        }
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            flag = false;
        }
    }

    private IEnumerator AttackRepeat()
    {
        flag = true;
        while (flag)
        {
            attackMove = attackMove % weaponModule.nowWeapon.AtkMoveCount + 1;

            mainModule.anim.SetInteger(_attack, attackMove);
            mainModule.anim.SetTrigger(_trigger);

            yield return new WaitForSeconds(attackDelay[(int)weaponModule.nowWeapon.Speed]);
        }
        mainModule.isAct = false;
        attackCoroutine = null;
    }
}
