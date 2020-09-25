using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor3 : MonoBehaviour
{
    [SerializeField] float tiempoDestruccion;

    void Start()
    {
        Destroy(gameObject, tiempoDestruccion);
    }
}
