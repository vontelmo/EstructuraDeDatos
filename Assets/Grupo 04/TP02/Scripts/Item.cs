using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum ItemsQuality { Legendary, Rare, Uncommon, Common };
public enum ItemsType { Weapon, Consumable, RangeWeapon, Armor };


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class Item : ScriptableObject
{

    public int ID;
    public string objName;
    public int price;
    public ItemsQuality quality;
    public ItemsType type;
    public Sprite icon;

    public void ChangePrice(int delta)
    {
        price = Mathf.Max(1, price + delta);
    }

}