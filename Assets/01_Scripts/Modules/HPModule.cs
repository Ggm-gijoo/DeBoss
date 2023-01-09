using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModule : MonoBehaviour
{
    [Header("HP")]
    [Range(10, 300)]
    public float maxHp = 150f;

    private float nowHp;
    public float NowHp => nowHp;

    private EnemySO enemySO;
    private MainModule mainModule;

    private void Start()
    {
        mainModule = GetComponent<MainModule>();

        if (MainModule.player == mainModule)
            nowHp = maxHp;
        else
        {
            enemySO = MainModule.boss.GetComponent<EnemyDefault>().enemySO;
            nowHp = enemySO.Hp;
        }
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

        if(mainModule == MainModule.player) HPBarManager.Instance.GetDamage(dmg);
    }
}
