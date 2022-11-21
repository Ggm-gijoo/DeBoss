using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum WeaponPos
{
    None,
    LeftHand,
    RightHand,
    AllHand
}
public class WeaponModule : MonoBehaviour
{
    private static Dictionary<int, WeaponSO> weapons = new Dictionary<int, WeaponSO>();

    [SerializeField] private Transform[] weaponTransform;

    [HideInInspector]
    public WeaponSO nowWeapon;
    [HideInInspector]
    public int nowWeaponIdx = 0;

    private MainModule mainModule;
    private GameObject clone;

    private readonly int _weapon = Animator.StringToHash("Weapon");
    private readonly int _trigger = Animator.StringToHash("Trigger");

    private void Awake()
    {
        mainModule = GetComponent<MainModule>();

        Init();
        SetNowWeapon();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            nowWeaponIdx = (nowWeaponIdx + 1) % weapons.Count;
            SetNowWeapon();
        }
    }
    private static void Init()
    {
        weapons.Clear();

        for (int i = 0; i < Directory.GetFiles($"Assets/Resources/SO/Weapon").Length / 2; i++)
        {
            WeaponSO weapon = Resources.Load($"SO/Weapon/ID_{i}") as WeaponSO;
            weapons.Add(weapon.WeaponId, weapon);
        }
    }

    public void SetWeaponIdx(int idx) => nowWeaponIdx = idx;

    public void SetNowWeapon()
    {
        nowWeapon = weapons[nowWeaponIdx];
        WeaponSwitch();
    }
    public void WeaponSwitch()
    {
        DisarmedWeapon();
        if (nowWeapon.WeaponPrefab != null)
        {
            clone = Instantiate(nowWeapon.WeaponPrefab);
            if (nowWeapon.Pos != WeaponPos.None)
            {
                clone.transform.SetParent(weaponTransform[(int)nowWeapon.Pos]);
                clone.transform.localPosition = Vector3.zero;
                clone.transform.localRotation = Quaternion.Euler(0, 180 * (int)nowWeapon.Pos, 0);
            }
        }
        mainModule.TriggerValue = AnimState.Idle;
        mainModule.anim.SetInteger(_weapon, (int)nowWeapon.Type);
        mainModule.anim.SetTrigger(_trigger);
    }

    public void DisarmedWeapon()
    {
        Destroy(clone);
    }

}
