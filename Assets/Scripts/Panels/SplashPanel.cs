using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashPanel : BaseStaticPanel<SplashPanel>
{
    public GameObject phaseTwoObject;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI coinText;

    public GameObject tapToStartObj;

    public override void Open()
    {
        base.Open();

        phaseTwoObject.SetActive(true);
        tapToStartObj.SetActive(true);
        UpdateCoin();
        titleText.color = Color.white;

        tapToStartObj.LeanScale(Vector3.one * 1.1f, .3f).setEaseInCirc().setLoopPingPong(-1);

        SettingsPanel.Instance.Open();
    }
    public override void Close()
    {
        phaseTwoObject.SetActive(false);
        tapToStartObj.SetActive(false);
        LeanTween.cancel(tapToStartObj);

        LeanTween.value(gameObject, titleText.color, Color.clear, .5f).
        setOnUpdate((Color c) =>
        {
            titleText.color = c;
        }).
        setOnComplete(base.Close);
        SettingsPanel.Instance.Close();
    }

    public void UpdateCoin()
    {
        coinText.text = PlayerManager.Instance.coins.ToString();
    }

    public void OnBottleSelect()
    {
        BottlePanel.Instance.Open();
    }
}