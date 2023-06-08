using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
    public FinishObject finishObject;

    private Vector3 startPosition;
    private Vector3 PlayerPosition => new Vector3(PlayerManager.Instance.playerObject.Position.x, endObject.transform.position.y, PlayerManager.Instance.playerObject.Position.z);
    public float Distance => Vector3.Distance(PlayerPosition, endObject.transform.position);
    public float StartEndDistance => Vector3.Distance(startPosition, endObject.transform.position);
    public float Progress => Mathf.Clamp(1 - (Distance / StartEndDistance), 0, 1);

    public void SetLevel()
    {
        gameObject.SetActive(true);
        PlayerManager.Instance.playerObject.Position = startObject.position;
        PlayerManager.Instance.playerObject.transform.eulerAngles = Vector3.zero;
        startPosition = PlayerPosition;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}