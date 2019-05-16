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
                    //Keeps GameObject go on load
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }

    }

    //The Get, should send off to webservice and return objects using their id
    public IEnumerator Get(string url, int id, System.Action<ObjectsList> callback)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url + "/GETOBJECT/" + id))
        {
            yield return www.SendWebRequest();

            //Makes sure there is no Networking error or returns a debug log
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if(www.isDone)
                {
                    //data from webservice into string
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    //Displays the JsonResult in debug log
                    Debug.Log(jsonResult);

                    ObjectsList objectList = JsonUtility.FromJson<ObjectsList>(jsonResult);

                    callback(objectList);
                    
                }
            }
        }
    }

    //The Post, should send data off to web service to be put into database
    public IEnumerator Post(string url, Objects newObjects, System.Action<ObjectsList> callback)
    {
        string jsonData = JsonUtility.ToJson(newObjects);
        //Shows in Debug log of whats in the jsonData that will be send over the UnityWebRequest
        Debug.Log("Json Data: " + jsonData);
        using (UnityWebRequest www = UnityWebRequest.Post(url + "/CREATEOBJECT", jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            //Uploads jsonData 
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {

                if (www.isDone)
                {
                    //data from web service as string
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    //Puts data from web service in debug log
                    Debug.Log(" Json Result : " + jsonResult);

                    //flls list with object from web service
                    ObjectsList objectList = JsonUtility.FromJson<ObjectsList>(jsonResult);

                    callback(objectList);
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
