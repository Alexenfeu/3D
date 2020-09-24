using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfLife : MonoBehaviour
{
    public float vitesse = 15f;
    public float life = 100f;
    public float initialLife = 100f;
    public GameObject myPrefab;
    public List<BallOfLife> list = new List<BallOfLife>();

    Transform target;
    int wayPointIndex = 0;

    void Start()
    {
        Debug.Log(myPrefab);
        GameObject sphere = (GameObject)Instantiate(myPrefab, new Vector3(18f, 0.5f, 15f), Quaternion.identity);
        BallOfLife ball = sphere.GetComponent<BallOfLife>();
        Debug.Log(sphere);
        list.Add(ball);
        target = WayPoints.points[0];
    }

    /*void OnGUI()
    {
        if (life > 0)
        {
            
        }
    }*/

    void Update()
    {
        Vector3 position = list[list.Count - 1].transform.position;
        if(this.life > 0f)
        {
            Vector3 direction = target.position - list[list.Count - 1].transform.position;
            //Debug.Log(direction.x + ", " + direction.y + ", " + direction.z);
            list[list.Count - 1].transform.Translate(direction.normalized * vitesse * Time.deltaTime, Space.World);
            if (Vector3.Distance(list[list.Count - 1].transform.position, target.position) <= 0.2)
            {
                GetNextWayPoint();
            }
            Debug.Log("Ma vie est de : " + life);
        }
        else
        {
            Debug.Log("La balle doit disparaitre !");
            Destroy(list[list.Count - 1]);
            StartCoroutine(DestroySphere());
            list.RemoveAt(list.Count - 1);
            GameObject sphere = (GameObject)Instantiate(myPrefab, position, Quaternion.identity);
            BallOfLife ball = sphere.GetComponent<BallOfLife>();
            ball.SetCurrentLife(ball.initialLife);
            list.Add(ball);
        }
              
    }

    IEnumerator DestroySphere()
    {
        yield return new WaitForSeconds(0.5f);
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

    public void SetCurrentLife(float currentLife)
    {
        this.life = currentLife;
    }
}
