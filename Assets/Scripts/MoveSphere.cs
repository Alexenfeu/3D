using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    //GameObject sphere = new GameObject();
    public bool isImpacted = false;
    public bool isExited = false;
    public Rigidbody r;

    private void Start()
    {
        this.r.velocity = new Vector3(1, 0.8f, 0.8f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isImpacted == true)
        {
            float i = Random.Range(-10.0f, 10.0f);
            float j = Random.Range(-10.0f, 10.0f);
            float k = Random.Range(-10.0f, 10.0f);
            this.r.velocity = new Vector3(i, j, k);
            this.r.AddForce(this.r.velocity, ForceMode.Force);
            isImpacted = false;
            print("ok");
        }

        /*if (isExited == true)
        {
            float i = Random.Range(-2.0f, 2.0f);
            float j = Random.Range(-2.0f, 2.0f);
            float k = Random.Range(-2.0f, 2.0f);
            this.r.velocity = new Vector3(i, j, k);
            this.r.AddForce(this.r.velocity, ForceMode.Force);
            isExited = false;
            print("ok2");
        }*/
    }
}
