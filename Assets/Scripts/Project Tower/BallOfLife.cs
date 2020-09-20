using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfLife : MonoBehaviour
{
    public float vitesse = 15f;
    public float life = 100f;
    public float initialLife = 100f;
    public GameObject myPrefab;
    public List<GameObject> list = new List<GameObject>();

    Transform target;
    int wayPointIndex = 0;
    void Start()
    {
        GameObject sphere = Instantiate(myPrefab, new Vector3(18f, 0.5f, 15f), Quaternion.identity);
        list.Add(sphere);
        target = WayPoints.points[0];
    }
    void Update()
    {
        Vector3 direction = target.position - list[0].transform.position;
        //Debug.Log(direction.x + ", " + direction.y + ", " + direction.z);
        list[0].transform.Translate(direction.normalized * vitesse * Time.deltaTime, Space.World);
        if(Vector3.Distance(list[0].transform.position, target.position) <= 0.2)
        {
            GetNextWayPoint();
        }

        /*Bullet lifeTarget = list[0].GetComponent<Bullet>();
        Debug.Log(lifeTarget);
        if (lifeTarget != null)
        {
            lifeTarget.LookForInitialLife(initialLife);
        }

        if (lifeTarget != null)
        {
            lifeTarget.LookForCurrentLife(life);
        }*/
        


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

    public float GetInitialLife()
    {
        return initialLife;
    }

    public float GetCurrentLife()
    {
        return life;
    }

    public List<GameObject> GetListOfBallsOfLife()
    {
        return list;
    }

    public float SetCurrentLife(float currentLife)
    {
        life = currentLife;
        return life;
    }
}
