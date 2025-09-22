using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{

    public int ID;
    public string objName;
    public int price;
    public string quality;
    public string type;
    public Sprite icon;

}
