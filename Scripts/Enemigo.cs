using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemigo : MonoBehaviour
{
    [SerializeField] protected int vidaMax;
    [SerializeField] GameObject explosion;
    [SerializeField] protected int puntosDano;
    [SerializeField] protected int puntosMuerte;
    [SerializeField] protected Material material;

    public int vida;
    protected int parpadeos;
    protected Material materialOriginal;
    public bool recibirDano = true;

    protected void Start()
    {
        vida = vidaMax;
        materialOriginal = GetComponentInChildren<MeshRenderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<Player>().RecibirDano(20);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.GetComponent<Player>().RecibirDano(20);
            Destroy(gameObject);
        }
    }

    public virtual void RecibirDano(int dano, int tipoBala)
    {
        if (recibirDano)
        {
            vida -= dano;

            if (vida <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);

                if (tipoBala == 0)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos((puntosDano + puntosMuerte), "Zarco");
                }
                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos((puntosDano + puntosMuerte), "Raven");
                }

                Destroy(gameObject);
            }
            else
            {
                foreach (MeshRenderer mr in GetComponents<MeshRenderer>())
                {
                    mr.material = material;
                }

                foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                {
                    mr.material = material;
                }

                Invoke("QuitarRecibiendoDano", 0.01f);

                if (tipoBala == 0)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos(puntosDano, "Zarco");
                }
                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos(puntosDano, "Raven");
                }
            }
        }
    }

    protected virtual void QuitarRecibiendoDano()
    {
        foreach (MeshRenderer mr in GetComponents<MeshRenderer>())
        {
            mr.material = materialOriginal;
        }

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = materialOriginal;
        }
    }
}
