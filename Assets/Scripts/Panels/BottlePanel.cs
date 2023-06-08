using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePanel : BaseStaticPanel<BottlePanel>
{
    public List<BottleSelectSlot> bottleSelectSlotList = new List<BottleSelectSlot>();

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public override void Open()
    {
        base.Open();

        UpdateBottleList();
        PlayerManager.Instance.isSelectingBottle = true;
    }
    public override void Close()
    {
        base.Close();

        PlayerManager.Instance.isSelectingBottle = false;
    }

    public void UpdateBottleList()
    {
        for (int i = 0; i < bottleSelectSlotList.Count; i++)
        {
            int id = bottleSelectSlotList[i].id;
            if (PlayerManager.Instance.bottleDict.ContainsKey(id) && PlayerManager.Instance.bottleDict[id].bottleCurrent != 0)
            {
                bottleSelectSlotList[i].UnlockBottle();
            }
        }
    }

    public void OnBottleRandom()
    {
        if (!PlayerManager.Instance.IsEnoughMoney(Constants.BottlePrice))
        {
            AdManager.Instance.OnRewardShow();
            return;
        }

        AudioManager.Instance.PlayCoin();

        int maxBottles = PlayerManager.Instance.bottleDict.Count;
        bool isAlreadyFull = true;
        foreach(var dict in PlayerManager.Instance.bottleDict)
        {
            if(dict.Value.bottleCurrent == 0)
            {
                isAlreadyFull = false;
                break;
            }
        }
        if (isAlreadyFull) return;
        for(; ; )
        {
            int random = Random.Range(0, maxBottles);
            int id = bottleSelectSlotList[random].id;
            if (PlayerManager.Instance.bottleDict.ContainsKey(id) && PlayerManager.Instance.bottleDict[id].bottleCurrent == 0)
            {
                bottleSelectSlotList[random].OnSelect();
                SaveLoadManager.Instance.SaveBottles(random);
                break;
            }
        }
        UpdateBottleList();
        PlayerManager.Instance.DeductCoins(Constants.BottlePrice);
    }
}