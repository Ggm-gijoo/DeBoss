using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyIdx;

    [Space]

    [Header("수식어")]
    [SerializeField] private string modifier;
    [Header("적 이름")]
    [SerializeField] private string enemyName;
    [Header("보스")]
    [SerializeField] private readonly bool isBoss;

    [Space]

    [Header("적 HP")]
    [SerializeField] private float hp;

    public string Modifier => modifier;
    public string Name => enemyName;
    public bool IsBoss => isBoss;
    public float Hp => hp;
}
