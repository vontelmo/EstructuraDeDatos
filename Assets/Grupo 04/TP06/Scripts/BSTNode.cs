using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public T Value;
    public Node<T> left, right;

    public Node(T datos)
    {
        this.Value = datos;
        left = null;
        right = null;
    }
    public Node(T datos, Node<T> izq, Node<T> der)
    {
        this.Value = datos;
        this.left = izq;
        this.right = der;
    }
}
