using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public string URL = "";
    // Start is called before the first frame update
    void Start()
    {

        //This StartCoroutine runs the Get from RestClient.cs
        //StartCoroutine(RestClient.Instance.Get(URL, 1, GetObjects));

        //This StartCoroutine runs the Post from RestClient.cs
        StartCoroutine(RestClient.Instance.Post(URL, new Objects
        {
            //Values are static right now but can be changed to be dynamic
            id = 5,
            type = "pointer",
            posx = 10,
            posy = 10,
            rotation = 10,
            posz = 10
        }, GetObjects));
    }

    
    //Gets a list of objects and should post id in Object ID
    void GetObjects(ObjectsList objectsList)
    {
        foreach(Objects objects in objectsList.objects)
        {
            Debug.Log("Object ID : " + objects.id);
        }
    }        

}
