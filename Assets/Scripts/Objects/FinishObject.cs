using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    public ParticleSystem confettiParticle;
    public Vector2 Position => transform.position;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Constants.PlayerTag))
        {
            confettiParticle.gameObject.SetActive(true);
            confettiParticle.Play();
            LevelManager.Instance.FinishLevel();
        }
    }
}