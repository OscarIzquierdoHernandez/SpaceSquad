using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionJugador : MonoBehaviour
{
    [SerializeField] Transform[] puntosCamara;
    [SerializeField] float velocidadCamara;
    [SerializeField] GameObject[] luces;
    [SerializeField] AudioClip[] audioJugadores;

    private int posicionCamara = 0;
    private int numeroJugadores;
    private float x, porcentaje;
    private Transform transformInicial;
    private Transform transformDestino;
    //private PlayerPrefsManager ppm;
    private UISeleccionJugadorManager ui;
    private bool cadenciaCambio = false;
    private bool seleccionado = false;
    private AudioSource audioSource;
    private AudioSource musica;
    private AudioSource efecto1;
    private AudioSource efecto2;

    void Start()
    {
        //ppm = GameObject.Find("PlayerPrefsManager").GetComponent<PlayerPrefsManager>();
        ui = GameObject.Find("UISeleccionJugadorManager").GetComponent<UISeleccionJugadorManager>();
        ui.IniciarUI(PlayerPrefsManager.ObtenerNumeroJugadores());

        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
        {
            luces[0].SetActive(true);
            luces[1].SetActive(true);
        }

        audioSource = GetComponent<AudioSource>();
        musica = GameObject.Find("AudioMusic").GetComponent<AudioSource>();
        efecto1 = GameObject.Find("AudioEfecto1").GetComponent<AudioSource>();
        efecto2 = GameObject.Find("AudioEfecto2").GetComponent<AudioSource>();
        musica.enabled = PlayerPrefsManager.ObtenerMusica() == 1 ? true : false;
        efecto1.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        efecto2.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        audioSource.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        musica.volume = PlayerPrefsManager.ObtenerVolumenMusica();
        efecto1.volume = PlayerPrefsManager.ObtenerVolumenFX();
        efecto2.volume = PlayerPrefsManager.ObtenerVolumenFX();
        audioSource.volume = PlayerPrefsManager.ObtenerVolumenFX();
    }

    void Update()
    {
        if (!seleccionado)
        {
            x = Input.GetAxis("Horizontal");

            if (Mathf.Abs(x) > 0.1f && !cadenciaCambio)
            {
                efecto1.Play();

                if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
                {
                    if (x < 0 && posicionCamara != 1)
                    {
                        porcentaje = 0;
                        transformInicial = transform;
                        transformDestino = puntosCamara[1];
                        luces[0].SetActive(true);
                        luces[1].SetActive(false);
                        StopCoroutine("MoverCamara");
                        StartCoroutine("MoverCamara");
                        posicionCamara = 1;
                        ui.MostrarZarco();
                    }
                    else if (x > 0 && posicionCamara != 2)
                    {
                        porcentaje = 0;
                        transformInicial = transform;
                        transformDestino = puntosCamara[2];
                        luces[0].SetActive(false);
                        luces[1].SetActive(true);
                        StopCoroutine("MoverCamara");
                        StartCoroutine("MoverCamara");
                        posicionCamara = 2;
                        ui.MostrarRaven();
                    }
                }
                else
                {
                    ui.CambiarJugadores();
                }

                cadenciaCambio = true;
                Invoke("QuitarCadencia", 0.3f);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                SeleccionarJugador();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Fire4"))
            {
                ui.CambiarEscena3();
            }
        }
    }

    private void SeleccionarJugador()
    {
        if (!seleccionado)
        {
            if (posicionCamara != 0 && PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
            {
                if (posicionCamara == 1)
                {
                    GameObject.Find("Zarco").GetComponent<Animator>().SetTrigger("selected");
                    audioSource.PlayOneShot(audioJugadores[0]);
                }
                else
                {
                    GameObject.Find("Raven").GetComponent<Animator>().SetTrigger("selected");
                    audioSource.PlayOneShot(audioJugadores[1]);
                }

                PlayerPrefsManager.GuardarNumeroJugadorUno((posicionCamara - 1));
                seleccionado = true;
                Invoke("CambiarEscena", 2);
            }
            else if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
            {
                if (ui.ObtenerTextoJugador(0) == "1P")
                {
                    PlayerPrefsManager.GuardarNumeroJugadorUno(0);
                    PlayerPrefsManager.GuardarNumeroJugadorDos(1);
                }
                else
                {
                    PlayerPrefsManager.GuardarNumeroJugadorUno(1);
                    PlayerPrefsManager.GuardarNumeroJugadorDos(0);
                }

                GameObject.Find("Zarco").GetComponent<Animator>().SetTrigger("selected");
                GameObject.Find("Raven").GetComponent<Animator>().SetTrigger("selected");
                efecto2.Play();
                seleccionado = true;
                Invoke("CambiarEscena", 2);
            }
        }
    }

    private void CambiarEscena()
    {
        ui.CambiarEscena();
    }

    private void QuitarCadencia()
    {
        cadenciaCambio = false;
    }

    private IEnumerator MoverCamara()
    {
        while (porcentaje <= 1)
        {
            porcentaje += Time.deltaTime * velocidadCamara;
            transform.position = Vector3.Lerp(transformInicial.position, transformDestino.position, porcentaje);
            transform.rotation = Quaternion.Lerp(transformInicial.rotation, transformDestino.rotation, porcentaje);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
