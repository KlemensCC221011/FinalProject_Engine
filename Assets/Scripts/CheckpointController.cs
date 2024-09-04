using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    GameController gameController;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private SpriteRenderer checkNoFlag, checkFlag;
    private Collider2D collider;

    private void Awake()
    {
        gameController = GameObject.Find("Player").GetComponent<GameController>();
        checkFlag.enabled = false;
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkFlag.enabled = true;
            checkNoFlag.enabled = false;
            collider.enabled = false;
            gameController.UpdateCheckpoint(respawnPoint.position);
        }
    }
}
