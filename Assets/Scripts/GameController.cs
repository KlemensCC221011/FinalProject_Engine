using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 checkPointPos;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRb;
    [SerializeField] private GameObject disappearObject;
    private SpriteRenderer disappearSprite;
    private Animator disappearAnimator;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        disappearSprite = disappearObject.GetComponent<SpriteRenderer>();
        disappearSprite.enabled = false;
        disappearAnimator = disappearObject.GetComponent<Animator>();
    }
    void Start()
    {
        checkPointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }   
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    private void Die()
    {
        
        StartCoroutine(Respawn(0.5f));
    }

    private IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;

        disappearSprite.enabled = true;
        disappearAnimator.SetTrigger("DieTrigger");

        yield return new WaitForSeconds(duration);

        transform.position = checkPointPos;
        disappearSprite.enabled = false;


        yield return new WaitForSeconds(0.7f);
        spriteRenderer.enabled = true;
        playerRb.simulated = true;
    }
}
