using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DmgUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dealText;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Transform pool;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            DmgShow(MainModule.player.transform.position, Random.Range(10, 50));
    }
    public void DmgShow(Vector3 hitPosition, float dmg)
    {
        dealText.text = $"{dmg}";

        dealText.transform.SetParent(canvas.transform);

        dealText.transform.position = hitPosition;
        dealText.gameObject.SetActive(true);

        float targetPositionY = rectTransform.anchoredPosition.y + 50f;

        dealText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }
    public void Despawn()
    {
        dealText.DOFade(1f, 0f);
        dealText.transform.SetParent(pool);
        dealText.gameObject.SetActive(false);
    }
}
