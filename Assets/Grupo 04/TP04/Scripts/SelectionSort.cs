using MyLinkedList;
using System;
using System.Collections;
using System.Collections.Generic;

public static class SelectionSort
{
    public static void SelectionSorting<T>(SimpleList<T> list) where T : IComparable<T>
    {
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++) 
            {
                if (list[j].CompareTo(list[minIndex]) < 0) 
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                T temp = list[i];
                list[i] = list[minIndex];
                list[minIndex] = temp;
            }
        }
    }


    public static void SelectionSorting<T>(MyList<T> list) where T : IComparable<T>
    {
        if (list == null || list.Count <= 1)
        {
            return;
        }

        MyNode<T> currentOuter = list.Root;

        while (currentOuter != null)
        {
            MyNode<T> minNode = currentOuter;
            MyNode<T> currentInner = currentOuter.Next;

            while (currentInner != null)
            {
                if (currentInner.Value.CompareTo(minNode.Value) < 0)
                {
                    minNode = currentInner;
                }
                currentInner = currentInner.Next;
            }

            // Intercambiar los valores si se encontro un valor menor
            if (minNode != currentOuter)
            {
                T tempData = currentOuter.Value;
                currentOuter.Value = minNode.Value;
                minNode.Value = tempData;
            }

            currentOuter = currentOuter.Next;
        }
    }
}
