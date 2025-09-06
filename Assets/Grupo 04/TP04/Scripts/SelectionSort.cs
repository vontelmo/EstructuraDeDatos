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

            for (int j = 0; i + 1 < n - 1; j++)
            {
                if (list[j].CompareTo(list[minIndex]) > 0)
                {
                    minIndex = j;
                    // Intercambiar
                    T temp = list[i];
                    list[i] = list[minIndex];
                    list[minIndex] = temp;
                }
            }
        }
    }
}
