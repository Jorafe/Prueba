using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource _audioSource;
    public AudioClip _jumpAudio;

    public AudioClip _AtackAudio;
    public AudioClip _HurtAudio;

    public AudioClip _DieAudio;

    public AudioClip _StopAudio;

    public AudioClip _starAudio;

    public AudioClip _MimicoHurtAudio;

    public AudioClip _MimicoDieAudio;

    public AudioClip _MimicoAudio;

    


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void StarSFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlaySFX(AudioSource source,AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

   
   
    
}
