using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStayMurs : MonoBehaviour
{
    public MoveSphere sphere;
    void OnCollisionEnter(Collision col)
    {
        sphere.isImpacted = true;
        Debug.Log("Impact !");

    }

    /*void OnCollisionStay(Collision col)
    {
        sphere.isExited = true;
        Debug.Log("Dedans !");
    }*/

    void OnCollisionExit(Collision col)
    {
        //sphere.isExited = true;
        Debug.Log("Sortie !");
    }
}
