using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mimico : MonoBehaviour
{

    [SerializeField] private Rigidbody2D enemyRigidBody;
    public static Animator enemyAnimator;

    private bool isPlayerInRange = false;

    public Mimico mimico;

    [SerializeField] private int healthPoints = 3;
    [SerializeField] private bool playerInRange = false;

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
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
       
    }

    void Die()
    {
        Destroy(gameObject, 0.5f);
    }

}

