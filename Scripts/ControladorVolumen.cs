using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVolumen : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.ObtenerVolumenFX();
    }
}
