using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AparecerProgresivo : MonoBehaviour
{
    [SerializeField] float duracion;

    void Start()
    {
        GetComponent<MeshRenderer>().material.DOFade(1, duracion);

        /*foreach (MeshRenderer m in GetComponents<MeshRenderer>())
        {
            m.material.DOFade(1, duracion);
        }*/
    }
}
