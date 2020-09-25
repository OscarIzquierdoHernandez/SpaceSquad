using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    void Start()
    {
        Invoke("Interactuar", 1);
    }

    void Interactuar()
    {
        GetComponent<Button>().interactable = true;
    }
}
