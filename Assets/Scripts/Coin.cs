using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    public AudioSource source;

    private bool interactable;

    public AudioClip coinSound;

    private BoxCollider2D boxCollider;

    void OnTriggerEnter2D(Collider2D collider)
    {
       
        if(collider.gameObject.tag == "Player")
        {  
          Destroy(gameObject);
          GameManager.instance.AddCoins();
        }
    }
}
