using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorAleatorio : MonoBehaviour
{
    [SerializeField] float speedMin;
    [SerializeField] float speedMax;

    private float speedX;
    private float speedY;
    private float speedZ;

    private void Start()
    {
        speedX = Random.Range(speedMin, speedMax);
        speedY = Random.Range(speedMin, speedMax);
        speedZ = Random.Range(speedMin, speedMax);
    }

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * speedX, Time.deltaTime * speedY, Time.deltaTime * speedZ));
    }
}
