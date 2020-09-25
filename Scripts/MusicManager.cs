using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider volumenFXSlider;
    [SerializeField] Text VolumenMusicaTexto;
    [SerializeField] Text VolumenFXTexto;

    private AudioSource audioSource;
    private AudioSource boton1;
    private AudioSource boton2;
    //private PlayerPrefsManager ppm;

    void Awake()
    {
        //ppm = GameObject.Find("PlayerPrefsManager").GetComponent<PlayerPrefsManager>();
        audioSource = GetComponent<AudioSource>();
        boton1 = GameObject.Find("AudioBoton1").GetComponent<AudioSource>();
        boton2 = GameObject.Find("AudioBoton2").GetComponent<AudioSource>();
        audioSource.enabled = PlayerPrefsManager.ObtenerMusica() == 1 ? true : false;
        boton1.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        boton2.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        audioSource.volume = PlayerPrefsManager.ObtenerVolumenMusica();
        boton1.volume = PlayerPrefsManager.ObtenerVolumenFX();
        boton2.volume = PlayerPrefsManager.ObtenerVolumenFX();
        volumenMusicaSlider.value = PlayerPrefsManager.ObtenerVolumenMusica();
        volumenFXSlider.value = PlayerPrefsManager.ObtenerVolumenFX();
        VolumenMusicaTexto.text = ("Volume " + ((int)(volumenMusicaSlider.value * 100)) + "%");
        VolumenFXTexto.text = ("Volume " + ((int)(volumenFXSlider.value * 100)) + "%");
    }

    public void CambiarVolumenMusica()
    {
        audioSource.volume = volumenMusicaSlider.value;
        VolumenMusicaTexto.text = ("Volume " + ((int)(volumenMusicaSlider.value * 100)) + "%");
    }

    public void CambiarVolumenFX()
    {
        boton1.volume = volumenFXSlider.value;
        boton2.volume = volumenFXSlider.value;
        VolumenFXTexto.text = ("Volume " + ((int)(volumenFXSlider.value * 100)) + "%");
    }
}
