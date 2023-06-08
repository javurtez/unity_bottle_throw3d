using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleSelectSlot : BaseSlot
{
    public int id = -1;
    public GameObject lockObject;
    public Image itemImage;

    public void UnlockBottle()
    {
        lockObject.SetActive(false);
    }

    public void OnSelect()
    {
        if (lockObject.activeInHierarchy) return;

        PlayerManager.Instance.BottleSelect(id);
    }
}