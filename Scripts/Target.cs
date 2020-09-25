using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField] Transform destino;

    void Start()
    {
        transform.DOScale(1, 1).SetLoops(3);
        GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 1).SetLoops(3);
        Invoke("Destruir", 5);
    }

    private void Update()
    {
        Vector3 v = new Vector3(destino.position.x, transform.position.y, destino.position.z);
        transform.LookAt(v);
    }

    private void Destruir()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
