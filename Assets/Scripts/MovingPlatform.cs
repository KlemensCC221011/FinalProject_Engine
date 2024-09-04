using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform posA, posB;
    [SerializeField] private float speed;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, posA.position) < 0.05f) 
        {
            targetPos = posB.position;
        }
        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            playerRb.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
