using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthHearts : MonoBehaviour
{
    public int health = 1;
    public PlayerControler playerControler;

    private AudioSource _audioSource;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerControler = collider.gameObject.GetComponent<PlayerControler>();

            //if(playerControler._currentHealthPoints + health > playerControler._maxHealthPoints)
            {
                playerControler.PlusHealth(health);
                SoundManager.instance.PlaySFX(SoundManager.instance._audioSource,SoundManager.instance._HealthAudio);
                Destroy(gameObject); 
            }
            
        }
    }
}



