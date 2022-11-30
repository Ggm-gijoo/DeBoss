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

        WeaponSO[] loadWeapon = Resources.LoadAll<WeaponSO>("SO/Weapon");

        foreach (var weapon in loadWeapon)
            weapons.Add(weapon.WeaponId, weapon);
    }

    public void SetWeaponIdx(int idx) => nowWeaponIdx = idx;

    public void SetNowWeapon()
    {
        nowWeapon = weapons[nowWeaponIdx];
        WeaponSwitch();
    }
    public void WeaponSwitch()
    { 
        if (nowWeapon.WeaponPrefab != null)
        {
            SetMeshItem(nowWeapon.WeaponPrefab);
        }
        mainModule.TriggerValue = AnimState.Idle;
        mainModule.anim.SetInteger(_weapon, (int)nowWeapon.Type);
        mainModule.anim.SetTrigger(_trigger);
    }

    public Transform[] SetMeshItem(GameObject meshObj)
    {
        Transform[] retMeshItems = SetMeshObj(meshObj.GetComponentsInChildren<Weapon>());

        return retMeshItems;
    }

    private Transform[] SetMeshObj(Weapon[] weapons)
    {
        List<Transform> retMeshObjs = new List<Transform>();
        Transform parentBone = null;
        int index = 1;

        foreach (var weapon in weapons)
        {
            if (nowWeapon.Pos == WeaponPos.None) continue;
            else if (nowWeapon.Pos == WeaponPos.AllHand)
            {
                GameObject itemObj = GameObject.Instantiate(weapon.gameObject, weaponTransform[index++]);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * index * 180);
                retMeshObjs.Add(itemObj.transform);
            }
            else
            {
                parentBone = weaponTransform[(int)nowWeapon.Pos];
                GameObject itemObj = GameObject.Instantiate(weapon.gameObject, parentBone);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * (int)nowWeapon.Pos * 180);
                retMeshObjs.Add(itemObj.transform);
            }

                
        }

        return retMeshObjs.ToArray();
    }


}
