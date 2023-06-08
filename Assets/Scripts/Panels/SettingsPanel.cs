using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BaseStaticPanel<SettingsPanel>
{
    public Sprite withSoundSprite, muteSprite;
    public Image audioImage;
    public GameObject settingObject;

    private bool isMuted = false;

    public override void Open()
    {
        base.Open();

        LeanTween.delayedCall(.1f, () =>
        {
            isMuted = SaveLoadManager.Instance.GetMute == 0;
            audioImage.sprite = isMuted ? muteSprite : withSoundSprite;

            if (isMuted)
            {
                AudioManager.Instance.Mute();
            }
            else
            {
                AudioManager.Instance.Sound();
            }
        });
    }
    public override void Close()
    {
        base.Close();

        settingObject.SetActive(false);
    }

    public void OnSettings()
    {
        settingObject.SetActive(!settingObject.activeInHierarchy);
    }
    public void OnMute()
    {
        isMuted = !isMuted;
        SaveLoadManager.Instance.SaveAudio(isMuted);
        audioImage.sprite = isMuted ? muteSprite : withSoundSprite;

        if (isMuted)
        {
            AudioManager.Instance.Mute();
        }
        else
        {
            AudioManager.Instance.Sound();
        }
    }
    public void OnPrivacy()
    {
        Application.OpenURL("https://trebertgames.wordpress.com/privacy/");
    }
}