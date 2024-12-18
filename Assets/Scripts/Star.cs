using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    private bool interactable;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable)
        {

            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource,SoundManager.instance._starAudio);
            
            GameManager.instance.AddStars();

            Destroy(gameObject);
        }        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            interactable = false;
        }
    }
}
