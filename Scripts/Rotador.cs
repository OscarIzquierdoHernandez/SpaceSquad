using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador : MonoBehaviour
{
    [SerializeField] float velocidadX;
    [SerializeField] float velocidadY;
    [SerializeField] float velocidadZ;

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * velocidadX, Time.deltaTime * velocidadY, Time.deltaTime * velocidadZ));
    }
}
