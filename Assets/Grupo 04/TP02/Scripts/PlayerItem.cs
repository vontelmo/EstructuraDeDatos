using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerItem
{
    public Item item;
    public int quantity;

    public PlayerItem(Item i, int cantidadInicial = 1)
    {
        item = i;
        quantity = cantidadInicial;
    }
}

