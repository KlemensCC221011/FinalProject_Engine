using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private SpriteRenderer plateOn, plateOff;
    [SerializeField] private float speed;
    [SerializeField] private GameObject linkedPlatform;
    private SwitchPlatform switchPlatform;

    private Vector3 targetPos;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;
        plateOff.enabled = true;
        plateOn.enabled = false;
        switchPlatform = linkedPlatform.GetComponent<SwitchPlatform>();


    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            targetPos = posB.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        }
        else
        {
            targetPos = posA.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("MoveableBox"))
        {
            isActive = true;
            plateOff.enabled = false;
            plateOn.enabled = true;
            switchPlatform.ActivatePlatform();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("MoveableBox"))
        {
            isActive = true;
            plateOff.enabled = false;
            plateOn.enabled = true;
            switchPlatform.ActivatePlatform();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("MoveableBox"))
        {
            isActive = false;
            plateOff.enabled = true;
            plateOn.enabled = false;
            switchPlatform.DeactivatePlatform();

        }
    }
}
