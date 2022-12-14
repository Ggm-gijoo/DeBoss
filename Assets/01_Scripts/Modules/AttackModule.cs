using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackModule : MonoBehaviour
{
    private float endDelay = 2f;

    private WeaponModule weaponModule;
    private Coroutine attackCoroutine;
    private MainModule mainModule;

    private readonly int _attack = Animator.StringToHash("Action");
    private readonly int _trigger = Animator.StringToHash("Trigger");
    private readonly int _skill = Animator.StringToHash("Skill");

    private bool flag = false;

    private void Awake()
    {
        weaponModule = GetComponent<WeaponModule>();
        mainModule = GetComponent<MainModule>();
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
            mainModule.isAct = true;
            mainModule.attackMove = mainModule.attackMove % weaponModule.nowWeapon.AtkMoveCount + 1;

            mainModule.anim.SetInteger(_attack, mainModule.attackMove);
            mainModule.anim.SetTrigger(_trigger);

            yield return null;
            yield return new WaitForSecondsRealtime(0.5f);
            if (mainModule.attackMove == weaponModule.nowWeapon.AtkMoveCount)
            {
                mainModule.isAct = false;
                yield return new WaitForSeconds(endDelay);
            }
        }
        mainModule.isAct = false;
        attackCoroutine = null;
    }
}
