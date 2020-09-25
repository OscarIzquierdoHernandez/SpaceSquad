using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("MoverBackground", 7);
    }

    private void MoverBackground()
    {
        transform.DOScale(Vector3.one * 2900, 180);
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, -1800), 480);
    }
}
