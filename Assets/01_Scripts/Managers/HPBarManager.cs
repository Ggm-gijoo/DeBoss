using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBarManager : MonoSingleton<HPBarManager>
{ 
    [SerializeField]
    TextMeshProUGUI hpText;
    [SerializeField]
    Image hpBarImage;

    HPModule playerHp;

    private void Start()
    {
        playerHp = MainModule.player.GetComponent<HPModule>();
        SetHPBar();
    }

    public void SetHPBar()
    {
        hpText.text = $"HP : {(int)playerHp.NowHp} / {(int)playerHp.maxHp}";
        hpBarImage.fillAmount = playerHp.NowHp / playerHp.maxHp;
    }

    public void GetDamage(float dmg)
    {
        StartCoroutine(HPDown(playerHp.NowHp + dmg, playerHp.NowHp));
    }

    private IEnumerator HPDown(float preHp, float postHp)
    {
        float curDownHp = preHp;
        while(curDownHp > postHp)
        {
            curDownHp -= 1f;
            hpText.text = $"HP : {(int)curDownHp} / {(int)playerHp.maxHp}";
            hpBarImage.fillAmount = curDownHp / playerHp.maxHp;
            yield return null;
        }

        curDownHp = postHp;
        hpText.text = $"HP : {(int)curDownHp} / {(int)playerHp.maxHp}";
        hpBarImage.fillAmount = curDownHp / playerHp.maxHp;
    }
}
