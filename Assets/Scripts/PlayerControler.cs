using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    private float horizontalInput;


    public static Animator characterAnimator;
    
    [SerializeField]private float characterSpeed = 4.5f;

    [SerializeField] private float jumpForce = 100f;

    [SerializeField] public int maxHealthPoints = 5;
    [SerializeField] public int currentHealthPoints;



    private bool isAttacking;

    private AudioSource _audioSource;

    [SerializeField] private Transform attackHitBox;
    [SerializeField] private float attackRadius;
    

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
        //SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._HurtAudio);

        currentHealthPoints = maxHealthPoints;

        GameManager.instance.SetHealthSlider(maxHealthPoints);
    }

    void Update()
    {
        Movimiento();

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        
        }

         if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded & !isAttacking)
        {
            StartAttack();
            
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);

    }

    void Movimiento()
    {
        if(horizontalInput == 0 && isAttacking)
         {
            horizontalInput = 0;
          
         }
         else
         {
            horizontalInput = Input.GetAxis("Horizontal");
         }
         

         if(horizontalInput == 0)
         {
            characterAnimator.SetBool("IsRunning", false);
         }

         
         else if(horizontalInput < 0)
         {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true); 
         }
          else if(horizontalInput > 0)
         {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true); 
         }

         
        
       /* if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }
        characterAnimator.SetBool("IsRunning", true);

        else if(horizontalInput > 0)
        {
           transform.rotation = Quaternion.Euler(0, 0, 0);
           characterAnimator.SetBool("IsRunning", true); 
           
        }
        else 
        {
            characterAnimator.SetBool("IsRunning", false);
            
        }*/
        
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        characterAnimator.SetBool("IsJumping", true);
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._jumpAudio);
    }

    /*void Attack()
    {
        StartCoroutine(AttackAnimation());
        characterAnimator.SetTrigger("Attack");
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._AtackAudio);
    }

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.1f);

        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag ("Mimico"))
            {
                enemy.GetComponent<Mimico>().TakeDamage();
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(0.4f);

        isAttacking = false;
    }*/

    void StartAttack()
    {
        isAttacking = true;
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._AtackAudio);
        characterAnimator.SetTrigger("Attack");
    }
    void Attack()
    {
        Debug.Log("Atake iniciado");
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        EndAttack();
        foreach(Collider2D Enemy in collider)
        {
            if(Enemy.gameObject.tag == "Mimico")
            {
                Debug.Log("enemigo detectado");
                Enemy.GetComponent<Mimico>().TakeDamage();
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = Enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                Mimico mimico = Enemy.GetComponent<Mimico>();

            }
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;

        GameManager.instance.UpdateHealthSlider(currentHealthPoints);

        if(currentHealthPoints <= 0)
        {
          Die();
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
        }

       SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._HurtAudio);
        
    }

    public void PlusHealth(int health)
    {
        currentHealthPoints += health;

        GameManager.instance.UpdateHealthSlider(currentHealthPoints);
    }

    void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        SoundManager.instance.PlaySFX(_audioSource,SoundManager.instance._DieAudio);
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.layer == 8)
        {
            TakeDamage(1);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
}