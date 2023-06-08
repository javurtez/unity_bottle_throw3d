using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrapObject : MonoBehaviour
{
    public float rotateZ = -45;
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(Constants.PlayerTag))
        {
            LeanTween.rotate(gameObject, new Vector3(0, 0, rotateZ), .5f);
        }
    }
}