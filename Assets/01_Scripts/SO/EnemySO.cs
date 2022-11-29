using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyIdx;

    [Space]

    [Header("�� �̸�")]
    [SerializeField] private string enemyName;
    [Header("����")]
    [SerializeField] private bool isBoss;

    [Space]

    [Header("�� HP")]
    [SerializeField] private float hp;
}
