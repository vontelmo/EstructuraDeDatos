using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

public class SimpleList<T> : ISimpleList<T>
{
    const int defaultSize = 4;
    private T[] array;
    private int count = 0;

    public T this[int index]
    {
        get => array[index];
        set => array[index] = value;
    }

    public int Size => defaultSize;

    public int Count { get { return count; } }
       
    public SimpleList()
    {
        array = new T[defaultSize];
    }

    public SimpleList(int size)
    {
        array = new T[size];
    }

    public void Add(T item)
    {
        if (array.Length <= count)
        {
            // duplicar array y agregar espacio
            int newArrayLenght = array.Length * 2;
            T[] duplicateArray = new T[newArrayLenght];

            for (int i = 0; i < array.Length; i++)
            {
                duplicateArray[i] = array[i];
            }

            array = duplicateArray;
        }

        array[count] = item;
        count++;
    }

    public void AddRange(T[] collection)
    {
        foreach (T item in collection)
        {
            Add(item);   
        }
    }

    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            array[i] = default;
        }
        count = 0;
    }

    public bool Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(array[i], item))
            {
                for (int j = i; j < count - 1; j++)
                    array[j] = array[j + 1];

                array[count - 1] = default;
                count--;
                return true;
            }
        }
        return false;
    }
}
