using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    public int vitesse;
    public int dureeBeforeDestroy;
    public GameObject myPrefab;
    public GameObject colObject;
    public AudioClip ping;
    public AudioClip swoosh;
    public bool isImpacted = false;
    public bool isStayed = false;
    public bool isExited = false;
    public Rigidbody r;
    public List<GameObject> list = new List<GameObject>();
    int couleur = 0;
    int numSphere = 1;
    bool initialIsRemoved = false;

    void Start()
    {
        GameObject sphere = Instantiate(myPrefab, new Vector3(0, 5, 0), Quaternion.identity);
        sphere.GetComponentInChildren<Rigidbody>().velocity = new Vector3(1 * vitesse, 0.8f * vitesse, 0.8f * vitesse);
        sphere.GetComponentInChildren<Rigidbody>().AddForce(sphere.GetComponentInChildren<Rigidbody>().velocity, ForceMode.Force);
        sphere.GetComponentInChildren<AudioSource>().PlayOneShot(ping);
        sphere.name = "Initial Sphere";
        list.Add(sphere);
    }

    void Update()
    {
        if (initialIsRemoved == false)
        {
            foreach (GameObject elt in list)
            {
                if (list.Count > 1 && elt.name.Equals("Initial Sphere"))
                {
                    initialIsRemoved = true;
                    StartCoroutine(SphereDestroyed(elt));
                }
            }
        }
        else
        {
            foreach (GameObject elt in list)
            {
                if (list.Count > 1 && elt == list[0])
                {
                    StartCoroutine(SphereDestroyed(elt));
                }
            }
        }

        if (isImpacted == true)
        {
            int modulo = couleur % 4;
            switch (modulo)
            {
                case 0:
                    colObject.GetComponentInChildren<Light>().color = Color.red;
                    break;
                case 1:
                    colObject.GetComponentInChildren<Light>().color = Color.blue;
                    break;
                case 2:
                    colObject.GetComponentInChildren<Light>().color = Color.magenta;
                    break;
                case 3:
                    colObject.GetComponentInChildren<Light>().color = Color.yellow;
                    break;
            }
            int i = Random.Range(-10, 10);
            int j = Random.Range(-10, 10);
            int k = Random.Range(-10, 10);
            colObject.GetComponentInChildren<Rigidbody>().velocity = new Vector3(i * vitesse, j * vitesse, k * vitesse);
            colObject.GetComponentInChildren<Rigidbody>().AddForce(colObject.GetComponentInChildren<Rigidbody>().velocity, ForceMode.Force);
            isImpacted = false;
            couleur++;
        }

        if (Input.GetButtonDown("Pop"))
        {
            Vector3 position = list[list.Count-1].GetComponentInChildren<Rigidbody>().transform.position;
            Debug.Log("X : " + position.x.ToString() + ", Y : " + position.y.ToString() + ", Z : " + position.z.ToString());
            GameObject moveSphere = Instantiate(myPrefab, position, Quaternion.identity);
            moveSphere.GetComponentInChildren<Rigidbody>().velocity = new Vector3(list[list.Count - 1].GetComponentInChildren<Rigidbody>().position.x * (-1), list[list.Count - 1].GetComponentInChildren<Rigidbody>().position.y * (-1), list[list.Count - 1].GetComponentInChildren<Rigidbody>().position.z * (-1));
            moveSphere.GetComponentInChildren<Rigidbody>().AddForce(moveSphere.GetComponentInChildren<Rigidbody>().velocity, ForceMode.Force);
            moveSphere.GetComponentInChildren<AudioSource>().PlayOneShot(ping);
            moveSphere.name = "Sphere " + numSphere;
            Debug.Log(moveSphere.name + " ajoutée !");
            list.Add(moveSphere);
            numSphere++;
            StartCoroutine(SphereDestroyed(moveSphere));
        }

        IEnumerator SphereDestroyed(GameObject g)
        {
            if (list.Count > 1 && initialIsRemoved == true && g != list[list.Count - 1])
            {
                list.RemoveAt(list.IndexOf(g));
                yield return new WaitForSeconds(dureeBeforeDestroy);
                GameObject.Destroy(g.gameObject.transform.GetChild(0));
                Debug.Log("Enfance détruite !");
                g.GetComponent<AudioSource>().PlayOneShot(swoosh);
                yield return new WaitForSeconds(1);
                Destroy(g);
                Debug.Log("Parent détruit !");
            }
        }
    }
}
