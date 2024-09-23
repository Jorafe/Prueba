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
        horizontalInput = Input.GetAxis("Horizontal");
        
        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true);
          
        }
       
        else if(horizontalInput > 0)
        {
           transform.rotation = Quaternion.Euler(0, 0, 0);
           characterAnimator.SetBool("IsRunning", true); 
           
        }
        else 
        {
            characterAnimator.SetBool("IsRunning", false);
            
        }
        

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded == true)
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            characterAnimator.SetBool("IsJumping", true);
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);

    }

    void TakeDamage()
    {
        healthPoints--;
        characterAnimator.SetTrigger("IsHurt");
       
        if(healthPoints == 0)
        {
          Die();
        }
    }

    void Die()
    {
        characterAnimator.SetBool("IsDead", true);
        Destroy(gameObject, 0.40f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.layer == 8)
        {
            TakeDamage();
        }
    }
}
