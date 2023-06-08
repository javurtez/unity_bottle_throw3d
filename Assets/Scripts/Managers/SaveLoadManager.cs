using UnityEngine;
using System.Collections;

public class SaveLoadManager : MonoManager<SaveLoadManager>
{
    private const string levelData = "level";
    private const string coinsData = "coins";
    private const string bottleData = "bottle";
    private const string selectedBottleData = "selectedbottle";
    private const string audioData = "audio";

    public int GetMute => PlayerPrefs.GetInt(audioData, 1);
    public void SaveAudio(bool isMute)
    {
        PlayerPrefs.SetInt(audioData, isMute ? 0 : 1);
    }

    public int GetSelectedBottle => PlayerPrefs.GetInt(selectedBottleData, 0);
    public void SaveSelectedBottles(int id)
    {
        PlayerPrefs.SetInt(selectedBottleData, id);
    }

    public int GetBottle(int id)
    {
        if (id == 0) return 1;
        return PlayerPrefs.GetInt($"{bottleData}{id}", 0);
    }
    public void SaveBottles(int id)
    {
        for (int index = 0; index < PlayerManager.Instance.bottleDict.Count; index++)
        {
            if (id == index)
            {
                PlayerManager.Instance.bottleDict[id].bottleCurrent = 1;
                PlayerPrefs.SetInt($"{bottleData}{index}", 1);
            }
        }
    }

    public int GetCoins => PlayerPrefs.GetInt(coinsData, 0);
    public void SaveCoins()
    {
        PlayerPrefs.SetInt(coinsData, PlayerManager.Instance.coins);
    }

    public int GetLevel => PlayerPrefs.GetInt(levelData, 1);
    public void SaveLevel()
    {
        PlayerPrefs.SetInt(levelData, LevelManager.Instance.level + 1);
    }
}