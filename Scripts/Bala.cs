using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] int dano;
    [SerializeField] float tiempoDestruccion;
    [SerializeField] int tipoBala;
    [SerializeField] GameObject prefabDisparo;
    [SerializeField] GameObject prefabColision;

    private int direccion;
    private float tiempo = 0;

    private void Start()
    {
        Destroy(this.gameObject, tiempoDestruccion);
        Instantiate(prefabDisparo, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemigo>() != null && tipoBala != 2)
        {
            collision.gameObject.GetComponent<Enemigo>().RecibirDano(dano, tipoBala);
        }
        else if (collision.gameObject.GetComponent<EnemigoFinal>() != null && tipoBala != 2)
        {
            collision.gameObject.GetComponent<EnemigoFinal>().RecibirDano(dano, tipoBala);
        }
        else if (collision.gameObject.GetComponent<Player>() != null && tipoBala == 2)
        {
            collision.gameObject.GetComponent<Player>().RecibirDano(dano);
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(prefabColision, pos, rot);

        Destroy(gameObject, tiempo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemigo>() != null && tipoBala != 2)
        {
            other.gameObject.GetComponent<Enemigo>().RecibirDano(dano, tipoBala);

        }
        else if (other.gameObject.GetComponent<Player>() != null && tipoBala == 2)
        {
            other.gameObject.GetComponent<Player>().RecibirDano(dano);
        }

        Instantiate(prefabColision, transform.position, transform.rotation);

        Destroy(gameObject, tiempo);
    }
}
