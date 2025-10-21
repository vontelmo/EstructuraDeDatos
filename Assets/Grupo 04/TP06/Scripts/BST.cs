using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BST<T> where T : IComparable<T>
{
    public Nodo<T> Root { get; private set; }

    public void Insert(T value)
    {
        Root = InsertRecursive(value, Root);
    }

    Nodo<T> InsertRecursive(T value, Nodo<T> current)
    {
        //Chequeamos si el nodo NO existe
        if (current == null)
            
            //Si no existe, insertamos ahi
            return new Nodo<T>(value);

        //Si existe, chequeamos si es mayor
        int cmp = value.CompareTo(current.datos);

        //Si es mayor, llamamos a InsertRecursive con el hijo derecho
        if (cmp < 0)
            current.izq = InsertRecursive(value, current.izq);

        //Si es menor, llamamos a InsertRecursive con el hijo izquierdo
        else if (cmp >= 0)
            current.der = InsertRecursive(value, current.der);

        // Si es igual, no hace nada (no acepta duplicados)
        return current;
    }

    public int GetHeight()
    {
        return GetHeightRecursive(Root);
    }

    int GetHeightRecursive(Nodo<T> current)
    {
        if (current == null)
        return 0;
       return Mathf.Max(GetHeightRecursive(current.izq), GetHeightRecursive(current.der));
    }
    
    int GetBalanceFactor(Nodo<T> nodo)
    {
        return GetBalanceFactor(Root);
    }

    public void InOrder()
    {
        InOrderRecursive(Root);
        //obtener valores en ascendente
    }

    void InOrderRecursive(Nodo<T> Nodo)
    {
        if (Nodo == null) return;
        InOrderRecursive(Nodo.izq);
        Debug.Log(Nodo.datos);
        InOrderRecursive(Nodo.der);
    }
    public void PreOrder()
    {
        PreOrderRecursive(Root);
        //copiar o serializar arbol
    }

    void PreOrderRecursive(Nodo<T> Nodo)
    {
        if (Nodo ==null)return;
        Debug.Log(Nodo.datos);
        PreOrderRecursive(Nodo.izq);
        PreOrderRecursive(Nodo.der);
    }

    public void PostOrder()
    {
        PostOrderRecursive(Root);
        //eliminar nodos
    }

    void PostOrderRecursive(Nodo<T> nodo)
    {
        if (nodo == null) return;

        PostOrderRecursive(nodo.izq); 
        PostOrderRecursive(nodo.der);
        Debug.Log(nodo.datos);
    }

    public void LevelOrder()
        //recorrer el arbol de izquierda a derecha
    {
        if (Root == null) return;
        Queue<Nodo<T>> cola = new Queue<Nodo<T>>();
        cola.Enqueue(Root);

        while (cola.Count > 0)
        {
            Nodo<T> current = cola.Dequeue();
            Debug.Log(current.datos);

            if (current.izq != null) cola.Enqueue(current.izq);
            if (current.der != null) cola.Enqueue(current.der);
        }
    }
}