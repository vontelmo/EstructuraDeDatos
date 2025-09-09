using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyStack<T>
{
    public int Count { get; private set; }

    private T[] stackArray;
    private int tail;
    private const int defaultSize = 4;

    public MyStack()
    {
        stackArray = new T[defaultSize];
        tail = 0;
        Count = 0;
    }

    public void Push(T item)
    {
        if (Count == stackArray.Length)
        {
            Resize(); // llama a la funcion para expandir el array
        }

        stackArray[tail] = item; // agrega item al tail del array
        tail = (tail + 1) % stackArray.Length; // mueve el puntero tail de posicion de forma circular dentro de los limites del array
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        tail = (tail - 1 + stackArray.Length) % stackArray.Length; // mueve el tail de forma circular hacia atrás
        T item = stackArray[tail]; // toma el último elemento agregado
        Count--;
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        // devuelve el valor de la cabeza de queue
        return stackArray[tail];
    }

    public void Clear()
    {
        // bucle para recorrer todos los indices del queue y asignarlos a valor default
        for (int i = 0; i < stackArray.Length; i++)
        {
            stackArray[i] = default;
        }

        // reinicia los punteros y el contador del queue
        tail = 0;
        Count = 0;
    }

    public T[] ToArray()
    {
        // crea un nuevo array de tamaño count y solo toma los valores entre head y tail
        var items = new T[Count];

        // copia los elementos en el rango de la cola (entre head y tail)s
        for (int i = 0; i < Count; i++)
        {
            items[i] = stackArray[i];
        }

        return items; // regresa un array
    }

    public override string ToString()
    {
        if (Count == 0)
        {
            return "[]";
        }

        T[] items = new T[Count]; // crea un nuevo array de tamaño count y solo toma los valores entre head y tail

        // copia los elementos en el rango de la cola (entre head y tail)
        for (int i = 0; i < Count; i++)
        {
            items[i] = stackArray[i];
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

        // El último elemento agregado está en (tail - 1)
        tail = (tail - 1 + stackArray.Length) % stackArray.Length; // Mueve el puntero tail hacia atrás
        item = stackArray[tail]; // Obtiene el último elemento
        stackArray[tail] = default; // Establece ese índice como el valor por defecto
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

        // El último elemento agregado está en (tail - 1)
        int peekIndex = (tail - 1 + stackArray.Length) % stackArray.Length; // Calcula el índice del último elemento
        item = stackArray[peekIndex]; // Obtiene el último elemento sin modificar el `tail`

        return true;
    }


    private void Resize()
    {
        int newCapacity = stackArray.Length * 2; // crea una variable local con el doble del tamaño del array
        T[] newArray = new T[newCapacity]; // inicia un nuevo array con la capacidad duplicada

        for (int i = 0; i < Count; i++)
        {
            newArray[i] = stackArray[(tail - 1 - i + stackArray.Length) % stackArray.Length]; // copia los valores del array original
        }

        stackArray = newArray; // asigna el nuevo array al del queue
        tail = Count; // actualiza el puntero tail
    }
}
