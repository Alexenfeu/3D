using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    public int vitesse;
    public GameObject myPrefab;
    public GameObject colObject;
    public bool isImpacted = false;
    public bool isStayed = false;
    public Rigidbody r;
    public List<GameObject> list = new List<GameObject>();

    void Start()
    {
        GameObject sphere = Instantiate(myPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        sphere.GetComponent<Rigidbody>().velocity = new Vector3(1 * vitesse, 0.8f * vitesse, 0.8f * vitesse);
        sphere.GetComponent<Rigidbody>().AddForce(sphere.GetComponent<Rigidbody>().velocity, ForceMode.Force);
        sphere.name = "Initial Sphere";
        list.Add(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        //|| isStayed == true
        if (isImpacted == true)
        {
            int i = Random.Range(-10, 10);
            int j = Random.Range(-10, 10);
            int k = Random.Range(-10, 10);
            colObject.GetComponent<Rigidbody>().velocity = new Vector3(i * vitesse, j * vitesse, k * vitesse);
            colObject.GetComponent<Rigidbody>().AddForce(colObject.GetComponent<Rigidbody>().velocity, ForceMode.Force);
            //this.myPrefab.GetComponent<Rigidbody>().AddExplosionForce(1.0f, this.myPrefab.GetComponent<Rigidbody>().position, 1.0f);
            isImpacted = false;
        }

        /*if (isStayed == true)
        {
            colObject.GetComponent<Rigidbody>().velocity = new Vector3(colObject.GetComponent<Rigidbody>().position.x * (-1), colObject.GetComponent<Rigidbody>().position.y * (-1), colObject.GetComponent<Rigidbody>().position.z * (-1));
            colObject.GetComponent<Rigidbody>().AddForce(colObject.GetComponent<Rigidbody>().velocity, ForceMode.Force);
            isStayed = false;
        }*/

        //Input.GetButtonDown("Pop") ||
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 position = list[list.Count-1].GetComponent<Rigidbody>().transform.position;
            GameObject moveSphere = Instantiate(myPrefab, position, Quaternion.identity);
            moveSphere.GetComponent<Rigidbody>().velocity = new Vector3(list[list.Count - 1].GetComponent<Rigidbody>().position.x * (-1), list[list.Count - 1].GetComponent<Rigidbody>().position.y * (-1), list[list.Count - 1].GetComponent<Rigidbody>().position.z * (-1));
            moveSphere.GetComponent<Rigidbody>().AddForce(moveSphere.GetComponent<Rigidbody>().velocity, ForceMode.Force);
            moveSphere.name = "Sphere " + list.Count.ToString();
            Debug.Log(moveSphere.name + " ajoutée !");
            //moveSphere.GetComponent<Rigidbody>().AddExplosionForce(1.0f, moveSphere.GetComponent<Rigidbody>().position, 1.0f);
            list.Add(moveSphere);
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
