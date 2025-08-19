using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerItem
{
    public Item item;
    public int cantidad;

    public PlayerItem(Item i, int cantidadInicial = 1)
    {
        item = i;
        cantidad = cantidadInicial;
    }
}


