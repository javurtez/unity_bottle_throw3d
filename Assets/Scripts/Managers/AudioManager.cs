using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoManager<AudioManager>
{
    [SerializeField] private AudioClip flipClip = null;
    [SerializeField] private AudioClip coinClip = null;
    [SerializeField] private AudioClip[] collisionClip = null;

    private AudioSource audioSource;
    public AudioSource winAudioSource;

    public float Volume => audioSource.volume;

    protected override void Start()
    {
        TryGetComponent(out audioSource);
    }

    public void Sound()
    {
        audioSource.volume = 1;
        winAudioSource.volume = 1;
    }
    public void Mute()
    {
        audioSource.volume = 0;
        winAudioSource.volume = 0;
    }

    public void PlayWin()
    {
        winAudioSource.Play();
    }
    public void StopWin()
    {
        winAudioSource.Stop();
    }

    public void PlayCoin()
    {
        audioSource.PlayOneShot(coinClip, audioSource.volume);
    }
    public void PlayFlip()
    {
        audioSource.PlayOneShot(flipClip, audioSource.volume);
    }
    public void PlayCollision()
    {
        audioSource.PlayOneShot(collisionClip.RandomIndex(), audioSource.volume);
    }
    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;
        audioSource.PlayOneShot(clip, audioSource.volume);
    }
}