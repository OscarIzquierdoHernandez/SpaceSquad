using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Asteroide : MonoBehaviour
{
    [SerializeField] GameObject explosion;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<Player>().RecibirDano(20);
            Destroy(gameObject);
        }
    }
}
