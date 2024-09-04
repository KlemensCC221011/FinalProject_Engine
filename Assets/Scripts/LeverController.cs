using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leverOn, leverOff;
    [SerializeField] private GameObject linkedPlatform;
    private SwitchPlatform switchPlatform;

    public bool isLeverOn = false;
    private bool canInteract = false;
    private PlayerController playerController;
    private bool canNextFlip = true;

    private void Awake()
    {
        leverOff.enabled = true;
        leverOn.enabled = false;
        
    }
    private void Start()
    {
        switchPlatform = linkedPlatform.GetComponent<SwitchPlatform>();
    }

    private void Update()
    {
        if (canInteract)
        {
            if (playerController.isInteracting)
            {
                FlipLever();
            }
        }
        
    }

    public void FlipLever()
    {
        if (canInteract && canNextFlip)
        {
            canNextFlip = false;
            isLeverOn = !isLeverOn;
            switchPlatform.TogglePlatform();
            leverOff.enabled = !leverOff.enabled;
            leverOn.enabled = !leverOn.enabled;
            StartCoroutine(WaitNextFlip());
        }

    }

    private IEnumerator WaitNextFlip()
    {
        canNextFlip = false;
        yield return new WaitForSeconds(0.8f);
        canNextFlip = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            playerController = collision.gameObject.GetComponent<PlayerController>();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
