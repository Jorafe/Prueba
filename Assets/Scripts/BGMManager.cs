using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
   public static BGMManager instance;

   public AudioSource _audioSource;

   public AudioClip _SoundTrak;

   void Awake()
   {
       if(instance != null && instance != this)
       {
           Destroy(gameObject);
       }
       else
       {
           instance = this;
          
       }

       _audioSource = GetComponent<AudioSource>();
   }

   public void PlayBGM(AudioClip clip)
   {
        _audioSource.clip = clip;
        _audioSource.Play();
   }
}
