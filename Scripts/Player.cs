using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] float velocidadMovimiento = 100f;
    [SerializeField] float velocidadRotacion = 100f;
    [SerializeField] float velocidadRetorno = 150f;
    [SerializeField] float fuerzaDisparo = 1000f;
    [SerializeField] float cadenciaDisparo = 0.15f;
    [SerializeField] float cadenciaLaser = 0.3f;
    [SerializeField] float fuerzaMisil = 1000f;
    [SerializeField] float cadenciaMisil = 0.15f;
    [SerializeField] float offsetRetorno = 0.05f;
    [SerializeField] float offsetJoystick = 0.1f;
    [SerializeField] float limiteEscenarioX = 500;
    [SerializeField] float limiteEscenarioY = 300;
    [SerializeField] int danoLaser = 2;
    [SerializeField] Camera camara;
    [SerializeField] int numeroJugador;
    [SerializeField] Transform[] puntosDisparo;
    [SerializeField] Transform puntosDisparoMisil;
    [SerializeField] GameObject prefabBala;
    [SerializeField] GameObject prefabMisil;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject llama;
    [SerializeField] GameObject[] efectoColision;
    [SerializeField] Material material;
    [SerializeField] string nombreJugador;
    [SerializeField] GameObject[] laser;
    [SerializeField] GameObject icono;

    private AudioSource audioLaser;
    private float x1, x2, y1, y2, rotacion;
    private float r = 1;
    private Rigidbody rb;
    //private PlayerPrefsManager ppm;
    private GameManager gm;
    private int posicionCamara = 0;
    private bool tieneCadencia = false;
    private bool tieneCadenciaMisil = false;
    private bool disparoContinuo = false;
    private bool disparoLaser = false;
    private bool cambiarCamara = false;
    private bool pausa = false;
    public enum EstadoPlayer { normal, recibiendoDano, reiniciando };
    public EstadoPlayer estadoPlayer = EstadoPlayer.normal;
    private RaycastHit hit1;
    private RaycastHit hit2;
    private LineRenderer laser1;
    private LineRenderer laser2;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //ppm = GameObject.Find("PlayerPrefsManager").GetComponent<PlayerPrefsManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioLaser = GetComponent<AudioSource>();
        laser1 = laser[0].GetComponent<LineRenderer>();
        laser2 = laser[1].GetComponent<LineRenderer>();

        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
        {
            camara.rect = new Rect(0, 0, 1, 1);
            camara.GetComponent<AudioListener>().enabled = true;
        }
        else if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2 && numeroJugador == 1)
        {
            camara.rect = new Rect(0.5f, 0, 0.5f, 1);
        }
        else
        {
            camara.rect = new Rect(0, 0, 0.5f, 1);
        }

        material.color = new Color(1, 1, 1, 1);

    }

    void Update()
    {
        if (GameManager.estadoJuego != GameManager.EstadoJuego.Terminado)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Fire4") || Input.GetButtonDown("Fire4_2"))
            {
                if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1 || nombreJugador == "Zarco")
                {
                    if (!pausa)
                    {
                        PausarJuego();
                    }
                    else
                    {
                        DespausarJuego();
                    }

                    pausa = !pausa;
                }
            }
        }
        else
        {
            audioLaser.Stop();
            StopCoroutine("DispararContinuo");
            StopCoroutine("DispararLaser");
            disparoLaser = false;
        }

        if (GameManager.estadoJuego == GameManager.EstadoJuego.Empezado && estadoPlayer != EstadoPlayer.reiniciando)
        {
            x1 = Input.GetAxis("Horizontal");
            y1 = Input.GetAxis("Vertical");
            x2 = Input.GetAxis("Horizontal2");
            y2 = Input.GetAxis("Vertical2");

            Mover();
            Rotar();

            if (posicionCamara != 0)
            {
                camara.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if ((Input.GetButtonDown("Fire3") && numeroJugador == 0) || (Input.GetButtonDown("Fire3_2") && numeroJugador == 1))
            {
                CambiarCamara();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject.Find("AudioPaniagua").GetComponent<AudioSource>().Play();
                gm.Paniaguizar();
            }

            if (!disparoContinuo && !tieneCadencia && gm.ObtenerDisparo(nombreJugador) == 2 && ((Input.GetButtonDown("Fire1") && numeroJugador == 0) || (Input.GetButtonDown("Fire1_2") && numeroJugador == 1)))
            {
                StartCoroutine("DispararContinuo");
                tieneCadencia = true;
                disparoContinuo = true;
                Invoke("QuitarCadencia", cadenciaDisparo);
            }
            else if (!disparoLaser && !tieneCadencia && gm.ObtenerDisparo(nombreJugador) ==3 && ((Input.GetButtonDown("Fire1") && numeroJugador == 0) || (Input.GetButtonDown("Fire1_2") && numeroJugador == 1)))
            {
                StartCoroutine("DispararContinuo");
                StartCoroutine("DispararLaser");
                tieneCadencia = true;
                disparoLaser = true;
                Invoke("QuitarCadencia", cadenciaDisparo);
            }
            else if (gm.ObtenerDisparo(nombreJugador) == 1 && ((Input.GetButtonDown("Fire1") && numeroJugador == 0) || (Input.GetButtonDown("Fire1_2") && numeroJugador == 1)))
            {
                Disparar();
            }

            if ((Input.GetButtonDown("Fire2") && numeroJugador == 0) || (Input.GetButtonDown("Fire2_2") && numeroJugador == 1))
            {
                DispararMisil();
            }

            if (gm.ObtenerDisparo(nombreJugador) == 2 && (Input.GetButtonUp("Fire1") && numeroJugador == 0) || (Input.GetButtonUp("Fire1_2") && numeroJugador == 1))
            {
                StopCoroutine("DispararContinuo");
                disparoContinuo = false;
            }

            if (gm.ObtenerDisparo(nombreJugador) == 3 && (Input.GetButtonUp("Fire1") && numeroJugador == 0) || (Input.GetButtonUp("Fire1_2") && numeroJugador == 1))
            {
                audioLaser.Stop();
                StopCoroutine("DispararContinuo");
                StopCoroutine("DispararLaser");
                disparoLaser = false;
            }
        }

        if (disparoLaser)
        {
            laser[0].SetActive(true);
            laser[1].SetActive(true);
            laser1.SetPosition(0, laser1.gameObject.transform.position);
            laser2.SetPosition(0, laser2.gameObject.transform.position);

            if (Physics.Raycast(laser1.gameObject.transform.position, laser1.gameObject.transform.forward, out hit1))
            {
                if (hit1.collider)
                {
                    laser1.SetPosition(1, hit1.point);
                    efectoColision[0].SetActive(true);
                    efectoColision[0].transform.position = hit1.point;      
                }
            }
            else
            {
                efectoColision[0].SetActive(false);
                laser1.SetPosition(1, laser1.gameObject.transform.forward * 200);
            }

            if (Physics.Raycast(laser2.gameObject.transform.position, laser2.gameObject.transform.forward, out hit2))
            {
                if (hit2.collider)
                {
                    laser2.SetPosition(1, hit2.point);
                    efectoColision[1].SetActive(true);
                    efectoColision[1].transform.position = hit2.point;
                }
            }
            else
            {
                efectoColision[1].SetActive(false);
                laser2.SetPosition(1, laser2.gameObject.transform.forward * 200);
            }
        }
        else
        {
            laser[0].SetActive(false);
            laser[1].SetActive(false);
        }
    }

    private void Mover()
    {
        if ((Mathf.Abs(x1) > offsetJoystick || Mathf.Abs(y1) > offsetJoystick) && numeroJugador == 0)
        {
            transform.Translate(x1 * velocidadMovimiento * Time.deltaTime, y1 * velocidadMovimiento * Time.deltaTime, 0, Space.World);
        }
        else if ((Mathf.Abs(x2) > offsetJoystick || Mathf.Abs(y2) > offsetJoystick) && numeroJugador == 1)
        {
            transform.Translate(x2 * velocidadMovimiento * Time.deltaTime, y2 * velocidadMovimiento * Time.deltaTime, 0, Space.World);
        }

        transform.position = new Vector3(Mathf.Min(transform.position.x, limiteEscenarioX), Mathf.Min(transform.position.y, limiteEscenarioY), transform.position.z);
        transform.position = new Vector3(Mathf.Max(transform.position.x, 0), Mathf.Max(transform.position.y, 0), transform.position.z);
    }

    private void Rotar()
    {
        float x;

        if (numeroJugador == 0)
        {
            x = x1;
        }
        else
        {
            x = x2;
        }

        Rotar2(x);
    }

    private void Rotar2(float x)
    {
        if (Mathf.Abs(x) > offsetJoystick)
        {
            transform.Rotate(0, 0, -x * velocidadRotacion * Time.deltaTime);

            if (transform.rotation.z > 0 && transform.eulerAngles.z > 30)
            {
                transform.eulerAngles = new Vector3(0, 0, 30);
            }
            else if (transform.rotation.z < 0 && transform.eulerAngles.z < 330)
            {
                transform.eulerAngles = new Vector3(0, 0, -30);
            }

            cambiarCamara = false;
        }
        else
        {
            if (transform.rotation.z > offsetRetorno)
            {
                transform.Rotate(0, 0, -velocidadRetorno * Time.deltaTime);
            }
            else if (transform.rotation.z < -offsetRetorno)
            {
                transform.Rotate(0, 0, velocidadRetorno * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                cambiarCamara = true;
            }
        }
    }

    private void CambiarCamara()
    {
        if (cambiarCamara)
        {
            switch (posicionCamara)
            {
                case 0:

                    camara.gameObject.transform.position = transform.position + new Vector3(0, 10, -30);
                    camara.gameObject.transform.rotation = transform.rotation;
                    posicionCamara = 1;

                    break;

                case 1:

                    camara.gameObject.transform.position = transform.position + new Vector3(0, 17, -50);
                    camara.gameObject.transform.rotation = transform.rotation;
                    posicionCamara = 2;

                    break;

                case 2:

                    camara.gameObject.transform.position = transform.position + new Vector3(0, 0.8f, 2.3f);
                    camara.gameObject.transform.rotation = transform.rotation;
                    posicionCamara = 0;

                    break;
            }
        }
    }

    private void Disparar()
    {
        if (!tieneCadencia)
        {
            Disparar2(0);
            Disparar2(1);

            tieneCadencia = true;
            Invoke("QuitarCadencia", cadenciaDisparo);
        }
    }

    private void Disparar2(int pos)
    {
        //Vector3 point = new Vector3();
        //Event currentEvent = Event.current;
        //Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        //mousePos.x = camara.pixelWidth / 2;
        //mousePos.y = camara.pixelHeight / 2;

        //point = camara.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camara.nearClipPlane));

        GameObject go = Instantiate(prefabBala, puntosDisparo[pos].position, puntosDisparo[pos].rotation);
        //go.GetComponent<Rigidbody>().AddForce((point - puntosDisparo[pos].position) * fuerzaDisparo);
        go.GetComponent<Rigidbody>().AddForce((Vector3.forward) * fuerzaDisparo);
    }

    private void QuitarCadencia()
    {
        tieneCadencia = false;
    }

    private void DispararMisil()
    {
        if (!tieneCadenciaMisil && gm.ObtenerMisiles(nombreJugador) > 0)
        {
            DispararMisil2();
            gm.RestarMisil(nombreJugador);
            tieneCadenciaMisil = true;
            Invoke("QuitarCadenciaMisil", cadenciaMisil);
        }
    }

    private void DispararMisil2()
    {
        GameObject go = Instantiate(prefabMisil, puntosDisparoMisil.position, puntosDisparoMisil.rotation);
        //go.GetComponent<Rigidbody>().AddForce((Vector3.forward) * fuerzaMisil);
    }

    private void QuitarCadenciaMisil()
    {
        tieneCadenciaMisil = false;
    }

    private IEnumerator DispararContinuo()
    {
        while (true)
        {
            Disparar2(0);
            Disparar2(1);
            Disparar2(2);
            Disparar2(3);
            yield return new WaitForSeconds(cadenciaDisparo);
        }
    }

    private IEnumerator DispararLaser()
    {
        while (true)
        {
            if (hit1.collider && hit1.collider.gameObject.CompareTag("Enemigo"))
            {
                if (hit1.collider.gameObject.GetComponent<Enemigo>() != null)
                {
                    if (nombreJugador == "Zarco")
                    {
                        hit1.collider.gameObject.GetComponent<Enemigo>().RecibirDano(danoLaser, 0);
                    }
                    else
                    {
                        hit1.collider.gameObject.GetComponent<Enemigo>().RecibirDano(danoLaser, 1);
                    }
                }
                else
                {
                    if (nombreJugador == "Zarco")
                    {
                        hit1.collider.gameObject.GetComponent<EnemigoFinal>().RecibirDano(danoLaser, 0);
                    }
                    else
                    {
                        hit1.collider.gameObject.GetComponent<EnemigoFinal>().RecibirDano(danoLaser, 1);
                    }
                }
            }

            if (hit2.collider && hit2.collider.gameObject.CompareTag("Enemigo"))
            {
                if (hit2.collider.gameObject.GetComponent<Enemigo>() != null)
                {
                    if (nombreJugador == "Zarco")
                    {
                        hit2.collider.gameObject.GetComponent<Enemigo>().RecibirDano(danoLaser, 0);
                    }
                    else
                    {
                        hit2.collider.gameObject.GetComponent<Enemigo>().RecibirDano(danoLaser, 1);
                    }
                }
                else
                {
                    if (nombreJugador == "Zarco")
                    {
                        hit2.collider.gameObject.GetComponent<EnemigoFinal>().RecibirDano(danoLaser, 0);
                    }
                    else
                    {
                        hit2.collider.gameObject.GetComponent<EnemigoFinal>().RecibirDano(danoLaser, 1);
                    }
                }
            }

            if (!audioLaser.isPlaying)
            {
                audioLaser.enabled = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
                audioLaser.volume = PlayerPrefsManager.ObtenerVolumenFX();
                audioLaser.Play();
            }

            yield return new WaitForSeconds(cadenciaLaser);
        }
    }

    public void GuardarNumeroJugador(int numeroJugadorNuevo)
    {
        numeroJugador = numeroJugadorNuevo;
    }

    public void RecargarEnergia(int cantidad)
    {
        gm.RecargarEnergia(cantidad, nombreJugador);
    }

    public void MejorarDisparo(int cantidad)
    {
        gm.MejorarDisparo(cantidad, nombreJugador);
    }

    public void RecargarMisiles(int cantidad)
    {
        gm.RecargarMisiles(cantidad, nombreJugador);
    }

    public void RecibirDano(int dano)
    {
        if (estadoPlayer == EstadoPlayer.normal)
        {
            if (!gm.RecibirDano(dano, nombreJugador))
            {
                MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
                Sequence s = DOTween.Sequence();
                s.Append(material.DOColor(new Color(1, 1, 1, 0), 0.05f));
                s.Append(material.DOColor(new Color(1, 1, 1, 1), 0.05f));
                s.SetLoops(14);
                estadoPlayer = EstadoPlayer.recibiendoDano;
                Invoke("QuitarRecibiendoDano", 1.4f);
            }
            else
            {
                GameObject go = Instantiate(explosion, transform.position, transform.rotation);
                icono.SetActive(false);
                material.color = new Color(1, 1, 1, 0);
                llama.SetActive(false);
                audioLaser.Stop();
                StopCoroutine("DispararContinuo");
                StopCoroutine("DispararLaser");
                disparoContinuo = false;
                disparoLaser = false;
                estadoPlayer = EstadoPlayer.reiniciando;

                if (!gm.RestarVida(nombreJugador))
                {
                    gm.ReiniciarJugador(nombreJugador);
                    Invoke("Reiniciar", 2.5f);
                }
                else
                {
                    gm.MostrarGameOver(nombreJugador);
                    tag = "Untagged";
                    name = name + "Muerto";
                }
            }
        }
    }

    public void PausarJuego()
    {
        estadoPlayer = EstadoPlayer.reiniciando;
        gm.PausarJuego();
    }

    public void DespausarJuego()
    {
        estadoPlayer = EstadoPlayer.normal;
        gm.DespausarJuego();
    }

    private void QuitarRecibiendoDano()
    {
        estadoPlayer = EstadoPlayer.normal;
    }

    private void Reiniciar()
    {
        gm.Empezar(nombreJugador);
        icono.SetActive(true);
        llama.SetActive(true);
        material.color = new Color(1, 1, 1, 1);
        Sequence s = DOTween.Sequence();
        s.Append(material.DOColor(new Color(1, 1, 1, 0), 0.05f));
        s.Append(material.DOColor(new Color(1, 1, 1, 1), 0.05f));
        s.SetLoops(14);
        estadoPlayer = EstadoPlayer.recibiendoDano;

        Invoke("QuitarRecibiendoDano", 1.4f);
    }

    public EstadoPlayer ObtenerEstado()
    {
        return estadoPlayer;
    }
}
