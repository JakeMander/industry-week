using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    private static RestClient _instance;

    public static RestClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RestClient>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RestClient).Name;
                    _instance = go.AddComponent<RestClient>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }

    }

    public IEnumerator Get(string url)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if(www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
