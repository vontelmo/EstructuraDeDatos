using MyLinkedList;
using System;
using System.Collections;
using System.Collections.Generic;

public static class BubbleSort
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

    public static void BubbleSorting<T>(MyList<T> list) where T : IComparable<T>
    {
        if (list.Tail == null || list.Tail.Next == null)
        {
            return;
        }

        bool swapped;
        MyNode<T> current;
        MyNode<T> lastSorted = null;

        do
        {
            swapped = false;
            current = list.Tail;
            MyNode<T> prev = null;

            while (current.Next != lastSorted)
            {
                if (current.Value.CompareTo(current.Next.Value) > 0)
                {
                    // Swap data values
                    T temp = current.Value;
                    current.Value = current.Next.Value;
                    current.Next.Value = temp;
                    swapped = true;
                }
                prev = current;
                current = current.Next;
            }
            lastSorted = current; // Mark the current end as sorted
        } while (swapped);
    }
}
