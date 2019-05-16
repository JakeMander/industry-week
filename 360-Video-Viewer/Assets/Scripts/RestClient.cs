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

    public IEnumerator Get(string url, System.Action<ObjectsList> callback)
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

                    ObjectsList objectList = JsonUtility.FromJson<ObjectsList>(jsonResult);

                    callback(objectList);
                }
            }
        }
    }

    public IEnumerator Post(string url, Objects newObjects, System.Action<ObjectsList> callback)
    {
        string jsonData = JsonUtility.ToJson(newObjects);
        Debug.Log("Json Data: " + jsonData);
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
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
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(" Json Result : " + jsonResult);
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
