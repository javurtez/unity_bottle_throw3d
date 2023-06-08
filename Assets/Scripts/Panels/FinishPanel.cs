using TMPro;
using UnityEngine;

public class FinishPanel : BaseStaticPanel<FinishPanel>
{
    public TextMeshProUGUI progressText;
    public GameObject rewardObject;
    public GameObject tapObject;
    public GameObject tapTextObject;

    private bool canProceed = false;

    private void Update()
    {
        if (!canProceed) return;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!PlayerManager.Instance.playerObject.isDead)
            {
                LevelManager.Instance.NextLevel();
            }
            else
            {
                LevelManager.Instance.ResetLevel();
            }
            Close();
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Ended)
            {
                if (!PlayerManager.Instance.playerObject.isDead)
                {
                    LevelManager.Instance.NextLevel();
                }
                else
                {
                    LevelManager.Instance.ResetLevel();
                }
                Close();
            }
        }
#endif
    }

    public override void Open()
    {
        base.Open();

        progressText.text = "";
        tapObject.SetActive(false);
        rewardObject.SetActive(!PlayerManager.Instance.playerObject.isDead);
        rewardObject.transform.localScale = Vector3.zero;
        rewardObject.LeanScale(Vector3.one, .5f).setOnComplete(() =>
        {
            if (PlayerManager.Instance.playerObject.isDead)
            {
                progressText.text = $"{LevelProgresPanel.Instance.progressImage.fillAmount * 100:0}% Completed";
            }
            else
            {
                progressText.text = "100% Completed";
            }
            tapObject.SetActive(true);
            tapObject.transform.localScale = Vector3.zero;
            tapObject.LeanScale(Vector3.one, .25f).setOnComplete(() =>
            {
                canProceed = true;
                tapTextObject.LeanScale(Vector3.one * 1.1f, .3f).setEaseInCirc().setLoopPingPong(-1);
            });
        });

        if (Technical.RandomPercentage() <= 35)
        {
            AdManager.Instance.OnInterstitialShow();
        }
        else
        {
            RateManager.Instance.ShowRate();
        }
    }
    public override void Close()
    {
        base.Close();

        canProceed = false;
        rewardObject.SetActive(false);
        tapObject.SetActive(false);
        LeanTween.cancel(tapTextObject);
    }
}