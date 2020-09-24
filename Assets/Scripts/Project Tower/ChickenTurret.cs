using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenTurret : MonoBehaviour
{
    private GameObject target;
    public float range = 5f;

    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public GameObject turretPrefab;
    private GameObject turret;
    public GameObject bulletPrefab;
    public Transform firePoint;
    //public Transform partToRotate;
    // Start is called before the first frame update
    void Start()
    {
        turret = (GameObject)Instantiate(turretPrefab, new Vector3(15f, 1.5f, 15f), Quaternion.identity);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] ballsOfLife = GameObject.FindGameObjectsWithTag("BallOfLife");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestBall = null;

        foreach(GameObject ball in ballsOfLife)
        {
            float distanceToBall = Vector3.Distance(turret.transform.position, ball.transform.position);
            if( distanceToBall < shortestDistance)
            {
                shortestDistance = distanceToBall;
                nearestBall = ball;
            }
        }

        if(nearestBall != null && shortestDistance <= range)
        {
            target = nearestBall;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 dir = target.transform.position - turret.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        turret.transform.rotation = Quaternion.Euler(0f, rotation.y + 90, 0f);

        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.LookForTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(turret.transform.position, range);
    }
}
