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
    private static Dictionary<int, WeaponSO> weapons = new Dictionary<int, WeaponSO>();
    private static Dictionary<int, GameObject> parentsDict = new Dictionary<int, GameObject>();
    
    private List<Transform> retMeshObjs = new List<Transform>();
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
    }
    private void Start()
    {
        SetPool();
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
    private void SetPool()
    {
        parentsDict.Clear();
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].WeaponPrefab == null) continue;

            SetMeshObj(weapons[i]);
            parentsDict.Add(weapons[i].WeaponId, weapons[i].WeaponPrefab);
            parentsDict[weapons[i].WeaponId].SetActive(false);

        }
    }

    public void GetPool()
    {

    }

    public void SetWeaponIdx(int idx) => nowWeaponIdx = idx;

    public void SetNowWeapon()
    {
        nowWeapon = weapons[nowWeaponIdx];
        WeaponSwitch();
    }
    public void WeaponSwitch()
    {
        if(nowWeapon.WeaponPrefab != null)
        {
            //SetMeshItem(nowWeapon.WeaponPrefab);
            parentsDict[nowWeapon.WeaponId].SetActive(true);
        }
        if(mainModule.anim.GetInteger("Jumping") == 0)
            mainModule.TriggerValue = AnimState.Idle;
        
        mainModule.attackMove = 0;

        mainModule.anim.SetInteger(_weapon, (int)nowWeapon.Type);
        mainModule.anim.SetTrigger(_trigger);
        mainModule.anim.SetTrigger(_weaponChange);
    }

    private Transform[] SetMeshObj(WeaponSO weaponSO)
    {
        Transform parentBone = null;

        GameObject meshObj = weaponSO.WeaponPrefab;
        Weapon[] weapons = meshObj.GetComponentsInChildren<Weapon>();

        int index = 1;

        foreach (var weapon in weapons)
        {
            if (weaponSO.Pos == WeaponPos.None) continue;

            else if (weaponSO.Pos == WeaponPos.AllHand)
            {
                GameObject itemObj = Instantiate(weapon.gameObject, weaponTransform[index++]);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * index * 180);
                retMeshObjs.Add(itemObj.transform);
            }
            else
            {
                parentBone = weaponTransform[(int)weaponSO.Pos];
                GameObject itemObj = Instantiate(weapon.gameObject, parentBone);
                itemObj.transform.localRotation = Quaternion.Euler(Vector3.right * (int)weaponSO.Pos * 180);
                retMeshObjs.Add(itemObj.transform);
            }
                
        }

        return retMeshObjs.ToArray();
    }


}
