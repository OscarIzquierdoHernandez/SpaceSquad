using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoFinal : Enemigo
{
    private GameManager gm;
    private Material[] materialesOriginales;

    private void Start()
    {
        base.Start();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        materialesOriginales = GetComponentInChildren<SkinnedMeshRenderer>().materials;
    }

    public override void RecibirDano(int dano, int tipoBala)
    {
        if (recibirDano)
        {
            vida -= dano;

            if (vida <= 0)
            {
                recibirDano = false;

                if (tipoBala == 0)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos((puntosDano + puntosMuerte), "Zarco");
                }
                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos((puntosDano + puntosMuerte), "Raven");
                }

                gm.ActualizarFase(5);
            }
            else
            {
                SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();
                Material[] ms = smr.materials;
                ms[0] = material;
                ms[1] = material;
                ms[2] = material;
                smr.materials = ms;
                

                foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                {
                    ms = mr.materials;

                    for (int i = 0; i < mr.materials.Length; i++)
                    {
                        ms[i] = material;
                    }

                    mr.materials = ms;
                }

                Invoke("QuitarRecibiendoDano", 0.01f);

                if (tipoBala == 0)
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos(puntosDano, "Zarco");
                }
                else
                {
                    GameObject.Find("GameManager").GetComponent<GameManager>().SumarPuntos(puntosDano, "Raven");
                }
            }
        }
    }

    protected override void QuitarRecibiendoDano()
    {
        SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] ms = smr.materials;
        ms[0] = materialesOriginales[0];
        ms[1] = materialesOriginales[1];
        ms[2] = materialesOriginales[2];
        smr.materials = ms;

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            ms = mr.materials;

            for (int i = 0; i < mr.materials.Length; i++)
            {
                ms[i] = materialOriginal;
            }

            mr.materials = ms;
        }
    }
}
