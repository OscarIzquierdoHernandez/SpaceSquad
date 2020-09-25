using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor2 : MonoBehaviour
{ 
    void Update()
    {
        if (transform.position.z < 0)
        {
            Destroy(gameObject);
        }
    }
}
