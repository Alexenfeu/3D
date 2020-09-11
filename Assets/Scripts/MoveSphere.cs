using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    public int vitesse;
    public GameObject myPrefab;
    public GameObject colObject;
    public AudioClip ping;
    public bool isImpacted = false;
    public bool isStayed = false;
    public bool isExited = false;
    public Rigidbody r;
    public List<GameObject> list = new List<GameObject>();
    int couleur = 0;

    void Start()
    {
        GameObject sphere = Instantiate(myPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        sphere.GetComponent<Rigidbody>().velocity = new Vector3(1 * vitesse, 0.8f * vitesse, 0.8f * vitesse);
        sphere.GetComponent<Rigidbody>().AddForce(sphere.GetComponent<Rigidbody>().velocity, ForceMode.Force);
        sphere.GetComponent<AudioSource>().PlayOneShot(ping);
        sphere.name = "Initial Sphere";
        list.Add(sphere);
    }

    void Update()
    {
        if (isImpacted == true)
        {
            int modulo = couleur % 4;
            switch (modulo)
            {
                case 0:
                    colObject.GetComponent<Light>().color = Color.red;
                    break;
                case 1:
                    colObject.GetComponent<Light>().color = Color.blue;
                    break;
                case 2:
                    colObject.GetComponent<Light>().color = Color.magenta;
                    break;
                case 3:
                    colObject.GetComponent<Light>().color = Color.yellow;
                    break;
            }
            int i = Random.Range(-10, 10);
            int j = Random.Range(-10, 10);
            int k = Random.Range(-10, 10);
            colObject.GetComponent<Rigidbody>().velocity = new Vector3(i * vitesse, j * vitesse, k * vitesse);
            colObject.GetComponent<Rigidbody>().AddForce(colObject.GetComponent<Rigidbody>().velocity, ForceMode.Force);
            isImpacted = false;
            couleur++;
        }

        if (Input.GetButtonDown("Pop"))
        {
            Vector3 position = list[list.Count-1].GetComponent<Rigidbody>().transform.position;
            GameObject moveSphere = Instantiate(myPrefab, position, Quaternion.identity);
            moveSphere.GetComponent<Rigidbody>().velocity = new Vector3(list[list.Count - 1].GetComponent<Rigidbody>().position.x * (-1), list[list.Count - 1].GetComponent<Rigidbody>().position.y * (-1), list[list.Count - 1].GetComponent<Rigidbody>().position.z * (-1));
            moveSphere.GetComponent<Rigidbody>().AddForce(moveSphere.GetComponent<Rigidbody>().velocity, ForceMode.Force);
            moveSphere.GetComponent<AudioSource>().PlayOneShot(ping);
            moveSphere.name = "Sphere " + list.Count.ToString();
            Debug.Log(moveSphere.name + " ajoutée !");
            list.Add(moveSphere);
        }
    }
}
