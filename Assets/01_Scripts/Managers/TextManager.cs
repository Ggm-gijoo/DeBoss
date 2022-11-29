using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;

    private Dictionary<int, string> textData = new Dictionary<int, string>();

    public void Awake()
    {
        DoDialogue("테스트 <color=#ff00ff>테스트</color> <size=150%>테스트</size>");
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DoDialogue("테스트 <color=#ff00ff>테스트</color> <size=150%>테스트</size>");
        }
    }
    public void DoDialogue(string text)
    {
        textDialogue.text = text;
        textDialogue.maxVisibleCharacters = 0;
        DOTween.To(x => textDialogue.maxVisibleCharacters = (int)x, 0f, text.Length, 5f);
    }

}
