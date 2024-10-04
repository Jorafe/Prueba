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

    [SerializeField] private int healthPoints = 5;

    private bool isAttacking;

    [SerializeField] private Transform attackHitBox;
    [SerializeField] private float attackRadius;
    

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
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
            Attack();
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
            return;
         }
         horizontalInput = Input.GetAxis("Horizontal");

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
    }

    void Attack()
    {
        StartCoroutine(AttackAnimation());
        characterAnimator.SetTrigger("Attack");
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
    }

    void TakeDamage()
    {
        healthPoints--;

        if(healthPoints <= 0)
        {
          Die();
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
        }
        
    }

    void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.layer == 8)
        {
            TakeDamage();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
}