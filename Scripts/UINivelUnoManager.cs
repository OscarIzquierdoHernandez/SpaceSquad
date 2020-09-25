using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UINivelUnoManager : MonoBehaviour
{
    [SerializeField] GameObject[] paneles;
    [SerializeField] GameObject[] mirillas;
    [SerializeField] GameObject[] minimapas;
    [SerializeField] GameObject[] avatares;
    [SerializeField] GameObject[] barrasEnergia;
    [SerializeField] GameObject[] barrasDisparo;
    [SerializeField] GameObject[] barrasMisiles;
    [SerializeField] GameObject[] imagenesDano;
    [SerializeField] GameObject[] avataresPuntuacion;
    [SerializeField] GameObject[] panelPuntuaciones;
    [SerializeField] Text[] textoVidas;
    [SerializeField] Text[] textoPuntuacion;
    [SerializeField] Text[] textosGameOver;
    [SerializeField] Text[] textoPuntuacion2;
    [SerializeField] Text[] textoRangos;
    [SerializeField] Image[] pantallasNegras;
    [SerializeField] Text textoPrincipal;
    [SerializeField] Text textoNivelCompletado;
    [SerializeField] Image pantallaNegra;
    [SerializeField] GameObject separador;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject warning2;
    [SerializeField] GameObject panelBotones;
    [SerializeField] Button reiniciarBoton;
    [SerializeField] Button salirBoton;


    public void FundirNegro(float alpha, float tiempo)
    {
        pantallaNegra.CrossFadeAlpha(alpha, tiempo, true);
    }

    public void MostrarInicio()
    {
        textoPrincipal.gameObject.SetActive(true);
        Sequence s = DOTween.Sequence();
        s.Append(textoPrincipal.DOColor(new Color(1, 1, 1, 1), 0.5f));
        s.Append(textoPrincipal.DOColor(new Color(1, 1, 1, 0), 0.5f));
        Invoke("MostrarInicio2", 3.5f);
    }

    public void MostrarInicio2()
    {
        textoPrincipal.text = "START";
        textoPrincipal.color = new Color(1, 1, 1, 1);
        textoPrincipal.transform.DOShakeScale(0.5f);
        Invoke("MostrarInicio3", 0.5f);
    }

    public void MostrarInicio3()
    {
        textoPrincipal.gameObject.SetActive(false);
    }

    public void MostrarSeparador()
    {
        separador.SetActive(true);
    }

    public void MostrarUnJugador(int jugadorUno, int vidas, int misiles)
    {
        paneles[0].SetActive(true);

        if (jugadorUno == 0)
        {
            avatares[0].SetActive(true);
            avatares[1].SetActive(false);
            barrasEnergia[0].SetActive(true);
            barrasEnergia[1].SetActive(false);
        }
        else
        {
            avatares[0].SetActive(false);
            avatares[1].SetActive(true);
            barrasEnergia[0].SetActive(false);
            barrasEnergia[1].SetActive(true);
        }

        Image[] imgs = barrasDisparo[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        imgs[0].enabled = true;
        imgs[1].enabled = true;

        imgs = barrasMisiles[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        imgs[0].enabled = true;

        for (int i = 1; i < imgs.Length; i++)
        {
            if (i <= misiles)
            {
                imgs[i].enabled = true;
            }
        }

        mirillas[0].SetActive(true);
        mirillas[1].SetActive(false);
        mirillas[2].SetActive(false);
        minimapas[0].SetActive(true);
        minimapas[1].SetActive(false);
        minimapas[2].SetActive(false);
        textoVidas[0].text = vidas.ToString();
        textoPuntuacion[0].text = "0";
    }

    public void ReiniciarJugador(string nombre, int vidas, int misiles)
    {
        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
        {

            if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
            {
                paneles[0].SetActive(true);
                mirillas[1].SetActive(true);
                minimapas[1].SetActive(true);

            }
            else
            {
                paneles[1].SetActive(true);
                mirillas[2].SetActive(true);
                minimapas[2].SetActive(true);
            }
        }
        else
        {
            paneles[0].SetActive(true);
            mirillas[0].SetActive(true);
            minimapas[0].SetActive(true);
        }

        int posicion;

        if (nombre == "Zarco")
        {
            if (avatares[0].activeSelf)
            {
                posicion = 0;
            }
            else
            {
                posicion = 2;
            }
        }
        else
        {
            if (avatares[1].activeSelf)
            {
                posicion = 1;
            }
            else
            {
                posicion = 3;
            }
        }

        Image[] imgs = barrasEnergia[posicion].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }


        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            posicion = 0;
        }
        else
        {
            posicion = 1;
        }

        imgs = barrasDisparo[posicion].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        imgs[0].enabled = true;
        imgs[1].enabled = true;

        imgs = barrasMisiles[posicion].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= misiles)
            {
                imgs[i].enabled = true;
            }
        }

        textoVidas[posicion].text = vidas.ToString();
    }

    public void MostrarDosJugadores(int jugadorUno, int jugadorDos, int vidas1, int misiles1, int vidas2, int misiles2)
    {
        paneles[0].SetActive(true);
        paneles[1].SetActive(true);

        if (jugadorUno == 0)
        {
            avatares[0].SetActive(true);
            avatares[1].SetActive(false);
            avatares[2].SetActive(false);
            avatares[3].SetActive(true);
            barrasEnergia[0].SetActive(true);
            barrasEnergia[1].SetActive(false);
            barrasEnergia[2].SetActive(false);
            barrasEnergia[3].SetActive(true);
        }
        else
        {
            avatares[0].SetActive(false);
            avatares[1].SetActive(true);
            avatares[2].SetActive(true);
            avatares[3].SetActive(false);
            barrasEnergia[0].SetActive(false);
            barrasEnergia[1].SetActive(true);
            barrasEnergia[2].SetActive(true);
            barrasEnergia[3].SetActive(false);
        }

        Image[] imgs = barrasDisparo[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        imgs[0].enabled = true;
        imgs[1].enabled = true;

        imgs = barrasMisiles[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }
        
        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= misiles1)
            {
                imgs[i].enabled = true;
            }
        }

        imgs = barrasDisparo[1].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        imgs[0].enabled = true;
        imgs[1].enabled = true;

        imgs = barrasMisiles[1].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }
        
        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= misiles2)
            {
                imgs[i].enabled = true;
            }
        }

        mirillas[0].SetActive(false);
        mirillas[1].SetActive(true);
        mirillas[2].SetActive(true);
        minimapas[0].SetActive(false);
        minimapas[1].SetActive(true);
        minimapas[2].SetActive(true);
        textoVidas[0].text = vidas1.ToString();
        textoVidas[1].text = vidas2.ToString();
        textoPuntuacion[0].text = "0";
        textoPuntuacion[1].text = "0";
    }

    public void ActualizarPuntuacion(int puntuacion, string nombre)
    {
        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            textoPuntuacion[0].text = puntuacion.ToString();
        }
        else
        {
            textoPuntuacion[1].text = puntuacion.ToString();
        }
    }

    public void ActualizarEnergia(int energia, string nombre)
    {
        Image[] imgs;

        if (nombre == "Zarco")
        {
            if (avatares[0].activeSelf)
            {
                imgs = barrasEnergia[0].GetComponentsInChildren<Image>();
            }
            else
            {
                imgs = barrasEnergia[2].GetComponentsInChildren<Image>();
            }
        }
        else
        {
            if (avatares[1].activeSelf)
            {
                imgs = barrasEnergia[1].GetComponentsInChildren<Image>();
            }
            else
            {
                imgs = barrasEnergia[3].GetComponentsInChildren<Image>();
            }
        }

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= energia)
            {
                imgs[i].enabled = true;
            }
        }
    }

    public void ActualizarDisparo(int disparo, string nombre)
    {
        Image[] imgs;

        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            imgs = barrasDisparo[0].GetComponentsInChildren<Image>();
        }
        else
        {
            imgs = barrasDisparo[1].GetComponentsInChildren<Image>();
        }

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= disparo)
            {
                imgs[i].enabled = true;
            }
        }
    }

    public void ActualizarMisiles(int disparo, string nombre)
    {
        Image[] imgs;

        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            imgs = barrasMisiles[0].GetComponentsInChildren<Image>();
        }
        else
        {
            imgs = barrasMisiles[1].GetComponentsInChildren<Image>();
        }

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        for (int i = 0; i < imgs.Length; i++)
        {
            if (i <= disparo)
            {
                imgs[i].enabled = true;
            }
        }
    }

    public void ActualizrVidas(int vidas, string nombre)
    {
        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        { 
            textoVidas[0].text = vidas.ToString();
        }
        else
        {
            textoVidas[1].text = vidas.ToString();
        }        
    }

    public void OcultarJugador(string nombre)
    {
        if (PlayerPrefsManager.ObtenerNumeroJugadores() == 2)
        {
            if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
            {
                paneles[0].SetActive(false);
                mirillas[1].SetActive(false);
                minimapas[1].SetActive(false);

            }
            else
            {
                paneles[1].SetActive(false);
                mirillas[2].SetActive(false);
                minimapas[2].SetActive(false);
            }
        }
        else
        {
            paneles[0].SetActive(false);
            mirillas[0].SetActive(false);
            minimapas[0].SetActive(false);
        }
    }

    public void MostrarWarning()
    {
        warning.SetActive(true);

        warning.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0.2f), 0.66f).SetLoops(6, LoopType.Yoyo);
        warning2.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 0.66f).SetLoops(6, LoopType.Yoyo);
    }

    public void MostrarDano()
    {
        imagenesDano[0].SetActive(true);
        imagenesDano[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        imagenesDano[0].GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1);
    }

    public void MostrarDanoJugador(string nombre)
    {
        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            imagenesDano[1].SetActive(true);
            imagenesDano[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imagenesDano[1].GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1);
        }
        else
        {
            imagenesDano[2].SetActive(true);
            imagenesDano[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            imagenesDano[2].GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1);
        }
    }

    public void MostrarGameOver()
    {
        pantallaNegra.gameObject.SetActive(true);
        textosGameOver[0].gameObject.SetActive(true);
        textosGameOver[0].text = "Game Over";
        pantallaNegra.CrossFadeAlpha(1, 2, true);
        textosGameOver[0].gameObject.transform.DOScale(1, 2).SetUpdate(true);
        Invoke("MostrarBotones", 2);
        
    }

    public void MostrarGameOverJugador(string nombre)
    {
        if ((nombre == "Zarco" && avatares[0].activeSelf) || (nombre == "Raven" && avatares[1].activeSelf))
        {
            pantallasNegras[0].gameObject.SetActive(true);
            textosGameOver[1].gameObject.SetActive(true);
            pantallasNegras[0].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            pantallasNegras[0].GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 2);
            textosGameOver[1].gameObject.transform.DOScale(1, 2);
        }
        else
        {
            pantallasNegras[1].gameObject.SetActive(true);
            textosGameOver[2].gameObject.SetActive(true);
            pantallasNegras[1].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            pantallasNegras[1].GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 2);
            textosGameOver[2].gameObject.transform.DOScale(1, 2);            
        }
    }

    public void PausarJuego()
    {
        textosGameOver[0].gameObject.SetActive(true);
        textosGameOver[0].text = "Pause";
        textosGameOver[0].gameObject.transform.DOScale(1, 0).SetUpdate(true);
        panelBotones.SetActive(true);
        panelBotones.transform.DOScale(1, 0).SetUpdate(true);
    }

    public void DespausarJuego()
    {
        textosGameOver[0].gameObject.SetActive(false);
        panelBotones.SetActive(false);
        panelBotones.transform.DOScale(0, 0).SetUpdate(true);
    }

    public void MostrarFinNivel(string[] nombreJugadores, int[] puntuaciones, string[] rangos)
    {
        paneles[0].SetActive(false);
        paneles[1].SetActive(false);
        mirillas[0].SetActive(false);
        mirillas[1].SetActive(false);
        mirillas[2].SetActive(false);
        minimapas[0].SetActive(false);
        minimapas[1].SetActive(false);
        minimapas[2].SetActive(false);


        pantallaNegra.gameObject.SetActive(true);
        textoNivelCompletado.gameObject.SetActive(true);
        panelPuntuaciones[0].SetActive(true);
        panelPuntuaciones[1].SetActive(true);
        
        if (nombreJugadores.Length == 1)
        {
            if (nombreJugadores[0] == "Zarco")
            {
                avataresPuntuacion[0].SetActive(true);
            }
            else
            {
                avataresPuntuacion[1].SetActive(true);
            }

            textoPuntuacion2[0].text = puntuaciones[0].ToString();
            textoRangos[0].text = rangos[0];

            switch (textoRangos[0].text)
            {
                case "D":

                    textoRangos[0].fontSize = 28;
                    break;

                case "C":

                    textoRangos[0].fontSize = 31;
                    break;

                case "B":

                    textoRangos[0].fontSize = 34;
                    break;

                case "A":

                    textoRangos[0].fontSize = 37;
                    break;

                case "S":

                    textoRangos[0].fontSize = 40;
                    break;
            }
        }
        else
        {
            panelPuntuaciones[2].SetActive(true);

            if (nombreJugadores[0] == "Zarco")
            {
                avataresPuntuacion[0].SetActive(true);
                avataresPuntuacion[3].SetActive(true);
                textoPuntuacion2[0].text = puntuaciones[0].ToString();
                textoPuntuacion2[1].text = puntuaciones[1].ToString();
                textoRangos[0].text = rangos[0];
                textoRangos[1].text = rangos[1];
            }
            else
            {
                avataresPuntuacion[1].SetActive(true);
                avataresPuntuacion[2].SetActive(true);
                textoPuntuacion2[0].text = puntuaciones[1].ToString();
                textoPuntuacion2[1].text = puntuaciones[0].ToString();
                textoRangos[0].text = rangos[1];
                textoRangos[1].text = rangos[0];
            }

            switch (textoRangos[0].text)
            {
                case "D":

                    textoRangos[0].fontSize = 28;
                    break;

                case "C":

                    textoRangos[0].fontSize = 31;
                    break;

                case "B":

                    textoRangos[0].fontSize = 34;
                    break;

                case "A":

                    textoRangos[0].fontSize = 37;
                    break;

                case "S":

                    textoRangos[0].fontSize = 40;
                    break;
            }
            
            switch (textoRangos[1].text)
            {
                case "D":

                    textoRangos[1].fontSize = 28;
                    break;

                case "C":

                    textoRangos[1].fontSize = 31;
                    break;

                case "B":

                    textoRangos[1].fontSize = 34;
                    break;

                case "A":

                    textoRangos[1].fontSize = 37;
                    break;

                case "S":

                    textoRangos[1].fontSize = 40;
                    break;
            }
        }


        pantallaNegra.color = new Color(0, 0, 0.2f);
        pantallaNegra.CrossFadeAlpha(0.3f, 2, true);

        textoNivelCompletado.gameObject.transform.position = new Vector3(textoNivelCompletado.gameObject.transform.position.x, (textoNivelCompletado.gameObject.transform.position.y - 300), textoNivelCompletado.gameObject.transform.position.z);

        Sequence s = DOTween.Sequence();
        
        s.Append(textoNivelCompletado.gameObject.transform.DOMoveY((textoNivelCompletado.gameObject.transform.position.y + 300), 1f));
        s.Append(panelPuntuaciones[0].transform.DOScale(1, 1f));
        s.Append(textoRangos[0].gameObject.transform.DOScale(1, 1f));

        if (nombreJugadores.Length == 2)
        {
            s.Append(textoRangos[1].gameObject.transform.DOScale(1, 1f));
        }

        Invoke("MostrarBotones", 4);
    }

    private void MostrarBotones()
    {
        Time.timeScale = 0;
        panelBotones.SetActive(true);
        panelBotones.transform.localScale = Vector3.zero;
        panelBotones.transform.DOScale(1, 2).SetUpdate(true);
    }

    public void Salir()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1;
        GameManager.estadoJuego = GameManager.EstadoJuego.Empezando;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PaniaguizarJugadores()
    {
        Image[] imgs;

        imgs = barrasEnergia[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasEnergia[1].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasEnergia[2].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasEnergia[3].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasDisparo[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasDisparo[1].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasMisiles[0].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        imgs = barrasMisiles[1].GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = true;
        }

        textoVidas[0].text = "99";
        textoVidas[1].text = "99";
    }


    // EFECTOS BOTONES
    
    public void AgrandarReiniciar()
    {
        reiniciarBoton.transform.DOScale(1.2f, 0).SetUpdate(true); ;
    }

    public void AgrandarSalir()
    {
        salirBoton.transform.DOScale(1.2f, 0).SetUpdate(true); ;
    }
    
    public void EncogerReiniciar()
    {
        reiniciarBoton.transform.DOScale(1, 0).SetUpdate(true); ;
    }

    public void EncogerSalir()
    {
        salirBoton.transform.DOScale(1, 0).SetUpdate(true); ;
    }
}
