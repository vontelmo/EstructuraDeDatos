using MyLinkedList;
using System;
using System.Collections;
using System.Collections.Generic;

public static class QuickSort
{
    public static void QuickSorting<T>(SimpleList<T> list) where T : IComparable<T>
    {
        QuickSortInternal(list, 0, list.Count - 1);
    }

    private static void QuickSortInternal<T>(SimpleList<T> list, int left, int right) where T : IComparable<T>
    {
        if (left >= right)
        {
            return;
        }

        int partition = PartitionInternal(list, left, right);

        QuickSortInternal(list, left, partition - 1);
        QuickSortInternal(list, partition + 1, right);
    }

    private static int PartitionInternal<T>(SimpleList<T> list, int left, int right) where T : IComparable<T>
    {
        T partition = list[right];

        // stack items smaller than partition from left to right
        int swapIndex = left;

        for (int i = left; i < right; i++)
        {
            T item = list[i];

            if (item.CompareTo(partition) <= 0)
            {
                list[i] = list[swapIndex];
                list[swapIndex] = item;

                swapIndex++;
            }
        }

        // put the partition after all the smaller items
        list[right] = list[swapIndex];
        list[swapIndex] = partition;

        return right;
    }

    public static void QuickSorting<T>(MyList<T> list) where T : IComparable<T>
    {
        if (list == null || list.Count <= 1)
        {
            return;
        }

        QuickSortInternal(list.Root, list.Tail);
    }

    private static void QuickSortInternal<T>(MyNode<T> low, MyNode<T> high) where T : IComparable<T>
    {
        if (low != null && high != null && low != high && low.Prev != high) // Caso base recursion
        {
            MyNode<T> pivot = Partition(low, high);
            QuickSortInternal(low, pivot.Prev);
            QuickSortInternal(pivot.Next, high);
        }
    }

    private static MyNode<T> Partition<T>(MyNode<T> low, MyNode<T> high) where T : IComparable<T>
    {
        T pivotValue = high.Value;
        MyNode<T> i = low.Prev; // Puntero

        for (MyNode<T> j = low; j != high; j = j.Next)
        {
            if (j.Value.CompareTo(pivotValue) <= 0)
            {
                i = (i == null) ? low : i.Next;
                // Swap values
                T temp = i.Value;
                i.Value = j.Value;
                j.Value = temp;
            }
        }

        i = (i == null) ? low : i.Next; // Operador ternario if (true : false)

        // Swap pivot value to its correct position
        T tempPivot = i.Value;
        i.Value = high.Value;
        high.Value = tempPivot;

        return i; // Return the pivot node
    }
}
