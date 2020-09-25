using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPrefabs : MonoBehaviour
{
    [SerializeField] float limiteIinfeeriior;
    [SerializeField] float limiteSuperiior;
    [SerializeField] float limiteIzquierda;
    [SerializeField] float limiteDerecha;
    [SerializeField] float velocidadPrefab;
    [SerializeField] float velocidadPrefabMax = 0;
    [SerializeField] float cadenciaMin;
    [SerializeField] float cadenciaMax;
    [SerializeField] float tiempoHastaComienzo;
    [SerializeField] int limitePrefabs;
    [SerializeField] bool infinito = true;
    [SerializeField] GameObject[] prefabs;

    private int numeoPrefabs = 0;

    void Start()
    {
        if (velocidadPrefabMax == 0)
        {
            velocidadPrefabMax = velocidadPrefab;
        }

        Invoke("EmpezarCorutina", tiempoHastaComienzo);
    }

    private void EmpezarCorutina()
    {
        StartCoroutine("GenerarPrefab");
    }

    private IEnumerator GenerarPrefab()
    {
        while (true)
        {
            Vector3 posicion = new Vector3(transform.position.x + Random.Range(limiteIzquierda, limiteDerecha), transform.position.y + Random.Range(limiteIinfeeriior, limiteSuperiior), transform.position.z);
            GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)], posicion, transform.rotation);

            if (go.GetComponent<Rigidbody>() != null)
            {
                go.GetComponent<Rigidbody>().AddForce(Vector3.forward * -(Random.Range(velocidadPrefab, velocidadPrefabMax)));
            }

            if (!infinito)
            {
                numeoPrefabs++;

                if (numeoPrefabs == limitePrefabs)
                {
                    Destroy(gameObject);
                }
            }

            yield return new WaitForSeconds(Random.Range(cadenciaMin, cadenciaMax));
        }
    }
}
