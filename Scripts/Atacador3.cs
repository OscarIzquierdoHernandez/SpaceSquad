using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador3 : MonoBehaviour
{
    [SerializeField] float velocidadDisparo;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] GameObject prefabBala;
    [SerializeField] Transform puntoDisparo;
    [SerializeField] float velocidadRotacion;

    private Transform playerTransform;
    public bool disparar = false;

    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            if (GameObject.Find("Zarco") != null)
            {
                playerTransform = GameObject.Find("Zarco").GetComponent<Transform>();
            }
            else
            {
                playerTransform = GameObject.Find("Raven").GetComponent<Transform>();
            }
        }
        else
        {
            if (GameObject.Find("Raven") != null)
            {
                playerTransform = GameObject.Find("Raven").GetComponent<Transform>();
            }
            else
            {
                playerTransform = GameObject.Find("Zarco").GetComponent<Transform>();
            }
        }

        StartCoroutine("Disparar");
    }

    void Update()
    {
        Quaternion q = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * velocidadRotacion);
    }

    private IEnumerator Disparar()
    {
        while (true)
        {
            if (disparar)
            {
                GameObject go = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
                go.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * velocidadDisparo);

                if (Random.Range(0, 2) == 0)
                {
                    if (GameObject.Find("Zarco") != null)
                    {
                        playerTransform = GameObject.Find("Zarco").GetComponent<Transform>();
                    }
                    else
                    {
                        playerTransform = GameObject.Find("Raven").GetComponent<Transform>();
                    }
                }
                else
                {
                    if (GameObject.Find("Raven") != null)
                    {
                        playerTransform = GameObject.Find("Raven").GetComponent<Transform>();
                    }
                    else
                    {
                        playerTransform = GameObject.Find("Zarco").GetComponent<Transform>();
                    }
                }
            }
            
            yield return new WaitForSeconds(cadenciaDisparo);
        }
    }
}
