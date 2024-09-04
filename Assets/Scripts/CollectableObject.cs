using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private float randomOffset;

    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.time * frequency + randomOffset) * amplitude;

        transform.position = tempPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //set collected ++
            Collect();
        }
    }


    public void Collect()
    {

            Destroy(gameObject);
    }
}
