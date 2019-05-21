using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFlagMarkerUI : MonoBehaviour
{
    //  Prefab References So We Can Instantiate New Objects.
    public GameObject _userCamera;
    public GameObject _canvas2;

    //  Store A Reference To Our New UI Menu.
    public GameObject _newMenu = null;

    private void OnMouseDown()
    {
        Debug.Log("Load Flag Edit Menu");
        if (_newMenu == null)
        {
            Debug.Log("Begin Flag Edit Menu Instantiations");
            _newMenu = Instantiate(_canvas2, _userCamera.transform.position + (_userCamera.transform.forward * 5), Quaternion.identity);
            _newMenu.transform.LookAt(_userCamera.transform);
          
        }

        else
        {
            Destroy(_newMenu);
            _newMenu = null;
        }
    }
}
