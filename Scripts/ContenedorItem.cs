using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorItem : MonoBehaviour
{
    [SerializeField] float velocidadRotacion;
    [SerializeField] float velocidadMovimiento;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject item;

    private bool destruido = false;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * velocidadRotacion));
        transform.Translate(new Vector3(0, 0, Time.deltaTime * velocidadMovimiento));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Proyectil") && !destruido)
        {
            destruido = true;
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
