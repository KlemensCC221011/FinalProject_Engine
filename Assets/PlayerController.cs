using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movement;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    private bool isGrounded = false;

    
    private Rigidbody2D rigidbodyPlayer;
    private Animator animator;




    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //HandleInteraction()
        HandleGravity();
    }

    public void HandleGravity()
    {

        if (rigidbodyPlayer.velocity.y <= 0)
        {
            rigidbodyPlayer.velocity += Vector2.up * Physics.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if (rigidbodyPlayer.velocity.y > 0)
        {
            rigidbodyPlayer.velocity += Vector2.up * Physics.gravity.y * Time.deltaTime;

        }
    }

    public void HandleMovement()
    {
        movement = Input.GetAxis("Horizontal");
        rigidbodyPlayer.velocity = new Vector2(movement * speed, rigidbodyPlayer.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rigidbodyPlayer.velocity.x));
        animator.SetFloat("yVelocity", rigidbodyPlayer.velocity.y);


        if (movement != 0)
        {
            //change

            if (movement > 0)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);

            }
        }
        if (Input.GetButtonDown("Jump") && isGrounded){
            Jump();
        }
    }

    public void Jump()
    {
        rigidbodyPlayer.velocity = new Vector2(rigidbodyPlayer.velocity.x, jumpSpeed);
        isGrounded = false;
        animator.SetBool("isJumping", !isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;  
            animator.SetBool("isJumping", !isGrounded);
        }
        

    }

}
