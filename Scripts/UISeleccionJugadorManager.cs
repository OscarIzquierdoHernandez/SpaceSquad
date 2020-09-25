using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UISeleccionJugadorManager : MonoBehaviour
{
    [SerializeField] GameObject[] panelesJugadores;
    [SerializeField] GameObject[] flechas;
    [SerializeField] GameObject[] jugadores;
    [SerializeField] Image pantallaNegra;
    
    void Start()
    {
        pantallaNegra.CrossFadeAlpha(0, 2, false);
        pantallaNegra.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f).SetLoops(4,LoopType.Incremental);
        pantallaNegra.transform.DOScale(new Vector3(0, 0, 0), 2);
    }

    public void IniciarUI(int numeroJugadores)
    {
        if (numeroJugadores == 1)
        {
            panelesJugadores[0].SetActive(false);
            panelesJugadores[1].SetActive(false);
            jugadores[0].SetActive(false);
            jugadores[1].SetActive(false);
        }
    }

    public void MostrarZarco()
    {
        panelesJugadores[0].SetActive(true);
        panelesJugadores[1].SetActive(false);
        flechas[0].SetActive(false);
        flechas[1].SetActive(true);
    }

    public void MostrarRaven()
    {
        panelesJugadores[0].SetActive(false);
        panelesJugadores[1].SetActive(true);
        flechas[0].SetActive(true);
        flechas[1].SetActive(false);
    }

    public void CambiarJugadores()
    {
        string texto = jugadores[0].GetComponent<Text>().text;
        jugadores[0].GetComponent<Text>().text = jugadores[1].GetComponent<Text>().text;
        jugadores[1].GetComponent<Text>().text = texto;
        jugadores[0].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.05f).SetLoops(2, LoopType.Yoyo);
        jugadores[1].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.05f).SetLoops(2, LoopType.Yoyo);
    }

    public void CambiarEscena()
    {
        //pantallaNegra.transform.rotation = Quaternion.Euler(Vector3.zero);
        pantallaNegra.CrossFadeAlpha(1, 1, false);
        //pantallaNegra.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f).SetLoops(2, LoopType.Incremental);
        pantallaNegra.transform.DOScale(new Vector3(1, 1, 1), 1);
        Invoke("CambiarEscena2", 1.5f);
    }

    private void CambiarEscena2()
    {
        SceneManager.LoadScene(2);
    }

    public void CambiarEscena3()
    {
        pantallaNegra.CrossFadeAlpha(1, 1, false);
        pantallaNegra.transform.DOScale(new Vector3(1, 1, 1), 1);
        Invoke("CambiarEscena4", 1.5f);
    }

    private void CambiarEscena4()
    {
        SceneManager.LoadScene(0);
    }

    public string ObtenerTextoJugador(int numeroJugador)
    {
        return jugadores[numeroJugador].GetComponent<Text>().text;
    }
}
