using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeUIManager : MonoBehaviour
{
    private bool flag = false;

    private int weaponIdx = 0;

    [SerializeField] private CanvasGroup weaponPanel;
    [SerializeField] private Button[] weaponUIs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !flag && MainModule.player.TriggerValue != AnimState.Jump)
        {
            flag = true;
            StopCoroutine(SetWeaponSwitch());
            StartCoroutine(SetWeaponSwitch());
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && flag)
        {
            flag = false;
        }

        weaponPanel.gameObject.SetActive(flag);
    }

    private IEnumerator SetWeaponSwitch()
    {
        float timer = 0f;
        int h;

        Time.timeScale = 0.005f;

        while (flag)
        {
            timer += Time.unscaledDeltaTime;
            h = (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

            if (Mathf.Abs(h) >= float.Epsilon && timer >= 0.2f)
            {
                timer = 0f;
                weaponIdx = Mathf.Clamp(weaponIdx - h, 0, WeaponModule.parentsDict.Count);
                ChangeWeapon(weaponIdx);
            }

            yield return null;
        }

        Time.timeScale = 1f;
        ChangeWeapon(weaponIdx);
    }

    public void ChangeWeapon(int weaponNum)
    {
        MainModule.player.GetComponent<WeaponModule>().nowWeaponIdx = weaponNum;
        MainModule.player.GetComponent<WeaponModule>().SetNowWeapon();
    }
}
