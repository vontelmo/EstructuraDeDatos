using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyStack<T>
{
    public int Count { get; private set; }

    private T[] stackArray;
    private const int defaultSize = 4;

    public MyStack()
    {
        stackArray = new T[defaultSize];
        Count = 0;
    }

    public void Push(T item)
    {
        if (Count == stackArray.Length)
        {
            Resize(); // llama a la funcion para expandir el array
        }

        stackArray[Count] = item; // agrega item al final del array
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        T item = stackArray[Count - 1]; // toma el último elemento agregado
        stackArray[Count - 1] = default;
        Count--;
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        // devuelve el valor del final de la pila
        return stackArray[Count - 1];
    }

    public void Clear()
    {
        // bucle para recorrer todos los elementos de la pila
        for (int i = 0; i < stackArray.Length; i++)
        {
            stackArray[i] = default;
        }

        // reinicia el contador de la pila
        Count = 0;
    }

    public T[] ToArray()
    {
        // crea un nuevo array de tamaño count
        var items = new T[Count];

        // copia los elementos en el rango de la pila
        for (int i = 0; i < Count; i++)
        {
            items[i] = stackArray[Count - 1 - i];
        }

        return items; // regresa un array
    }

    public override string ToString()
    {
        if (Count == 0)
        {
            return "[]";
        }

        T[] items = new T[Count]; // crea un nuevo array de tamaño count¿

        // copia los elementos en la pila
        for (int i = 0; i < Count; i++)
        {
            items[i] = stackArray[Count - 1 - i];
        }

        return "[" + string.Join(", ", items) + "]";
    }

    public bool TryPop(out T item)
    {
        if (Count == 0)
        {
            item = default; // La pila está vacía, asignamos el valor por defecto
            return false;
        }

        item = stackArray[Count - 1]; // Obtiene el último elemento
        stackArray[Count - 1] = default; // Establece ese índice como el valor por defecto
        Count--; // Decrementa el contador de elementos

        return true;
    }

    public bool TryPeek(out T item)
    {
        if (Count == 0)
        {
            item = default; // La pila está vacía
            return false;
        }

        item = stackArray[Count - 1]; // Obtiene el último elemento

        return true;
    }


    private void Resize()
    {
        int newCapacity = stackArray.Length * 2; // crea una variable local con el doble del tamaño del array
        T[] newArray = new T[newCapacity];

        for (int i = 0; i < Count; i++)
        {
            newArray[i] = stackArray[i];
        }

        stackArray = newArray; // asigna el nuevo array al de la pila
    }
}
