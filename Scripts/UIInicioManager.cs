using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIInicioManager : MonoBehaviour
{
    [SerializeField] Transform textoAnyKey;

    [SerializeField] Toggle musica;
    [SerializeField] Toggle fx;
    [SerializeField] Slider volumenMusicaSlider;
    [SerializeField] Slider volumenFXSlider;
    [SerializeField] Text VolumenMusicaTexto;
    [SerializeField] Text VolumenFXTexto;
    [SerializeField] Button unJugadorBoton;
    [SerializeField] Button dosJugadoresBoton;
    [SerializeField] Button puntuacionesBoton;
    [SerializeField] Button opcionesBoton;
    [SerializeField] Button salirBoton;
    [SerializeField] Button guardarBoton;
    [SerializeField] Button cancelarBoton;
    [SerializeField] GameObject fondo;
    [SerializeField] GameObject menuPrincipal;
    [SerializeField] GameObject oizquier;
    [SerializeField] GameObject pantallaNegra;

    /*public const string MEJOR_PUNTUACION_1 = "MEJOR_PUNTUACION_1";
    public const string MEJOR_PUNTUACION_2 = "MEJOR_PUNTUACION_2";
    public const string MEJOR_PUNTUACION_3 = "MEJOR_PUNTUACION_3";
    public const string MEJOR_PUNTUACION_4 = "MEJOR_PUNTUACION_4";
    public const string MEJOR_PUNTUACION_5 = "MEJOR_PUNTUACION_5";
    public const string MEJOR_PUNTUACION_6 = "MEJOR_PUNTUACION_6";
    public const string MEJOR_PUNTUACION_7 = "MEJOR_PUNTUACION_7";
    public const string MEJOR_PUNTUACION_8 = "MEJOR_PUNTUACION_8";
    public const string MEJOR_PUNTUACION_9 = "MEJOR_PUNTUACION_9";
    public const string MEJOR_PUNTUACION_10 = "MEJOR_PUNTUACION_10";
    public const string CHECKPOINT_ACTIVO = "CHECKPOINT_ACTIVO";
    public const string PARAM_X = "x";
    public const string PARAM_Y = "y";
    public const string NIVEL_EMPEZADO = "NIVEL_EMPEZADO";
    public const string NIVEL_ACTUAL = "NIVEL_ACTUAL";
    public const string PANTALLA_ACTUAL = "PANTALLA_ACTUAL";
    public const string VIDAS = "VIDAS";
    public const string PODER = "PODER";*/

    private bool anyKeyPresionada = true;
    //private PlayerPrefsManager ppm;

    private void Start()
    {
        //ppm = GameObject.Find("PlayerPrefsManager").GetComponent<PlayerPrefsManager>();
        Cursor.visible = false;
        textoAnyKey.gameObject.GetComponent<Text>().DOColor(new Color(1, 1, 1, 0), 1f).SetLoops(-1, LoopType.Yoyo);
        textoAnyKey.gameObject.GetComponent<Outline>().DOColor(new Color(1, 1, 1, 0), 1f).SetLoops(-1, LoopType.Yoyo);

        Sequence s = DOTween.Sequence();
        s.Append(oizquier.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 2.1f));
        s.Append(oizquier.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 2.4f));
        s.Append(oizquier.gameObject.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 2.1f));
        Invoke("QuitarPresentacion", 6.5f);
    }

    void Update()
    {
        if (Input.anyKey && !anyKeyPresionada)
        {
            fondo.SetActive(true);
            menuPrincipal.SetActive(true);
            anyKeyPresionada = true;
            GameObject.Find("AudioBoton2").GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void QuitarPresentacion()
    {
        oizquier.SetActive(false);
        pantallaNegra.SetActive(false);
        anyKeyPresionada = false;
    }

    private void CargarOpciones()
    {
        musica.isOn = PlayerPrefsManager.ObtenerMusica() == 1 ? true : false;
        fx.isOn = PlayerPrefsManager.ObtenerFX() == 1 ? true : false;
        volumenMusicaSlider.value = PlayerPrefsManager.ObtenerVolumenMusica();
        volumenFXSlider.value = PlayerPrefsManager.ObtenerVolumenFX();
        VolumenMusicaTexto.text = ("Volume " + ((int)(volumenMusicaSlider.value * 100)) + "%");
        VolumenFXTexto.text = ("Volume " + ((int)(volumenFXSlider.value * 100)) + "%");
    }

    public void EmpezarJuegoUnJugador()
    {
        PlayerPrefsManager.GuardarNumeroJugadores(1);
        pantallaNegra.SetActive(true);
        pantallaNegra.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        pantallaNegra.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
        Invoke("CargarSeleccionJugador", 1f);
    }

    public void EmpezarJuegoDosJugadores()
    {
        PlayerPrefsManager.GuardarNumeroJugadores(2);
        pantallaNegra.SetActive(true);
        pantallaNegra.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        pantallaNegra.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
        Invoke("CargarSeleccionJugador", 1f);
    }

    private void CargarSeleccionJugador()
    {
        SceneManager.LoadScene("SeleccionJugador");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Guardar()
    {
        int music = musica.isOn ? 1 : 0;
        int fxs = fx.isOn ? 1 : 0;
        float volumeMusic = volumenMusicaSlider.value;
        float volumeFX = volumenFXSlider.value;
        PlayerPrefsManager.GuardarMusica(music);
        PlayerPrefsManager.GuardarFX(fxs);
        PlayerPrefsManager.GuardarVolumenMusica(volumeMusic);
        PlayerPrefsManager.GuardarVolumenFX(volumeFX);
    }

    public void Cancelar()
    {
        CargarOpciones();
    }

    // EFECTOS BOTONES

    public void AgrandarUnJugador()
    {
        unJugadorBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarDosJugadores()
    {
        dosJugadoresBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarOpciones()
    {
        opcionesBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarPuntuaciones()
    {
        puntuacionesBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarSalir()
    {
        salirBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarGuardar()
    {
        guardarBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarCancelar()
    {
        cancelarBoton.transform.DOScale(1.2f, 0);
    }

    public void EncogerUnJugador()
    {
        unJugadorBoton.transform.DOScale(1, 0);
    }

    public void EncogerDosJugadores()
    {
        dosJugadoresBoton.transform.DOScale(1, 0);
    }

    public void EncogerPuntuaciones()
    {
        puntuacionesBoton.transform.DOScale(1, 0);
    }

    public void EncogerOpciones()
    {
        opcionesBoton.transform.DOScale(1, 0);
    }

    public void EncogerSalir()
    {
        salirBoton.transform.DOScale(1, 0);
    }

    public void EncogerGuardar()
    {
        guardarBoton.transform.DOScale(1, 0);
    }

    public void EncogerCancelar()
    {
        cancelarBoton.transform.DOScale(1, 0);
    }
}
