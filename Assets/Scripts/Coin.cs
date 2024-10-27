using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    private AudioSource _audioSource;

    private bool interactable;

    public AudioClip coinSound;

    private BoxCollider2D boxCollider;

    void OnTriggerEnter2D(Collider2D collider)
    {
       
        if(collider.gameObject.tag == "Player")
        {  
          SoundManager.instance.PlaySFX(SoundManager.instance._audioSource,SoundManager.instance._CoinAudio);
          Destroy(gameObject);
          GameManager.instance.AddCoins();
        }
    }
}
