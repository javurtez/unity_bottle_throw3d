/*
Made by Melbert Bolocon
*/

using UnityEngine;

public class GameManager : MonoManager<GameManager>
{
    public Transform managerParent;
    public Transform panelParent;

    public Camera uiCamera;

    protected override void Awake()
    {
        instance = this;

        AdManager.Instance.Create();

        SaveLoadManager.Instance.Create();
        PlayerManager.Instance.Create();
        LevelManager.Instance.Create();

        AudioManager.Instance.Create();

        SplashPanel.Instance.Open();
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (ConfirmPanel.Instance.IsActive) return;
            //if (AnnouncePanel.Instance.IsActive) return;
            //if (ItemGivenPanel.Instance.IsActive) return;
            //if (BuySellPanel.Instance.IsActive) return;
            //ConfirmPanel.Instance.Open("Quitting?", "Are you sure you want to exit?", ConfirmQuit);
        }
    }

    private void ConfirmQuit()
    {
        //SaveLoadManager.Instance.SaveData();
        Application.Quit();
    }
}