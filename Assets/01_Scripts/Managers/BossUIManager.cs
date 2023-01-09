using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class BossUIManager : MonoSingleton<BossUIManager>
{
    [SerializeField]private TextMeshProUGUI bossNameText;
    [SerializeField]private Image[] bossHPBar;
    [SerializeField]private CanvasGroup bossUICanvas;

    private EnemySO bossSO;
    private HPModule hpModule;
    private List<float> UiX = new List<float>();

    private void Start()
    {
        if (!IsBossAppear())
        {
            bossUICanvas.alpha = 0;
            return;
        }
        hpModule = MainModule.boss.GetComponent<HPModule>();
        bossSO = MainModule.boss.GetComponent<EnemyDefault>().enemySO;
        BossNameUI();
    }
    private void Update()
    {
        if (!IsBossAppear() && hpModule == null && bossSO == null) return;

        if (IsBossAppear() && hpModule == null && bossSO == null)
        {
            hpModule = MainModule.boss.GetComponent<HPModule>();
            bossSO = MainModule.boss.GetComponent<EnemyDefault>().enemySO;
            BossNameUI();
        }

        bossHPBar[0].fillAmount = hpModule.NowHp / bossSO.Hp;

        if (IsBossAppear() && bossUICanvas.alpha <= 0)
        {
            bossUICanvas.DOFade(1, 0.2f);
            BossNameUI();
            BossHPBarUpdate();
        }
        else if(!IsBossAppear() && bossUICanvas.alpha >= 1)
        {
            BossHPBarUpdate();
            bossUICanvas.DOFade(0, 0.4f);
        }
    }
    public bool IsBossAppear()
    {
        return MainModule.boss != null;
    }
    public void BossNameUI()
    {
        bossNameText.text = $"{bossSO.Modifier}\n<size=180%>{bossSO.Name}";
    }
    public void BossHPBarUpdate()
    {

        for (int i = 0; i < bossHPBar.Length; i++)
        {
            if(UiX != null && UiX.Count < bossHPBar.Length)
                UiX.Add(bossHPBar[i].rectTransform.sizeDelta.x);

            if (IsBossAppear())
            {
                bossHPBar[i].rectTransform.sizeDelta = new Vector2(0f, bossHPBar[i].rectTransform.sizeDelta.y);
                bossHPBar[i].rectTransform.DOSizeDelta(new Vector2(UiX[i], bossHPBar[i].rectTransform.sizeDelta.y), 0.5f);
            }
            else
            {
                bossHPBar[i].rectTransform.sizeDelta = new Vector2(UiX[i], bossHPBar[i].rectTransform.sizeDelta.y);
                bossHPBar[i].rectTransform.DOSizeDelta(new Vector2(0f, bossHPBar[i].rectTransform.sizeDelta.y), 0.5f);
            }
        }
    }
}
