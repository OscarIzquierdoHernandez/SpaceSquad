using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string NUMERO_PLAYERS = "NUMERO_PLAYERS";
    private const string PLAYER1_CHARACTER = "PLAYER1_CHARACTER";
    private const string PLAYER2_CHARACTER = "PLAYER2_CHARACTER";
    private const string MUSICA = "MUSICA";
    private const string FX = "FX";
    private const string VOLUMEN_MUSICA = "VOLUMEN_MUSICA";
    private const string VOLUMEN_FX = "VOLUMEN_FX";

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void GuardarNumeroJugadores(int numeroJugadores)
    {
        PlayerPrefs.SetInt(NUMERO_PLAYERS, numeroJugadores);
        PlayerPrefs.Save();
    }

    public static int ObtenerNumeroJugadores()
    {
        return PlayerPrefs.GetInt(NUMERO_PLAYERS, 1);
    }

    public static void GuardarNumeroJugadorUno(int numeroJugador)
    {
        PlayerPrefs.SetInt(PLAYER1_CHARACTER, numeroJugador);
        PlayerPrefs.Save();
    }

    public static int ObtenerNumeroJugadorUno()
    {
        return PlayerPrefs.GetInt(PLAYER1_CHARACTER, 1);
    }

    public static void GuardarNumeroJugadorDos(int numeroJugador)
    {
        PlayerPrefs.SetInt(PLAYER2_CHARACTER, numeroJugador);
        PlayerPrefs.Save();
    }

    public static int ObtenerNumeroJugadorDos()
    {
        return PlayerPrefs.GetInt(PLAYER2_CHARACTER, 1);
    }

    public static void GuardarMusica(int musica)
    {
        PlayerPrefs.SetInt(MUSICA, musica);
        PlayerPrefs.Save();
    }

    public static int ObtenerMusica()
    {
        return PlayerPrefs.GetInt(MUSICA, 1);
    }

    public static void GuardarFX(int fx)
    {
        PlayerPrefs.SetInt(FX, fx);
        PlayerPrefs.Save();
    }

    public static int ObtenerFX()
    {
        return PlayerPrefs.GetInt(FX, 1);
    }

    public static void GuardarVolumenMusica(float volumenMusica)
    {
        PlayerPrefs.SetFloat(VOLUMEN_MUSICA, volumenMusica);
        PlayerPrefs.Save();
    }

    public static float ObtenerVolumenMusica()
    {
        return PlayerPrefs.GetFloat(VOLUMEN_MUSICA, 1);
    }

    public static void GuardarVolumenFX(float volumenFX)
    {
        PlayerPrefs.SetFloat(VOLUMEN_FX, volumenFX);
        PlayerPrefs.Save();
    }

    public static float ObtenerVolumenFX()
    {
        return PlayerPrefs.GetFloat(VOLUMEN_FX, 1);
    }
}
