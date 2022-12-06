using System;
using UnityEngine;
using UnityEngine.VFX;

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
    [SerializeField] WeaponType type = WeaponType.Knuckle;
    [Header("���� ��ġ")]
    [SerializeField] WeaponPos pos = WeaponPos.None;

    [Space]

    [Header("���� �����")]
    [SerializeField] private float dmg = 5f;
    [Header("ũ��Ƽ�� Ȯ��")]
    [Range(0, 100)]
    [SerializeField] private float crit = 8f;
    [Header("���� �ִϸ��̼� ����")]
    [SerializeField] private int attackMoveCount = 3;
    [Header("���� ����Ʈ")]
    [SerializeField] private VisualEffectAsset[] vfxs;


    public int WeaponId => weaponId;
    public GameObject WeaponPrefab => weaponModel;
    public string WeaponName => weaponName;
    public string Description => description;
    public WeaponType Type => type;
    public WeaponPos Pos => pos;
    public float Dmg => dmg;
    public float Crit => crit;
    public int AtkMoveCount => attackMoveCount;
    public VisualEffectAsset[] Vfxs => vfxs;


}
public enum WeaponType
{
    Knuckle,
    Sword,
    Wand,
    DualBlade,
    GreatSword,
    Spear
}