using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Weapon : MonoBehaviour
{
    [Header("2번째 무기에만 체크")]
    public bool twoHanded = false;

    public WeaponSO weaponSO;

    protected MainModule mainModule;
    protected WeaponModule weaponModule;

    protected Dictionary<int, GameObject> vfxs = new Dictionary<int, GameObject>();

    protected readonly int _attack = Animator.StringToHash("Action");
    protected readonly int _trigger = Animator.StringToHash("Trigger");
    protected readonly int _skill = Animator.StringToHash("Skill");

    protected bool isAtkEnd = true;

    [SerializeField]
    private VisualEffect hitVfx = null;
    [HideInInspector]
    public static VisualEffect hitEffect = null;

    private void Start()
    {
        mainModule = MainModule.player;
        weaponModule = mainModule.GetComponent<WeaponModule>();
        Init();
        SetHitEffect();

        if (twoHanded) return;

        for (int i = 0; i < weaponSO.Vfxs.Length; i++)
        {
            GameObject clone = Instantiate(weaponSO.Vfxs[i], null);
            vfxs.Add(i, clone);
            clone.SetActive(false);
        }
    }
    public virtual void Init(){}
    public virtual IEnumerator Attack()
    {
        mainModule.TriggerValue = AnimState.Attack;
        mainModule.attackMove = mainModule.attackMove % weaponSO.AtkMoveCount + 1;

        mainModule.anim.SetInteger(_attack, mainModule.attackMove);
        mainModule.anim.SetTrigger(_trigger);

        StartCoroutine(CheckAtkEnd());
        CinemachineCameraShaking.Instance.CameraShake();

        yield return new WaitForSeconds(0.2f);

        if (vfxs.Count >= mainModule.attackMove)
        {
            PlayVfx(mainModule.attackMove - 1);
            PlayHitVfx(mainModule.transform.position + mainModule.transform.forward * 2f + Vector3.up * 1.4f);
        }

        yield return new WaitUntil(()=>isAtkEnd);
    }

    public void PlayVfx(int num)
    {
        GameObject vfx = vfxs[num];

        vfx.transform.position = mainModule.transform.position;
        vfx.transform.rotation = mainModule.transform.rotation;

        vfx.SetActive(false);
        vfx.SetActive(true);
    }

    public void PlayHitVfx(Vector3 pos)
    {
        hitEffect.transform.position = pos;
        hitEffect?.Play();
    }

    public void SetHitEffect()
    {
        if (hitVfx == null) return;
        if (hitEffect == null)
            hitEffect = Instantiate(hitVfx).GetComponent<VisualEffect>();
    }

    public virtual IEnumerator CheckAtkEnd()
    {
        isAtkEnd = false;

        yield return new WaitUntil(() => !mainModule.anim.GetCurrentAnimatorStateInfo(0).IsName($"Attack{mainModule.attackMove}"));

        isAtkEnd = true;
    }
}
