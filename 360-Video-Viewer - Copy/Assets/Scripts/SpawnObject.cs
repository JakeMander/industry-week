using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    private int _numberOfSpawnedFlags = 0;
    private int _numberOfSpawnedWarnings = 0;
    private int _numberOfSpawnedBolts = 0;

    private List<GameObject> _flagList = new List<GameObject>();
    private List<GameObject> _WarningList = new List<GameObject>();
    private List<GameObject> _BoltList = new List<GameObject>();

    //  Store References To Each Button So We Can Assign An On Click Listener.
    public Button _flagButton;
    public Button _exclamationButton;
    public Button _electricityButton;

    //  Store The References To The Models We Want To Spawn Into The Scene.
    public GameObject _flag;
    public GameObject _exclamationMark;
    public GameObject _electricity;

    //  Store The Reference To Our Camera So We Can Spawn In Our Objects At A Distance And Rotation
    //  Relative To The Camera.
    public GameObject _userCamera;

    // Start is called before the first frame update
    void Start()
    {
        _flagButton.onClick.AddListener(SpawnFlag);
        _electricityButton.onClick.AddListener(SpawnElectricity);
        _exclamationButton.onClick.AddListener(SpawnWarning);
    }

    void SpawnFlag()
    {
        GameObject newInstance = null;
       
        float cameraXPos = _userCamera.gameObject.transform.position.x;
        float cameraYPos = _userCamera.gameObject.transform.position.y;
        float cameraZPos = _userCamera.gameObject.transform.position.z;

        Debug.Log("Flag Button Clicked");
        newInstance = Instantiate(_flag, _userCamera.transform.position + (_userCamera.transform.forward * 5), Quaternion.identity);

        //  Enable Renderer To Display Menu.
        Renderer r = newInstance.GetComponent<Renderer>();
        r.enabled = true;

        newInstance.transform.LookAt(_userCamera.transform);

    }

    void SpawnElectricity()
    {
        GameObject newInstance = null;

        float cameraXPos = GetComponent<Camera>().gameObject.transform.position.x;
        float cameraYPos = GetComponent<Camera>().gameObject.transform.position.y;
        float cameraZPos = GetComponent<Camera>().gameObject.transform.position.z;

        Debug.Log("Flag Button Clicked");
        newInstance = Instantiate(_electricity, transform.position + (transform.forward * 3), Quaternion.identity);
        newInstance.transform.LookAt(GetComponent<Camera>().transform);
    }

    void SpawnWarning()
    {
        GameObject newInstance = null;

        float cameraXPos = GetComponent<Camera>().gameObject.transform.position.x;
        float cameraYPos = GetComponent<Camera>().gameObject.transform.position.y;
        float cameraZPos = GetComponent<Camera>().gameObject.transform.position.z;

        Debug.Log("Flag Button Clicked");
        newInstance = Instantiate(_exclamationMark, transform.position + (transform.forward * 3), Quaternion.identity);
        newInstance.transform.LookAt(GetComponent<Camera>().transform);
    }
}
