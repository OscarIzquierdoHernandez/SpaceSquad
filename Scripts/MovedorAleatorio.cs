using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedorAleatorio : MonoBehaviour
{
    [SerializeField] float speedMin;
    [SerializeField] float speedMax;
    [SerializeField] float sizeMin;
    [SerializeField] float sizeMax;

    private float x, y, z, speed, size;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
        z = Random.Range(-1f, 1f);
        speed = Random.Range(speedMin, speedMax);
        size = Random.Range(sizeMin, sizeMax);
        transform.localScale = Vector3.one * size;
        GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z) * speed);
    }
}
