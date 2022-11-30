using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyIdx;

    [Space]

    [Header("���ľ�")]
    [SerializeField] private string modifier;
    [Header("�� �̸�")]
    [SerializeField] private string enemyName;
    [Header("����")]
    [SerializeField] private readonly bool isBoss;

    [Space]

    [Header("�� HP")]
    [SerializeField] private float hp;

    public string Modifier => modifier;
    public string Name => enemyName;
    public bool IsBoss => isBoss;
    public float Hp => hp;
}
