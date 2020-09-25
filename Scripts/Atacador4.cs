using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador4 : MonoBehaviour
{
    [SerializeField] float velocidadMovimiento;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] int dano;
    [SerializeField] Transform puntoDisparo;

    public bool disparar = false;

    private Transform playerTransform;
    private bool rotar = true;
    private int contador = 0;

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
        if (disparar)
        {
            Vector3 destino = new Vector3(playerTransform.position.x, (playerTransform.position.y + 4), transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destino, Time.deltaTime * velocidadMovimiento);

            if (rotar)
            {
                if (transform.rotation.z > 0.1f)
                {
                    transform.Rotate(0, 0, -velocidadMovimiento * Time.deltaTime);
                }
                else if (transform.rotation.z < -0.1f)
                {
                    transform.Rotate(0, 0, velocidadMovimiento * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    rotar = false;
                }
            }
        }
    }

    private IEnumerator Disparar()
    {
        while (true)
        {
            if (disparar)
            {
                RaycastHit hit;

                if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit))
                {
                    if (hit.collider && hit.collider.gameObject.CompareTag("Player"))
                    {
                        hit.collider.gameObject.GetComponent<Player>().RecibirDano(dano);
                    }
                }
            }

            if (contador == 50)
            {
                contador = 0;

                if (playerTransform.gameObject.name == "Zarco")
                {
                    if (GameObject.Find("Raven") != null)
                    {
                        playerTransform = GameObject.Find("Raven").GetComponent<Transform>();
                    }
                }
                else
                {
                    if (GameObject.Find("Zarco") != null)
                    {
                        playerTransform = GameObject.Find("Zarco").GetComponent<Transform>();
                    }
                }
            }
            else
            {
                contador++;
            }

            yield return new WaitForSeconds(cadenciaDisparo);
        }
    }
}
