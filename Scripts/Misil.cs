using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil : MonoBehaviour
{
    [SerializeField] int dano;
    [SerializeField] int tipoBala;
    [SerializeField] float radio;

    private Collider[] colliders;
    
    private void OnCollisionEnter(Collision collision)
    {
        colliders = Physics.OverlapSphere(transform.position, radio);
        
        if (colliders != null)
        {
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Enemigo"))
                {
                    c.gameObject.GetComponent<Enemigo>().RecibirDano(dano, tipoBala);
                }
            }
        }
    }
}
