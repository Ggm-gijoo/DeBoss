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
            "��, �ǲǵ� �θ̳�.", 
            "���� ���� �����δ� ���ظ� ������ ����� �� ������?", 
            "�׷�, �� ����� �� �ܴ��� ��ü�� ���ظ� �ֱ�� ����� �ž�.",
            "�ϴ��� ��� �İ��鼭 ������ ã�ƺ����� ����."
        });
        textData.Add(1, new string[] { 
            "���Ҿ�!",
            "�� �ڰ� �����̾�����.",
            "�� �� ������ �˾Ƴ�����, ���̴� �� ������!" });
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
