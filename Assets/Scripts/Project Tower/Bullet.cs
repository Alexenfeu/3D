using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    //private Transform target;
    private GameObject target;
    private float currentLifeTarget;
    private float initialLifeTarget;
    private float ratio;
    public float power = 10f;
    public float speed = 70f;

    public void LookForTarget(GameObject _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            BallOfLife ball = target.GetComponent<BallOfLife>();
            initialLifeTarget = ball.GetInitialLife();
            currentLifeTarget = ball.GetCurrentLife();
            Debug.Log("Position : " + ball.gameObject.transform.position);
            currentLifeTarget -= power;
            ball.SetCurrentLife(currentLifeTarget);
            Debug.Log("Vie : " + ball.GetCurrentLife());
            ratio = currentLifeTarget / initialLifeTarget * 100;
            if (ratio > 0)
            {
                target.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
                //target.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Assets/Resources/Materials/MidLifeSup.mat") as Material);
            }
            if (ratio > 25)
            {
                target.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
                //target.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Assets/Resources/Materials/MidLifeSup.mat") as Material);
            }
            if (ratio > 50)
            {
                target.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
                //target.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Assets/Resources/Materials/MidLifeSup.mat") as Material);
            }
            if (ratio > 75)
            {
                target.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
                //target.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Assets/Resources/Materials/MidLifeSup.mat") as Material);
            }
            float valueBar = ratio/100;
            target.GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>().SetValueWithoutNotify(valueBar);
            Debug.Log(valueBar);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
