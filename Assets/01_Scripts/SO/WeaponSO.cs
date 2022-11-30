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

    [Header("���� �̸�")]
    [SerializeField] private string weaponName = "";
    [TextArea(8, 1)]
    [Header("���� ����")]
    [SerializeField] private string description = "";
    [Header("���� ����")]
    [SerializeField] WeaponType type = WeaponType.None;
    [Header("���� ��ġ")]
    [SerializeField] WeaponPos pos = WeaponPos.None;

    [Space]

    [Header("���� �ӵ�")]
    [SerializeField] private AttackType speed = AttackType.Middle;
    [Header("���� �����")]
    [SerializeField] private float dmg = 5f;
    [Header("ũ��Ƽ�� Ȯ��")]
    [Range(0, 100)]
    [SerializeField] private float crit = 8f;
    [Header("���� �ִϸ��̼� ����")]
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