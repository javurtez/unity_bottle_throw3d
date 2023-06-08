using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgresPanel : BaseStaticPanel<LevelProgresPanel>
{
    public TextMeshProUGUI currentLevelText, nextLevelText;
    public Image progressImage;

    private bool isReset = false;

    private void LateUpdate()
    {
        if (LevelManager.Instance.isFinishLevel)
        {
            if (!isReset)
            {
                isReset = true;
                LeanTween.value(progressImage.gameObject, progressImage.fillAmount, 1, .2f).
                setOnUpdate((float f)=>
                {
                    progressImage.fillAmount = f;
                });
            }
            return;
        }
        progressImage.fillAmount = LevelManager.Instance.GetLevel().Progress;
    }

    public override void Open()
    {
        base.Open();

        isReset = false;
        currentLevelText.text = LevelManager.Instance.level.ToString();
        nextLevelText.text = (LevelManager.Instance.level + 1).ToString();
    }
    public override void Close()
    {
        base.Close();

        progressImage.fillAmount = 0;
    }
}