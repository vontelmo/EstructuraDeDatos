using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MyQueue<T>
{
    public int Count { get; private set; }

    private T[] queueArray;
    private int head;
    private int tail;
    private const int defaultSize = 4;

    // constructor
    public MyQueue()
    {
        queueArray = new T[defaultSize];
        head = 0;
        tail = 0;
        Count = 0;
    }

    public void Enqueue(T item)
    {
        if (Count == queueArray.Length)
        {
            Resize(); // llama a la funcion para expandir el array
        }

        queueArray[tail] = item; // agrega item al tail del array
        tail = (tail + 1) % queueArray.Length; // mueve el puntero tail de posicion dentro de los limites del array por el modulo
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T value = queueArray[head]; // toma el valor de la cabeza de la queue
        queueArray[head] = default; // asigna el valor default al head
        head = (head + 1) % queueArray.Length; // mueve el puntero head
        Count--;

        return value; // retorna el valor quitado de queue
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        // devuelve el valor de la cabeza de queue
        return queueArray[head];
    }

    public void Clear()
    {
        // bucle para recorrer todos los indices del queue y asignarlos a valor default
        for (int i = 0; i < queueArray.Length; i++)
        {
            queueArray[i] = default;
        }

        // reinicia los punteros y el contador del queue
        head = 0;
        tail = 0;
        Count = 0;
    }

    public T[] ToArray()
    {
        // crea un nuevo array de tamaño count y solo toma los valores entre head y tail
        var items = new T[Count];

        for (int i = 0; i < Count; i++)
        {
            items[i] = queueArray[i];        
        }

        return items; // regresa un array
    }

    public override string ToString()
    {
        if (Count == 0)
        {
            return "[]";
        }

        var items = new T[Count]; // crea un nuevo array de tamaño count y solo toma los valores entre head y tail

        for (int i = 0; i < Count; i++)
        {
            items[i] = queueArray[i];
        }

        return "[" + string.Join(", ", items) + "]";
    }

    public bool TryDequeue(out T item)
    {
        if (Count == 0)
        {
            item = default;
            return false;
        }

        item = queueArray[head]; // toma el valor de la cabeza de la queue
        queueArray[head] = default; // asigna el valor default al head
        head = (head + 1) % queueArray.Length; // mueve el puntero head
        Count--;

        return true;
    }

    public bool TryPeek(out T item)
    {
        if (Count == 0)
        {
            item = default; // Asigna el valor por defecto de T si la cola está vacía
            return false; // Cola vacía
        }

        item = queueArray[head]; // Si la cola no está vacía, devuelve el primer elemento
        return true; // Indica que la operación fue exitosa
    }

    private void Resize()
    {
        int newCapacity = queueArray.Length * 2; // crea una variable local con el doble del tamaño del array
        T[] newArray = new T[newCapacity]; // inicia un nuevo array con la capacidad duplicada

        for (int i = 0; i < Count; i++)
        {
            newArray[i] = queueArray[(head + i) % queueArray.Length]; // copia los valores del array original
        }

        queueArray = newArray; // asigna el nuevo array al del queue
        head = 0;
        tail = Count;
    }
}
