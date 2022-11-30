using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;

    private Dictionary<int, string[]> textData = new Dictionary<int, string[]>();
    private Coroutine textCoroutine = null;
    int idx = 0;

    public void Awake()
    {
        InitTextData();
        if(textCoroutine == null)
            textCoroutine = StartCoroutine(DoDialogue(textData[idx]));
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && textCoroutine == null)
        {
            idx = (idx + 1) % textData.Count;
            textCoroutine = StartCoroutine(DoDialogue(textData[idx]));
        }
    }
    public void InitTextData()
    {
        textData.Add(0, new string[] {
            "윽, 꽁꽁도 싸맸네.", 
            "내가 가진 무기들로는 피해를 입히기 어려울 것 같은데?", 
            "그래, 네 말대로 저 단단한 몸체에 피해를 주기는 어려울 거야.",
            "일단은 계속 파고들면서 약점을 찾아보도록 하자."
        });
        textData.Add(1, new string[] { 
            "좋았어!",
            "등 뒤가 약점이었구나.",
            "한 번 약점을 알아냈으니, 죽이는 건 쉽겠지!" });
    }
    public IEnumerator DoDialogue(string[] texts)
    {
        textDialogue.alpha = 1;
        foreach (var text in texts)
        {
            textDialogue.text = text;
            textDialogue.maxVisibleCharacters = 0;

            DOTween.To(x => textDialogue.maxVisibleCharacters = (int)x, 0f, text.Length + 1, 1.5f);

            yield return new WaitForSeconds(3f);
        }
        textDialogue.DOFade(0, 0.5f);
        //while (textDialogue.alpha > 0)
        //{
        //    textDialogue.alpha -= 0.01f;
        //    yield return null;
        //}
        textCoroutine = null;
    }

}
