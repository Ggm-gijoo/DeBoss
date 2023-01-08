using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class BossUIManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI bossNameText;
    [SerializeField]private Image[] bossHPBar;
    [SerializeField]private CanvasGroup bossUICanvas;

    private EnemySO bossSO;
    private void Start()
    {
        if (!IsBossAppear())
        {
            bossUICanvas.gameObject.SetActive(false);
            return;
        }
        bossSO = MainModule.boss.GetComponent<EnemyDefault>().enemySO;
        BossNameUI();
    }
    private void Update()
    {
        if(IsBossAppear() && bossUICanvas.alpha <= 0)
        {
            bossUICanvas.DOFade(1, 0.5f);
            BossNameUI();
            BossHPBarUpdate();
        }
        else if(!IsBossAppear() && bossUICanvas.alpha > 0)
        {
            bossUICanvas.DOFade(0, 0.5f);
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
        foreach (var bHp in bossHPBar)
        {
            float UiX = bHp.rectTransform.sizeDelta.x;
            float UiY = bHp.rectTransform.sizeDelta.y;

            bHp.rectTransform.sizeDelta = new Vector2(0f, UiY);
            bHp.rectTransform.DOSizeDelta(new Vector2(UiX, UiY), 0.5f);
        }
    }
}
