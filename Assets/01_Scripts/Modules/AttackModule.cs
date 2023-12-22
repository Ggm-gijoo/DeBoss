using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class AttackModule : MonoBehaviour
{
    private WeaponModule weaponModule;
    private Coroutine attackCoroutine;
    private MainModule mainModule;

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
            mainModule.IsAct = true;
            flag = true;
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
        while (flag)
        {
            yield return StartCoroutine(WeaponModule.parentsDict[weaponModule.nowWeaponIdx][0].GetComponent<Weapon>().Attack());
        }
        MainModule.player.TriggerValue = AnimState.Idle;
        mainModule.IsAct = false;
        attackCoroutine = null;
    }
}
