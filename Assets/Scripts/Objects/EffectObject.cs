using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EffectObject : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(Constants.PlayerTag))
        {
            if (animator)
            {
                animator.SetTrigger("Collide");
            }
            if (audioSource)
            {
                audioSource.volume = AudioManager.Instance.Volume;
                audioSource.Play();
            }
        }
    }
}