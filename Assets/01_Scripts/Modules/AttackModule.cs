using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class AttackModule : MonoBehaviour
{
    private float endDelay = 1f;

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

            yield return new WaitForSeconds(0.1f);

            //GameObject vfx = null;

            //if (vfx != weaponModule?.weaponVfx[weaponModule.nowWeaponIdx][mainModule.attackMove - 1])
            //    vfx = Instantiate(weaponModule.weaponVfx[weaponModule.nowWeaponIdx][mainModule.attackMove - 1], transform);

            //vfx?.GetComponent<VisualEffect>().Play();
            //vfx.transform.SetParent(null);

            yield return new WaitUntil(() => !mainModule.anim.GetCurrentAnimatorStateInfo(0).IsName($"Attack{mainModule.attackMove}"));

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
