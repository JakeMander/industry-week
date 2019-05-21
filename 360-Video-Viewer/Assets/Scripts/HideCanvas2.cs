using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvas2 : MonoBehaviour
{
    GameObject canvas2;
    // Start is called before the first frame update
    void Start()
    {
        Renderer r = canvas2.GetComponent<Renderer>();
        r.enabled = false;
    }
}
