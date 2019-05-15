using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMousePopUp : MonoBehaviour
{

    public GameObject Panel;
    
    // Start is called before the first frame update
    void Start()
    {
        Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
