using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int tipo;
    [SerializeField] int cantidad;
    [SerializeField] float velocidadMovimiento;
    [SerializeField] GameObject efecto;

    private bool recogido = false;


    private bool destruido = false;

    void Update()
    {
        transform.Translate(new Vector3(0, 0, Time.deltaTime * -velocidadMovimiento));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !recogido)
        {
            recogido = true;

            switch (tipo)
            {
                case 0:

                    other.gameObject.GetComponent<Player>().RecargarEnergia(cantidad);
                    break;

                case 1:

                    other.gameObject.GetComponent<Player>().MejorarDisparo(cantidad);
                    break;

                case 2:

                    other.gameObject.GetComponent<Player>().RecargarMisiles(cantidad);
                    break;
            }

            Instantiate(efecto, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
