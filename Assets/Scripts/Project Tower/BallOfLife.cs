using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfLife : MonoBehaviour
{
    public float vitesse = 15f;

    Transform target;
    int wayPointIndex = 0;
    void Start()
    {
        target = WayPoints.points[0];
    }
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * vitesse * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position, target.position) <= 0.2)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        wayPointIndex++;
        if(wayPointIndex == 4)
        {
            wayPointIndex = 0;
        }
        target = WayPoints.points[wayPointIndex];
    }
}
