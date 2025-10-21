using System;
using MyLinkedList;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    public static void BubbleSorting<T>(SimpleList<T> list) where T : IComparable<T>
    {
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (list[j].CompareTo(list[j + 1]) > 0)
                {
                    // Intercambiar
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    public static void BubbleSortingMyList<T>(MyList<T> list) where T : IComparable<T>
    {
        Debug.Log("Ordenando con BubbleSorting MyList");

        if (list.Count <= 1)
        {
            Debug.Log("Lista vacía o con un solo elemento.");
            return;
        }

        bool swapped;
        MyNode<T> current;
        MyNode<T> lastSorted = null;

        do
        {
            swapped = false;
            current = list.Root; // comienza desde el principio de la lista
            MyNode<T> prev = null;

            Debug.Log("Valor de list.Count: " + list.Count);
            Debug.Log("Valor de list.Root: " + (list.Root != null ? list.Root.Value.ToString() : "null"));
            Debug.Log("Valor de list.Root.Next: " + (list.Root?.Next != null ? list.Root.Next.Value.ToString() : "null"));

            // recorre la lista hasta el nodo previo al ultimo nodo ordenado
            while (current != null && current.Next != lastSorted)
            {
                Debug.Log($"Tipo de current.Value: {current.Value.GetType()}");

                if (current.Value.CompareTo(current.Next.Value) > 0)
                {
                    Debug.Log($"Intercambiando: {current.Value} > {current.Next.Value}");

                    // Swap data values
                    T temp = current.Value;
                    current.Value = current.Next.Value;
                    current.Next.Value = temp;

                    swapped = true;
                }

                prev = current;
                current = current.Next;
            }
            lastSorted = current;

        } while (swapped);

        Debug.Log("Lista después de ordenar: " + list.ToString());
    }
}
