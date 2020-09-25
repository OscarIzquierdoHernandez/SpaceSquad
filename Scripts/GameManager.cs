using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public enum EstadoJuego { Empezando, Empezado, Pausado, Terminado };
    public static EstadoJuego estadoJuego = EstadoJuego.Empezando;

    [SerializeField] GameObject player1;
    [SerializeField] int energia1;
    [SerializeField] int energiaMax1;
    [SerializeField] int vidas1;
    [SerializeField] int vidasMax1;
    [SerializeField] int misiles1;
    [SerializeField] int misilesMax1;
    [SerializeField] int puntuacion1;
    private int nivelDisparo1 = 1;
    private int nivelDisparoMax1 = 3;

    [SerializeField] GameObject player2;
    [SerializeField] int energia2;
    [SerializeField] int energiaMax2;
    [SerializeField] int vidas2;
    [SerializeField] int vidasMax2;
    [SerializeField] int misiles2;
    [SerializeField] int misilesMax2;
    [SerializeField] int puntuacion2;
    private int nivelDisparo2 = 1;
    private int nivelDisparoMax2 = 3;

    [SerializeField] GameObject[] generadoresFasesJugador1;
    [SerializeField] GameObject[] generadoresFasesJugador2;
    [SerializeField] GameObject jefeFinal;
    [SerializeField] AudioClip musicaJefeFinal1;
    [SerializeField] AudioClip musicaJefeFinal2;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip nivelCompletado;

    private UINivelUnoManager uim;
    private AudioSource musica;
    private VideoPlayer fondo;
    private int fase = 0;
    private int faseActual = 0;

    void Start()
    {
        musica = GameObject.Find("AudioMusic").GetComponent<AudioSource>();
        fondo = GameObject.Find("VideoPlayerFondo").GetComponent<VideoPlayer>();
        musica.enabled = PlayerPrefsManager.ObtenerMusica() == 1 ? true : false;
        musica.volume = PlayerPrefsManager.ObtenerVolumenMusica();
        uim = GameObject.Find("UINivelUnoManager").GetComponent<UINivelUnoManager>();
        IniciarJuego();
    }

    private void Update()
    {
        switch (fase)
        {
            case 1:

                generadoresFasesJugador1[fase].SetActive(true);

                if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
                {
                    generadoresFasesJugador2[fase].SetActive(true);
                }

                Invoke("ComprobarFase", 60);
                fase = 0;

                break;

            case 2:

                generadoresFasesJugador1[fase].SetActive(true);
                Invoke("ComprobarFase", 43);
                fase = 0;

                break;

            case 3:

                uim.MostrarWarning();
                jefeFinal.SetActive(true);
                musica.Stop();
                Invoke("ReproducirMusicaBatallaFinal", 4f);
                Invoke("ComprobarFase2", 0);
                fase = 0;
                
                break;

            case 4:

                jefeFinal.tag = "Enemigo";
                jefeFinal.GetComponent<Animator>().SetTrigger("Final");
                fase = 0;

                break;

            case 5:

                jefeFinal.GetComponent<Animator>().SetTrigger("Death");
                Invoke("TerminarNivel", 4);
                fase = 0;

                break;
        }
    }

    private void IniciarJuego()
    {
        musica.Play();
        energia1 = energiaMax1;
        energia2 = energiaMax2;
        uim.FundirNegro(0, 0.2f);
        Invoke("MostrarInicio", 2);
        generadoresFasesJugador1[fase].SetActive(true);

        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
        {
            generadoresFasesJugador2[fase].SetActive(true);
        }

        Invoke("ComprobarFase", 40);
        
       // fase = 3;
       // faseActual = 3;

        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
        {
            if (PlayerPrefsManager.ObtenerNumeroJugadorUno() == 0)
            {
                player1.SetActive(true);
                player2.SetActive(false);
                player1.GetComponent<Player>().GuardarNumeroJugador(0);
            }
            else
            {
                player1.SetActive(false);
                player2.SetActive(true);
                player2.GetComponent<Player>().GuardarNumeroJugador(0);
            }

            Invoke("MostrarUnJugador", 6);
        }
        else
        {
            if (PlayerPrefsManager.ObtenerNumeroJugadorUno() == 0)
            {
                player1.GetComponent<Player>().GuardarNumeroJugador(0);
                player2.GetComponent<Player>().GuardarNumeroJugador(1);
            }
            else
            {
                player1.GetComponent<Player>().GuardarNumeroJugador(1);
                player2.GetComponent<Player>().GuardarNumeroJugador(0);
            }

            player1.SetActive(true);
            player2.SetActive(true);

            uim.MostrarSeparador();
            player2.SetActive(true);
            Invoke("MostrarDosJugadores", 6);
        }
    }

    private void MostrarInicio()
    {
        uim.MostrarInicio();
    }

    private void MostrarUnJugador()
    {
        uim.MostrarUnJugador(PlayerPrefsManager.ObtenerNumeroJugadorUno(), vidas1, misiles1);
        estadoJuego = EstadoJuego.Empezado;
    }

    private void MostrarDosJugadores()
    {
        uim.MostrarDosJugadores(PlayerPrefsManager.ObtenerNumeroJugadorUno(), PlayerPrefsManager.ObtenerNumeroJugadorDos(), vidas1, misiles1, vidas2, misiles2);
        estadoJuego = EstadoJuego.Empezado;
    }

    public void RecargarEnergia(int cantidad, string nombreJugador)
    {
        if (energia1 < energiaMax1 && nombreJugador == "Zarco")
        {
            energia1 += cantidad;
            energia1 = Mathf.Min(energia1, energiaMax1);
            uim.ActualizarEnergia(energia1, nombreJugador);
        }
        else if (energia2 < energiaMax2 && nombreJugador == "Raven")
        {
            energia2 += cantidad;
            energia2 = Mathf.Min(energia2, energiaMax2);
            uim.ActualizarEnergia(energia2, nombreJugador);
        }
    }

    public void MejorarDisparo(int cantidad, string nombreJugador)
    {
        if (nivelDisparo1 < nivelDisparoMax1 && nombreJugador == "Zarco")
        {
            nivelDisparo1 += cantidad;
            nivelDisparo1 = Mathf.Min(nivelDisparo1, nivelDisparoMax1);
            uim.ActualizarDisparo(nivelDisparo1, nombreJugador);
        }
        else if (nivelDisparo2 < nivelDisparoMax2 && nombreJugador == "Raven")
        {
            nivelDisparo2 += cantidad;
            nivelDisparo2 = Mathf.Min(nivelDisparo2, nivelDisparoMax2);
            uim.ActualizarDisparo(nivelDisparo2, nombreJugador);
        }

    }

    public void RecargarMisiles(int cantidad, string nombreJugador)
    {
        if (misiles1 < misilesMax1 && nombreJugador == "Zarco")
        {
            misiles1 += cantidad;
            misiles1 = Mathf.Min(misiles1, misilesMax1);
            uim.ActualizarMisiles(misiles1, nombreJugador);
        }
        else if (misiles2 < misilesMax2 && nombreJugador == "Raven")
        {
            misiles2 += cantidad;
            misiles2 = Mathf.Min(misiles2, misilesMax2);
            uim.ActualizarMisiles(misiles2, nombreJugador);
        }
    }

    public void SumarPuntos(int puntos, string jugador)
    {
        if (jugador == "Zarco")
        {
            puntuacion1 += puntos;
            uim.ActualizarPuntuacion(puntuacion1, jugador);
        }
        else
        {
            puntuacion2 += puntos;
            uim.ActualizarPuntuacion(puntuacion2, jugador);
        }
    }

    public bool RecibirDano(int dano, string nombre)
    {
        bool resultado = false;

        if (nombre == "Zarco")
        {
            energia1 -= dano;

            if (energia1 <= 0)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            uim.ActualizarEnergia(energia1, nombre);
        }
        else
        {
            energia2 -= dano;

            if (energia2 <= 0)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }

            uim.ActualizarEnergia(energia2, nombre);
        }

        if (energia1 > 0)
        {
            if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
            {
                uim.MostrarDano();
            }
            else
            {
                uim.MostrarDanoJugador(nombre);
            }
        }

        return resultado;
    }

    public bool RestarVida(string nombre)
    {
        bool resultado;

        if (nombre == "Zarco")
        {
            vidas1--;

            if (vidas1 < 0)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
        }
        else
        {
            vidas2--;

            if (vidas2 < 0)
            {
                resultado = true;
            }
            else
            {
                resultado = false;
            }
        }

        return resultado;
    }

    public void ReiniciarJugador(string nombre)
    {
        if (nombre == "Zarco")
        {
            energia1 = energiaMax1;
            misiles1 = 5;
            nivelDisparo1 = 1;
        }
        else
        {
            energia2 = energiaMax2;
            misiles2 = 5;
            nivelDisparo2 = 1;
        }

        uim.OcultarJugador(nombre);
    }

    public void Empezar(string nombre)
    {
        if (nombre == "Zarco")
        {
            uim.ReiniciarJugador(nombre, vidas1, misiles1);
        }
        else
        {
            uim.ReiniciarJugador(nombre, vidas2, misiles2);
        }
    }

    public int ObtenerDisparo(string nombre)
    {
        if (nombre == "Zarco")
        {
            return nivelDisparo1;
        }
        else
        {
            return nivelDisparo2;
        }
    }

    public int ObtenerMisiles(string nombre)
    {
        if (nombre == "Zarco")
        {
            return misiles1;
        }
        else
        {
            return misiles2;
        }
    }

    public void RestarMisil(string nombre)
    {
        if (nombre == "Zarco")
        {
            misiles1--;
            uim.ActualizarMisiles(misiles1, nombre);
        }
        else
        {
            misiles2--;
            uim.ActualizarMisiles(misiles2, nombre);
        }
    }

    private void ComprobarFase()
    {
        faseActual++;
        StartCoroutine("CambiarFase");
    }

    private IEnumerator CambiarFase()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemigo").Length == 0)
            {
                fase = faseActual;
                StopCoroutine("CambiarFase");
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void ComprobarFase2()
    {
        faseActual++;
        StartCoroutine("CambiarFase2");
    }

    private IEnumerator CambiarFase2()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemigo").Length == 0)
            {
                fase = faseActual;
                StopCoroutine("CambiarFase2");
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void ReproducirMusicaBatallaFinal()
    {
        musica.clip = musicaJefeFinal1;
        musica.Play();
    }

    public void MostrarGameOver(string nombre)
    {
        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
        {
            uim.MostrarGameOver();
            PararJuego();
        }
        else
        {
            uim.MostrarGameOverJugador(nombre);

            if (nombre == "Zarco")
            {
                if (GameObject.Find("RavenMuerto") != null)
                {
                    uim.MostrarGameOver();
                    PararJuego();
                }
            }
            else
            {
                if (GameObject.Find("ZarcoMuerto") != null)
                {
                    uim.MostrarGameOver();
                    PararJuego();
                }
            }
        }
    }

    private void PararJuego()
    {
        estadoJuego = EstadoJuego.Terminado;
        uim.MostrarGameOver();
        musica.Stop();
        musica.loop = false;
        musica.clip = gameOver;
        musica.Play();
    }

    public void PausarJuego()
    {
        Time.timeScale = 0;
        musica.Pause();
        fondo.Pause();
        uim.PausarJuego();
    }

    public void DespausarJuego()
    {
        Time.timeScale = 1;
        musica.Play();
        fondo.Play();
        uim.DespausarJuego();
    }

    private void TerminarNivel()
    {
        musica.Stop();
        musica.clip = nivelCompletado;
        musica.loop = false;
        musica.Play();
        estadoJuego = EstadoJuego.Terminado;

        string[] nombresJugadores;
        int[] puntuaciones;
        string[] rangos;
        
        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 1)
        {
            nombresJugadores = new string[1];
            puntuaciones = new int[1];
            rangos = new string[1];

            if (PlayerPrefsManager.ObtenerNumeroJugadorUno() == 0)
            {
                nombresJugadores[0] = "Zarco";

                puntuaciones[0] = puntuacion1;

                if (puntuacion1 < 7500)
                {
                    rangos[0] = "D";
                }
                else if (puntuacion1 < 10000)
                {
                    rangos[0] = "C";
                }
                else if (puntuacion1 < 15000)
                {
                    rangos[0] = "B";
                }
                else if (puntuacion1 < 20000)
                {
                    rangos[0] = "A";
                }
                else
                {
                    rangos[0] = "S";
                }
            }
            else
            {
                nombresJugadores[0] = "Raven";

                puntuaciones[0] = puntuacion2;

                if (puntuacion2 < 7500)
                {
                    rangos[0] = "D";
                }
                else if (puntuacion2 < 10000)
                {
                    rangos[0] = "C";
                }
                else if (puntuacion2 < 15000)
                {
                    rangos[0] = "B";
                }
                else if (puntuacion2 < 20000)
                {
                    rangos[0] = "A";
                }
                else
                {
                    rangos[0] = "S";
                }
            }
        }
        else
        {
            nombresJugadores = new string[2];
            puntuaciones = new int[2];
            rangos = new string[2];

            if (PlayerPrefsManager.ObtenerNumeroJugadorUno() == 0)
            {
                nombresJugadores[0] = "Zarco";
                nombresJugadores[1] = "Raven";
            }
            else
            {
                nombresJugadores[0] = "Raven";
                nombresJugadores[1] = "Zarco";
            }

            puntuaciones[0] = puntuacion1;
            puntuaciones[1] = puntuacion2;

            if (puntuacion1 < 7500)
            {
                rangos[0] = "D";
            }
            else if (puntuacion1 < 10000)
            {
                rangos[0] = "C";
            }
            else if (puntuacion1 < 15000)
            {
                rangos[0] = "B";
            }
            else if (puntuacion1 < 20000)
            {
                rangos[0] = "A";
            }
            else
            {
                rangos[0] = "S";
            }


            if (puntuacion2 < 7500)
            {
                rangos[1] = "D";
            }
            else if (puntuacion1 < 10000)
            {
                rangos[1] = "C";
            }
            else if (puntuacion1 < 15000)
            {
                rangos[1] = "B";
            }
            else if (puntuacion1 < 20000)
            {
                rangos[1] = "A";
            }
            else
            {
                rangos[1] = "S";
            }
        }

        uim.MostrarFinNivel(nombresJugadores, puntuaciones, rangos);
    }

    public void ActualizarFase(int faseNueva)
    {
        faseActual = faseNueva;
        fase = faseActual;
    }

    public void Paniaguizar()
    {
        vidas1 = vidasMax1;
        energia1 = energiaMax1;
        misiles1 = 10;
        nivelDisparo1 = 3;
        vidas2 = vidasMax2;
        energia2 = energiaMax2;
        misiles2 = 10;
        nivelDisparo2 = 3;
        uim.PaniaguizarJugadores();
    }
}
    