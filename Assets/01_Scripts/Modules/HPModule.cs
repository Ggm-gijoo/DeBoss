using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModule : MonoBehaviour
{
    [Header("플레이어 HP")]
    [Range(10, 300)]
    public float maxHp = 150f;

    private float nowHp;
    public float NowHp => nowHp;

    private void Awake()
    {
        nowHp = maxHp;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Damage(Random.Range(10, 51));
        }
    }

    public void Damage(float dmg)
    {
        nowHp -= dmg;
        HPBarManager.Instance.GetDamage(dmg);
    }
}
