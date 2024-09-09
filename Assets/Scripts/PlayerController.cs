using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movement;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    public bool facingRight = true;
    private bool isGrounded = false;
    [SerializeField] private LayerMask isGround;

    
    private Rigidbody2D rigidbodyPlayer;
    private Animator animator;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;

    private CameraFollowObject _cameraFollowObject;
    [SerializeField] private GameObject _cameraFollowGO;

    public bool isInteracting;

    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
        HandleGravity();
        CollisionCheck();
        HandleAnimator();

    }

    private void HandleInteraction()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SceneController.instance.pauseMenu.isPaused)
            {
                SceneController.instance.pauseMenu.PauseGame();
            }
            else
            {
                SceneController.instance.pauseMenu.ResumeGame();

            }
        }
    }

    private void HandleAnimator()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rigidbodyPlayer.velocity.x));
        animator.SetFloat("yVelocity", rigidbodyPlayer.velocity.y);
        animator.SetBool("isJumping", !isGrounded);

        animator.SetBool("isWallSliding", isWallSliding);
    }

    private void FixedUpdate()
    {

        if(isWallDetected && canWallSlide)
        {
            isWallSliding = true;
            rigidbodyPlayer.velocity = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.y * 0.75f);
        }
        else
        {
            isWallSliding = false;
            rigidbodyPlayer.velocity = new Vector2(movement * speed, rigidbodyPlayer.velocity.y);
        }


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
        movement = Input.GetAxisRaw("Horizontal");
        


        if (movement != 0)
        {
            //change

            if (movement > 0 && !facingRight)
            {
                Flip();
            }
            else if(movement < 0 && facingRight)
            {
                Flip();

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && rigidbodyPlayer.velocity.y <= 0.1f)
        {
            isGrounded = true;  
            isWallSliding = false;
            //animator.SetBool("isJumping", !isGrounded);
        }
        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && rigidbodyPlayer.velocity.y <= 0.1f)

        {
            isGrounded = true;
            isWallSliding = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            isWallSliding = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        wallCheckDistance *= -1;
        transform.Rotate(0, 180, 0);

        _cameraFollowObject.CallTurn();
    }

    private void CollisionCheck()
    {
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, isGround);
        if (!isGrounded && rigidbodyPlayer.velocity.y < 0)
        {
            canWallSlide = true;
        }
        else
        {
            canWallSlide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
