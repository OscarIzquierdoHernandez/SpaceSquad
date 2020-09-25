using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivadorAnimator : MonoBehaviour
{
    public void DesactivarAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
