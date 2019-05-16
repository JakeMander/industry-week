using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public string URL = "";
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(RestClient.Instance.Get(URL, GetObjects));
        StartCoroutine(RestClient.Instance.Post(URL, new Objects
        {
            id = 5,
            type = "pointer",
            posx = 10,
            posy = 10,
            rotation = 10,
            posz = 10
        }, GetObjects));
    }

    void GetObjects(ObjectsList objectsList)
    {
        foreach(Objects objects in objectsList.objects)
        {
            Debug.Log("Object ID : " + objects.id);
        }
    }        

}
