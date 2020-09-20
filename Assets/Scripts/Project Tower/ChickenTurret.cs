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
    public GameObject bulletPrefab;
    public Transform firePoint;
    //public Transform partToRotate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] ballsOfLife = GameObject.FindGameObjectsWithTag("BallOfLife");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestBall = null;

        foreach(GameObject ball in ballsOfLife)
        {
            float distanceToBall = Vector3.Distance(turretPrefab.transform.position, ball.transform.position);
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

        Vector3 dir = target.transform.position - turretPrefab.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        turretPrefab.transform.rotation = Quaternion.Euler(0f, rotation.y + 90, 0f);

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
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
