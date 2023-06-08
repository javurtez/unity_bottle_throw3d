using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ObjectFollow : MonoBehaviour
{
    public float xAddition;
    private Transform toFollow;

    private void Start()
    {
        SetFollow();
    }
    private void FixedUpdate()
    {
        if (!toFollow) return;
        transform.position = new Vector3(toFollow.position.x + xAddition, 2.5f, -2);
    }

    public void SetFollow()
    {
        toFollow = PlayerManager.Instance.playerObject.transform;
    }
}