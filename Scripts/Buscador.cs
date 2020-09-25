using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buscador : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float radio;

    private Transform transformEnemigo = null;
    private Collider[] colliders;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
        if (transformEnemigo != null)
        {
            transform.LookAt(transformEnemigo);
        }
        else
        {
            colliders = Physics.OverlapSphere(transform.position, radio);

            if (colliders != null)
            {
                for(int i = 0; i < colliders.Length && transformEnemigo == null; i++)
                {
                    if (colliders[i].gameObject.CompareTag("Enemigo"))
                    {
                        transformEnemigo = colliders[i].gameObject.transform;
                    }
                }
            }
        }
    }
}
