using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(Constants.PlayerTag))
        {
            PlayerManager.Instance.playerObject.Flip();
        }
    }
}