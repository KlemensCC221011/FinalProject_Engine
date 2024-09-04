using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 targetPos;

    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float waitDuration;

    private int pointIndex;
    private int pointCount;
    private int direction = 1;
    private int speedMultiplier = 1;


    private void Awake()
    {
        waypoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            waypoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = waypoints.Length;
        pointIndex = 1;
        targetPos = waypoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speedMultiplier * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if(transform.position == targetPos)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        if(pointIndex == pointCount-1) //Last point arrived
        {
            direction = -1;
        }
        if( pointIndex == 0) // First point arrived
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = waypoints[pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    private IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;

    }
}
