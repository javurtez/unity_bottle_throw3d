using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BottleData
{
    public int bottleCurrent = 0;
    public GameObject bottleObjectPrefab;
    public GameObject bottleObjectInScene;

    public void Open()
    {
        if (!bottleObjectInScene) return;
        bottleObjectInScene.SetActive(true);
    }
    public void Close()
    {
        if (!bottleObjectInScene) return;
        bottleObjectInScene.SetActive(false);
    }
}
public class PlayerManager : MonoManager<PlayerManager>
{
    public Dictionary<int, BottleData> bottleDict = new Dictionary<int, BottleData>();
    private int currentBottleId = -1;

    public int coins;

    public PlayerObject playerObject;
    public TrailObject trailObject;

    public bool isSelectingBottle = false;
    public bool hasStarted = false;

    public override void Create()
    {
        playerObject = Instantiate(playerObject);
        trailObject = playerObject.GetComponentInChildren<TrailObject>();
        trailObject.Close();

        coins = SaveLoadManager.Instance.GetCoins;

        for (int i = 0; i < bottleDict.Count; i++)
        {
            if (bottleDict.ContainsKey(i))
            {
                bottleDict[i].bottleCurrent = SaveLoadManager.Instance.GetBottle(i);
            }
        }

        currentBottleId = SaveLoadManager.Instance.GetSelectedBottle;
        BottleSelect(currentBottleId);
    }

    //public int num = 0;
    //[Button("Get Image")]
    //public void GetImage()
    //{
    //        var item = bottleDict[num];
    //        Texture2D tex = UnityEditor.AssetPreview.GetAssetPreview(item.bottleObjectPrefab);

    //        byte[] bytes = tex.EncodeToPNG();
    //        //DestroyImmediate(tex);

    //        File.WriteAllBytes($"C:/Users/JVT/Documents/Project Impossible Bottle 3D/Assets/Sprites/{item.bottleObjectPrefab.name}.png", bytes);
    //}

    public void BottleSelect(int id)
    {
        if (!bottleDict.ContainsKey(id)) return;
        if (bottleDict[id].bottleCurrent == 0) return;

        bottleDict[currentBottleId].Close();
        if (!bottleDict[id].bottleObjectInScene)
        {
            bottleDict[id].bottleObjectInScene = Instantiate(bottleDict[id].bottleObjectPrefab, playerObject.transform);
            bottleDict[id].bottleObjectInScene.transform.localPosition = Vector3.zero;
        }
        bottleDict[id].Open();
        currentBottleId = id;
        SaveLoadManager.Instance.SaveSelectedBottles(currentBottleId);
    }

    public void StartLevel()
    {
        if (hasStarted) return;
        playerObject.isDead = false;
        hasStarted = true;
        trailObject.Open();
        LevelProgresPanel.Instance.Open();
        SplashPanel.Instance.Close();
    }
    public void EndLevel()
    {
        hasStarted = false;
        trailObject.Close();

        AddCoins(25);
    }

    public bool IsEnoughMoney(int coin)
    {
        return coins >= coin;
    }

    public void AddCoins(int coin)
    {
        coins += coin;

        SplashPanel.Instance.UpdateCoin();
        SaveLoadManager.Instance.SaveCoins();
    }
    public void DeductCoins(int coin)
    {
        coins -= coin;

        SplashPanel.Instance.UpdateCoin();
        SaveLoadManager.Instance.SaveCoins();
    }

    public void GameOver()
    {
        playerObject.Dead();
        hasStarted = false;
        FinishPanel.Instance.Open();
    }
}