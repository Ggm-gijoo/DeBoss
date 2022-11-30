using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus",menuName ="Scriptables")]
public class WeaponSO : ScriptableObject
{
    [Header("ID number")]
    [SerializeField] private int weaponId = 0;
    [Header("Weapon Prefab")]
    [SerializeField] private GameObject weaponModel = null;
    
    [Space]

    [Header("무기 이름")]
    [SerializeField] private string weaponName = "";
    [TextArea(8, 1)]
    [Header("무기 설명")]
    [SerializeField] private string description = "";
    [Header("무기 종류")]
    [SerializeField] WeaponType type = WeaponType.None;
    [Header("장착 위치")]
    [SerializeField] WeaponPos pos = WeaponPos.None;

    [Space]

    [Header("공격 속도")]
    [SerializeField] private AttackType speed = AttackType.Middle;
    [Header("공격 대미지")]
    [SerializeField] private float dmg = 5f;
    [Header("크리티컬 확률")]
    [Range(0, 100)]
    [SerializeField] private float crit = 8f;
    [Header("공격 애니메이션 개수")]
    [SerializeField] private int attackMoveCount = 3;


    public int WeaponId => weaponId;
    public GameObject WeaponPrefab => weaponModel;
    public string WeaponName => weaponName;
    public string Description => description;
    public WeaponType Type => type;
    public WeaponPos Pos => pos;
    public AttackType Speed => speed;
    public float Dmg => dmg;
    public float Crit => crit;
    public int AtkMoveCount => attackMoveCount;


}
public enum WeaponType
{
    None,
    Sword,
    Wand,
    DualBlade
}
public enum AttackType
{
    Light,
    Middle,
    Heavy,
}