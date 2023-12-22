using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GreatSword : Weapon
{
    private bool isCharging = false;
    private readonly int _charge = Animator.StringToHash("Charging");
    
    public override void Init()
    {
        InputModule.Instance.EndAttackEvent.RemoveListener(() => isCharging = false);
        InputModule.Instance.EndAttackEvent.AddListener(() => isCharging = false);
    }
    public override IEnumerator Attack()
    {
        mainModule.attackMove = 1;
        mainModule.anim.SetInteger(_attack, mainModule.attackMove);
        mainModule.anim.SetTrigger(_trigger);

        isCharging = true;
        mainModule.anim.SetBool(_charge, true);
        CinemachineCameraShaking.Instance.CameraShake(5, 0.1f);
        StartCoroutine(CheckAtkEnd());

        yield return new WaitForSeconds(0.2f);
        PlayVfx(0);
        PlayHitVfx(mainModule.transform.position + mainModule.transform.forward * 2f + Vector3.up * 1.4f);

        yield return new WaitForSeconds(0.3f);

        float timer = 0f;
        while(isCharging && timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        mainModule.attackMove = 2;

        isCharging = false;
        mainModule.anim.SetBool(_charge, false);
        mainModule.anim.SetInteger(_attack, mainModule.attackMove);
        CinemachineCameraShaking.Instance.CameraShake(10, 0.2f);

        yield return new WaitForSeconds(0.2f);

        PlayVfx(1);
        PlayHitVfx(mainModule.transform.position + mainModule.transform.forward * 2f + Vector3.up * 1.4f);

        yield return new WaitUntil(() => isAtkEnd);
    }

    public override IEnumerator CheckAtkEnd()
    {
        isAtkEnd = false;

        yield return new WaitUntil(() => !mainModule.anim.GetCurrentAnimatorStateInfo(0).IsName($"Attack2"));

        isAtkEnd = true;
    }
}
