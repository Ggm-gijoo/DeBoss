using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.VFX;

public enum WeaponPos
{
    None,
    LeftHand,
    RightHand,
    AllHand
}
public class WeaponModule : MonoBehaviour
{
    public static Dictionary<int, WeaponSO> weapons = new Dictionary<int, WeaponSO>();
    public static Dictionary<int, List<GameObject>> parentsDict = new Dictionary<int, List<GameObject>>();

    private VisualEffectAsset[][] weaponVfx = new VisualEffectAsset[100][];

    [SerializeField] private Transform[] weaponTransform;

    [HideInInspector]
    public WeaponSO nowWeapon;
    [HideInInspector]
    public int nowWeaponIdx = 0;

    private MainModule mainModule;

    private readonly int _weapon = Animator.StringToHash("Weapon");
    private readonly int _trigger = Animator.StringToHash("Trigger");
    private readonly int _weaponChange = Animator.StringToHash("WeaponChange");

    private void Awake()
    {
        mainModule = GetComponent<MainModule>();
        Init();
        SetPool();
        SetNowWeapon();
    }

    private static void Init()
    {
        weapons.Clear();

        WeaponSO[] loadWeapon = Resources.LoadAll<WeaponSO>("SO/Weapon");

        foreach (var weapon in loadWeapon)
            weapons.Add(weapon.WeaponId, weapon);
    }
    private void SetPool()
    {
        parentsDict.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].WeaponPrefab == null) continue;

            SetMeshObj(weapons[i]);
            for (int j = 0; j < parentsDict[weapons[i].WeaponId].Count; j++)
                parentsDict[weapons[i].WeaponId][j].SetActive(false);

        }
    }

    public void GetPool()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].WeaponPrefab == null) continue;

            for(int j = 0; j < parentsDict[weapons[i].WeaponId].Count; j++)
                parentsDict[weapons[i].WeaponId][j].SetActive(false);
        }
        for (int j = 0; j < parentsDict[nowWeapon.WeaponId].Count; j++)
        {
            parentsDict[nowWeapon.WeaponId][j].SetActive(true);
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
        if (nowWeapon.WeaponPrefab != null)
        {
            GetPool();
        }
        else
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].WeaponPrefab == null) continue;

                for (int j = 0; j < parentsDict[weapons[i].WeaponId].Count; j++)
                    parentsDict[weapons[i].WeaponId][j].SetActive(false);
            }
        }

        if (mainModule.TriggerValue != AnimState.Dodge)
            mainModule.anim.SetTrigger(_trigger);
        mainModule.attackMove = 0;

        mainModule.anim.SetInteger(_weapon, (int)nowWeapon.Type);
        mainModule.anim.SetTrigger(_weaponChange);
    }

    private void SetMeshObj(WeaponSO weaponSO)
    {
        Transform parentBone = null;

        GameObject meshObj = weaponSO.WeaponPrefab;
        Weapon[] weapons = meshObj.GetComponentsInChildren<Weapon>();

        List<GameObject> retMeshObjs = new List<GameObject>();

        int index = 1;

        foreach (var weapon in weapons)
        {
            if (weaponSO.Pos == WeaponPos.None) continue;

            GameObject itemObj;

            if (weaponSO.Pos == WeaponPos.AllHand)
            {
                itemObj = Instantiate(weapon.gameObject, weaponTransform[index++]);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * index * 180);
                retMeshObjs.Add(itemObj);
            }
            else
            {
                parentBone = weaponTransform[(int)weaponSO.Pos];
                itemObj = Instantiate(weapon.gameObject, parentBone);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * (int)weaponSO.Pos * 180);
                retMeshObjs.Add(itemObj);
            }

        }
        parentsDict.Add(weaponSO.WeaponId, retMeshObjs);
    }


}
