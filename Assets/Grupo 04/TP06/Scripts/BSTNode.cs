using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo<T>
{
    public T datos;
    public Nodo<T> izq, der;

    public Nodo(T datos)
    {
        this.datos = datos;
        izq = null;
        der = null;
    }
    public Nodo(T datos, Nodo<T> izq, Nodo<T> der)
    {
        this.datos = datos;
        this.izq = izq;
        this.der = der;
    }
}