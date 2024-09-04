using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : MonoBehaviour
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private float speed;
    private Vector3 targetPos;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
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

    public void TogglePlatform()
    {
        isActive = !isActive;
    }

    public void ActivatePlatform()
    {
        isActive = true;

    }

    public void DeactivatePlatform()
    {
        isActive = false;
    }
}
