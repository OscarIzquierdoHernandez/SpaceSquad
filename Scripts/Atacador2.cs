using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador2 : MonoBehaviour
{
    /*[SerializeField] float velocidadMovimientoAntesDeteccion;
    [SerializeField] float velocidadMovimientoDespuesDeteccion;
    [SerializeField] float distanciaPararDeteccion;
    [SerializeField] float distanciaParaReducirVelodidad;*/
    [SerializeField] float tiempoHastaComienzo;
    [SerializeField] float velocidadDisparo;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] float velocidadRotacion = 100;
    [SerializeField] GameObject prefabBala;
    [SerializeField] Transform puntoDisparo;

    private Transform playerTransform;
    public bool disparar = true;

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

        Invoke("EmpezarDisparar", tiempoHastaComienzo);
    }

    private void EmpezarDisparar()
    {
        StartCoroutine("Disparar");
        StartCoroutine("CambiarJugador");
    }

    void Update()
    {
        //float y = playerTransform.position.y;
        //playerTransform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        Quaternion q = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * velocidadRotacion);
        //playerTransform.position = new Vector3(playerTransform.position.x, y, playerTransform.position.z);
    }

    private IEnumerator Disparar()
    {
        while (true)
        {
            if (disparar)
            {
                //puntoDisparo.LookAt(playerTransform);
                GameObject go = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
                go.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * velocidadDisparo);
            }

            yield return new WaitForSeconds(cadenciaDisparo);
        }
    }

    private IEnumerator CambiarJugador()
    {
        while (true)
        {
            yield return new WaitForSeconds((cadenciaDisparo * 2));

            if (disparar)
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
            }
        }
    }
}
