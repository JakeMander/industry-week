using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
    public GameObject panel;
    public bool showing;

    // Update is called once per frame
    void Update()
    {

        //  If Right Mouse Is Clicked
        if (Input.GetMouseButtonDown(1))
        {
            //  Switch "showing" flag to Opposite State And Pass To SetActive To Determine Visiblity Of Menu.
            showing = !showing;
            panel.SetActive(showing);
        }
    }
}
