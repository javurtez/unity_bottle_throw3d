using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailObject : MonoBehaviour
{
    private void Update()
    {
        transform.position = PlayerManager.Instance.playerObject.Position;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}