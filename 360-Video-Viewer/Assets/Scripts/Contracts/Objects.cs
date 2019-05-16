using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//System.Serializable allows it to serialized and de-serialized for JSON use
[System.Serializable]
public class Objects
{
    //Values in Db
    public int id;
    public string type;
    public int posx;
    public int posy;
    public int rotation;
    public int posz;
}
