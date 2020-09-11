using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStayMurs : MonoBehaviour
{
    public MoveSphere sphere;

    void OnCollisionEnter(Collision col)
    {
        sphere.isImpacted = true;
        sphere.colObject = col.gameObject;
        //Debug.Log("Impact de "+ col.gameObject.name +"!");

    }

    /*void OnCollisionStay(Collision col)
    {
        sphere.isStayed = true;
        sphere.colObject = col.gameObject;
        Debug.Log(col.gameObject.name + " est dedans !");
    }*/

    /*void OnCollisionExit(Collision col)
    {
        sphere.isExited = true;
        Debug.Log("Sortie !");
    }*/
}
