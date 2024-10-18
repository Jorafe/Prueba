using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthHearts : MonoBehaviour
{
    public int health = 1;
    public PlayerControler playerControler;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerControler = collider.gameObject.GetComponent<PlayerControler>();
            playerControler.PlusHealth(health);
            Destroy(gameObject);

           
        }
    }
}



