using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador1 : MonoBehaviour
{
    [SerializeField] float velocidadMovimientoAntesDeteccion;
    [SerializeField] float velocidadMovimientoDespuesDeteccion;
    [SerializeField] float distanciaPararDeteccion;
    [SerializeField] float distanciaParaReducirVelodidad;
    [SerializeField] float distanciaDisparo;
    [SerializeField] float velocidadDisparo;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] GameObject prefabBala;
    [SerializeField] Transform puntoDisparo;

    private Transform playerTransform;
    private float distanciaJugador;
    private bool dejarPerseguir = false;
    private bool reducirVelocidad = false;

    // Start is called before the first frame update
    void Start()    {

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
        Vector3 offset = playerTransform.position - transform.position;
        distanciaJugador = offset.sqrMagnitude;
        //distanciaJugador = Vector3.Distance(transform.position, player.transform.position);

        if (distanciaJugador > distanciaPararDeteccion * distanciaPararDeteccion && !dejarPerseguir)
        {
            //Vector3 objetivo = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            //transform.LookAt(objetivo);
            transform.LookAt(playerTransform);
        }
        else
        {
            dejarPerseguir = true;
            velocidadMovimientoAntesDeteccion = velocidadMovimientoDespuesDeteccion;
        }

        if (distanciaJugador < distanciaParaReducirVelodidad * distanciaParaReducirVelodidad && !reducirVelocidad)
        {
            reducirVelocidad = true;
            velocidadMovimientoAntesDeteccion = velocidadMovimientoDespuesDeteccion;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * velocidadMovimientoAntesDeteccion);
    }

    private IEnumerator Disparar()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadenciaDisparo);

            if (distanciaJugador < distanciaDisparo * distanciaDisparo)
            {
                GameObject go = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
                go.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * velocidadDisparo);
            }
        }
    }
}
