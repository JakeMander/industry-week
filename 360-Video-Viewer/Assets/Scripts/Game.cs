using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public string URL = "https://computing.derby.ac.uk/~partypool/IndustryWeek/";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RestClient.Instance.Get(URL));
    }


}
