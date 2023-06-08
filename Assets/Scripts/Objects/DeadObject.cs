using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(Constants.PlayerTag))
        {
			if(PlayerManager.Instance.playerObject.isDead)return;
            PlayerManager.Instance.GameOver();
        }
    }
}