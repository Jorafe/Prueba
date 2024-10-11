using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mimico : MonoBehaviour
{

    [SerializeField] private Rigidbody2D enemyRigidBody;
    public static Animator enemyAnimator;

    

    private bool isPlayerInRange = false;

    public Mimico mimico;

    private AudioSource _audioSource;

    [SerializeField] private int healthPoints = 3;
    [SerializeField] private bool playerInRange = false;


    void Start()
    {
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._MimicoAudio);
    }

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            TakeDamage();
        }

    }



public void TakeDamage()
    {
        healthPoints--;

        if(healthPoints <= 0)
        {
          Die();
        }

         SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._MimicoHurtAudio);
       
    }

    void Die()
    {
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._MimicoDieAudio);
        Destroy(gameObject, 0.5f);
    }

}

