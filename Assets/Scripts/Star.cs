using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    private bool interactable;
    private BoxCollider2D boxCollider;

    public static GameManager Instance { get; private set; }

    public HUD hud;

    public GameManager _gameManager;

   public int estrellas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable)
        {
            
            Destroy(gameObject);
            
            HUD.instance.ActivarMoneda(1);

            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource,SoundManager.instance._starAudio);
 
        }        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            hud = hud.GetComponent<HUD>();
            interactable = true;
            //_gameManager.AddStars();  
            
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            interactable = false;
        }
    }

     public void ConseguirEstrellas()
   {
        if (estrellas == 3)
        {
            return;
        }
        else
        {
            hud.ActivarMoneda(estrellas);
            estrellas += 1;
        }
        
   }
}
